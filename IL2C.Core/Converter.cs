﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IL2C
{
    public static class Converter
    {
        internal static IEnumerable<ILData> DecodeAndEnumerateOpCodes(DecodeContext context)
        {
            while (true)
            {
                var label = context.MakeLabel();
                if (context.TryDecode(out var ilc) == false)
                {
                    break;
                }

                var operand = ilc.DecodeOperand(context);
                yield return new ILData(label, ilc, operand);

                if (ilc.IsEndOfPath)
                {
                    yield break;
                }
            }
        }

        public static void Convert(TextWriter tw, MethodInfo method, string indent)
        {
            var methodName = Utilities.GetFullMethodName(method);
            var module = method.DeclaringType.Module;

            InternalConvert(
                tw,
                method.ReturnType,
                methodName,
                method.GetParameters(),
                method.GetMethodBody(),
                module,
                indent);
        }

        private struct GeneratedSourceCode
        {
            public readonly Label Label;
            public readonly string SourceCode;

            public GeneratedSourceCode(Label label, string sourceCode)
            {
                this.Label = label;
                this.SourceCode = sourceCode;
            }
        }

        private static void InternalConvert(
            TextWriter tw,
            Type returnType,
            string methodName,
            ParameterInfo[] parameters,
            MethodBody body,
            Module module,
            string indent)
        {
            var locals = body.LocalVariables;

            var returnTypeName =
                Utilities.GetCLanguageTypeName(returnType);

            tw.WriteLine("#include <stdbool.h>");
            tw.WriteLine("#include <stdint.h>");

            tw.WriteLine();

            var parametersString = string.Join(
                ", ",
                parameters.Select(parameter => string.Format(
                    "{0} {1}",
                    Utilities.GetCLanguageTypeName(parameter.ParameterType),
                    parameter.Name)));

            tw.WriteLine("{0} {1}({2})",
                returnTypeName,
                methodName.Replace(".", "_"),
                (parametersString.Length >= 1) ? parametersString : "void");
            tw.WriteLine("{");

            foreach (var local in locals)
            {
                tw.WriteLine(
                    "{0}{1} local{2};",
                    indent,
                    Utilities.GetCLanguageTypeName(local.LocalType),
                    local.LocalIndex);                
            }

            tw.WriteLine();

            var decodeContext = new DecodeContext(
                methodName,
                returnType,
                parameters,
                locals,
                body.GetILAsByteArray(),
                module);

            var bodySourceCode = new List<GeneratedSourceCode>();
            while (decodeContext.TryDequeueNextPath())
            {
                bodySourceCode.AddRange(
                    from ilData in DecodeAndEnumerateOpCodes(decodeContext)
                    let sourceCode = ilData.ILConverter.Apply(ilData.Operand, decodeContext)
                    select new GeneratedSourceCode(ilData.Label, sourceCode));
            }

            foreach (var si in decodeContext.ExtractStacks())
            {
                tw.WriteLine(
                    "{0}{1} {2};",
                    indent,
                    Utilities.GetCLanguageTypeName(si.TargetType),
                    si.SymbolName);
            }

            tw.WriteLine();

            foreach (var entry in bodySourceCode)
            {
                if (decodeContext.TryGetLabelName(
                    entry.Label, out var labelName))
                {
                    tw.WriteLine("{0}:", labelName);
                }

                if (entry.SourceCode != null)
                {
                    tw.WriteLine(
                        "{0}{1};",
                        indent, entry.SourceCode);
                }
            }

            tw.WriteLine("}");
        }
    }
}
