# IL2C - A translator for ECMA-335 CIL/MSIL to C language.

![Intermediate language everywhere!](images/banner.png)

## Packages

| Packages | master |
|:---|:---|
| IL2C.Build | [![NuGet IL2C.Build](https://img.shields.io/nuget/v/IL2C.Build.svg?style=flat)](https://www.nuget.org/packages/IL2C.Build) |
| IL2C.Interop | [![NuGet IL2C.Interop](https://img.shields.io/nuget/v/IL2C.Interop.svg?style=flat)](https://www.nuget.org/packages/IL2C.Interop) |
| IL2C.Core | [![NuGet IL2C.Core](https://img.shields.io/nuget/v/IL2C.Core.svg?style=flat)](https://www.nuget.org/packages/IL2C.Core) |
| IL2C.Runtime | [![NuGet IL2C.Runtime](https://img.shields.io/nuget/v/IL2C.Runtime.svg?style=flat)](https://www.nuget.org/packages/IL2C.Runtime) |
| IL2C.Runtime.msvc | [![NuGet IL2C.Runtime.msvc](https://img.shields.io/nuget/v/IL2C.Runtime.msvc.svg?style=flat)](https://www.nuget.org/packages/IL2C.Runtime.msvc) |

| Packages | devel |
|:---|:---|
| IL2C.Build | [![MyGet IL2C.Build](https://img.shields.io/myget/il2c/v/IL2C.Core.svg?style=flat&label=myget)](https://www.myget.org/feed/il2c/package/nuget/IL2C.Build) |
| IL2C.Interop | [![MyGet IL2C.Interop](https://img.shields.io/myget/il2c/v/IL2C.Interop.svg?style=flat&label=myget)](https://www.myget.org/feed/il2c/package/nuget/IL2C.Interop) |
| IL2C.Core | [![MyGet IL2C.Core](https://img.shields.io/myget/il2c/v/IL2C.Core.svg?style=flat&label=myget)](https://www.myget.org/feed/il2c/package/nuget/IL2C.Core) |
| IL2C.Runtime | [![MyGet IL2C.Runtime](https://img.shields.io/myget/il2c/v/IL2C.Runtime.svg?style=flat&label=myget)](https://www.myget.org/feed/il2c/package/nuget/IL2C.Runtime) |
| IL2C.Runtime.msvc | [![MyGet IL2C.Runtime.msvc](https://img.shields.io/myget/il2c/v/IL2C.Runtime.msvc.svg?style=flat&label=myget)](https://www.myget.org/feed/il2c/package/nuget/IL2C.Runtime.msvc) |

## Status

|Configuration|master|
|:--|:--|
|Publish|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-publish-master)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=6)
|Debug|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-master-Debug) ![Azure pipelines tests](https://img.shields.io/azure-devops/tests/kekyo/IL2C/2.svg)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=2)
|Release|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-master-Release) ![Azure pipelines tests](https://img.shields.io/azure-devops/tests/kekyo/IL2C/3.svg)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=3)


|Configuration|devel|
|:--|:--|
|Publish|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-publish-devel)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=6)
|Debug|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-devel-Debug) ![Azure pipelines tests](https://img.shields.io/azure-devops/tests/kekyo/IL2C/4.svg)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=4)<br>![Build Stats](https://buildstats.info/azurepipelines/chart/kekyo/IL2C/4?includeBuildsFromPullRequest=false)|
|Release|[![Azure pipelines (.NET 4.5 / .NET Core 2.0)](https://kekyo.visualstudio.com/IL2C/_apis/build/status/IL2C-devel-Release) ![Azure pipelines tests](https://img.shields.io/azure-devops/tests/kekyo/IL2C/5.svg)](https://kekyo.visualstudio.com/IL2C/_build?definitionId=5)<br>![Build Stats](https://buildstats.info/azurepipelines/chart/kekyo/IL2C/5?includeBuildsFromPullRequest=false)|

## What's this?

* IL2C is a translator (transpiler) of ECMA-335 CIL/MSIL to C language.

* We're aiming for:
  * **Better predictability of runtime costs**  
  Better human readability of C source code translated by IL2C.
  * **Very tiny footprint requirements**  
  We're thinking about how to fit from large system with many resources to tiny embedded system. (KB order for the non-OSes system)
  * **Better code/runtime portability**  
  Minimum requirement is only C99 compiler. The runtime minimum requires only the heap, CAS instructions, (POSIX) signal and setjmp/longjmp. Additional better feature is threading API (Win32, pthreads and FreeRTOS.)
  * **Better interoperabilities for existed C libraries**  
  You can use the standard .NET interop technics (like P/Invoke.)
  * **Containing seamless building systems for major C toolkits**  
  for example: CMake system, Arduino IDE, VC++ ...

## Simple hello-world like code

Original C# source code:

```csharp
public static class HelloWorld
{
    public static void Main()
    {
        Console.WriteLine("Hello world with IL2C!");
    }
}
```

Translated to C source code (all comments are stripped):

```c
IL2C_CONST_STRING(string0__, L"Hello world with IL2C!");

void HelloWorld_Main()
{
    struct
    {
        const IL2C_EXECUTION_FRAME* pNext__;
        const uint16_t objRefCount__;
        const uint16_t valueCount__;
        System_String* stack0_0__;
    } frame__ = { NULL, 1, 0 };
    il2c_link_execution_frame(&frame__);

    frame__.stack0_0__ = string0__;
    System_Console_WriteLine_10(frame__.stack0_0__);
    il2c_unlink_execution_frame(&frame__);
    return;
}
```

[View with comments / other sample translation results (contain complex results)](docs/sample-translation-results.md)

## Getting started

IL2C current status is **experimental**, read a simple ["Getting started"](docs/getting-started.md) for first step.

### Inside IL2C

If you need understanding deep knowledge for IL2C, see ["Inside IL2C"](docs/inside-il2c.md) .

## Overall status

### Following lists are auto-generated by unit test.

* [Supported IL opcodes list](docs/supported-opcodes.md)

* [Supported basic types](docs/supported-basic-types.md)

* [Supported runtime system features](docs/supported-runtime-system-features.md)

* [Supported features (old)](docs/supported-features.md)

## License

Under Apache v2.

## Related information

* Slide: [Making archive IL2C](https://www.slideshare.net/kekyo/making-archive-il2c-655-dotnet600-2018)

  * **Covers overall information about internal IL2C.**
  * #6-52 session in [dotNET 600 2018](https://centerclr.connpass.com/event/101479/) conference.

* Slide: [Write common, run anywhere](https://www.slideshare.net/kekyo/write-common-run-anywhere)
  * #6-51 session in [dotNET 600 2017](https://centerclr.connpass.com/event/71414/)
 conference
  * [Session video (Japanese)](http://bit.ly/2DiaoKZ)
  
* Polish notation calculator:  [Minimum, but useful impls for "Win32", "UEFI" and "M5Stack(ESP32)"](samples/Calculator)
  ![Calculator.M5Stack](images/Calculator.M5Stack.jpg)

* Slide: [How to make the calculator / Making archive IL2C](https://www.slideshare.net/kekyo/how-to-make-the-calculator)
  * #6-52 session in [.NET Fringe Japan 2018](https://dotnetfringe-japan.connpass.com/event/74536/)

* Slide: [Making archive IL2C](https://www.slideshare.net/kekyo/mvp-summit-f-meetup-making-archive-il2c-653)
  * #6-53 session in [MVP Summit F# Meetup / SEATTLE F# USERS GROUP](https://www.meetup.com/en-US/FSharpSeattle/events/247905452/)

* Slide: [Making archive IL2C](https://www.slideshare.net/kekyo/making-archive-il2c-654-at-mvp-summit-2018-vs-hackathon)
  * #6-54 session in [Microsoft MVP Global Summit 2018](https://mvp.microsoft.com/en-us/Summit/Agenda)
 – VS HACKATHON"

* Session video (Japanese): ["Making archive IL2C play list"](http://bit.ly/2xtu4MH)
  * "How to create IL2C or a tool similar to IL2CPP? You see just the pure design process includes the concept."
  * #1 session in [Center CLR #6](https://centerclr.connpass.com/)

* Video letter (Japanese): [Tested IL2C on micro:bit and arduino platform](http://bit.ly/2xGFo9J)
  ![Tested IL2C on micro:bit and arduino platform #8](images/microbit.jpg)

* Session video (Japanese): [Making archive IL2C](http://bit.ly/2hI1jTb)
  * #6-28 session in [.NET Conf 2017 in Tokyo Room B](https://csugjp.connpass.com/event/66004/)

* Milestone 2+ informations (Japanese): [Extensive Xamarin - Xamaritans](http://bit.ly/2ycNVzW)
  * included in the booklet "Extensive Xamarin - Techbook Fest part 3"

## Photos of design process

* #6-6: Near milestones

  ![#6-6: Near milestones](images/IMG_20170917_194810.jpg)

* #6-14: Data flow analysis

  ![#6-14: Data flow analysis](images/IMG_20170926_225355.jpg)

* Milestone 1: Test on VC++ and check how C compiler's optimizer works.

  ![Milestone 1: Test on VC++ and check how C compiler's optimizer works](images/il2c1.png)

* #6-48: How mark-and-sweep garbage collection works on a translated code.

  ![#6-48: How mark-and-sweep garbage collection works on a translated code](images/IMG_20171130_200549.jpg)

* How overload/override/virtual method symbol calculus work.

  ![How overload/override/virtual method symbol calculus work](images/IMG_20181028_165314.jpg)
  
* How to translate exception handlers when combined the local unwind and global unwind.
  * We are thinking about at [Center CLR Try! development meetup (11/10/2018, Japanese)](https://centerclr.connpass.com/event/107981/)

  ![How to translate exception handlers when combined the local unwind and global unwind](images/IMG_20181110_181756.jpg)
    
* This is the strcuture graph for the exection-frame and exception-frame.

  ![This is the strcuture graph for the exection-frame and exception-frame](images/IMG_20181112_120412.jpg)
