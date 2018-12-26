#ifndef System_Runtime_InteropServices_NativePointer_H__
#define System_Runtime_InteropServices_NativePointer_H__

#pragma once

#include <il2c.h>

#ifdef __cplusplus
extern "C" {
#endif

/////////////////////////////////////////////////////////////
// System.Runtime.InteropServices.NativePointer

typedef void* System_Runtime_InteropServices_NativePointer;

IL2C_DECLARE_RUNTIME_TYPE(System_Runtime_InteropServices_NativePointer);

#define System_Runtime_InteropServices_NativePointer_op_Implicit(value) ((void*)(value))
#define System_Runtime_InteropServices_NativePointer_op_Implicit_1(value) ((intptr_t)(value))

#ifdef __cplusplus
}
#endif

#endif
