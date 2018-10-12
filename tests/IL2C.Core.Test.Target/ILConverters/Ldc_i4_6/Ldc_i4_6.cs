using System.Runtime.CompilerServices;

namespace IL2C.ILConverters
{
    [Case("RawValue", 6)]
    [Case("Add", 10, 4)]
    public static class Ldc_i4_6
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int RawValue();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Add(int num);
    }
}
