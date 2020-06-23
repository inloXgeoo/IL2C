/////////////////////////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) 2016-2019 Kouji Matsui (@kozy_kekyo, @kekyo2)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//	http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef System_IndexOutOfRangeException_H__
#define System_IndexOutOfRangeException_H__

#pragma once

#include <il2c.h>

#ifdef __cplusplus
extern "C" {
#endif

/////////////////////////////////////////////////////////////
// System.IndexOutOfRangeException

typedef System_Exception System_IndexOutOfRangeException;

typedef System_Exception_VTABLE_DECL__ System_IndexOutOfRangeException_VTABLE_DECL__;

#define System_IndexOutOfRangeException_VTABLE__ System_Exception_VTABLE__

IL2C_DECLARE_RUNTIME_TYPE(System_IndexOutOfRangeException);

static inline void System_IndexOutOfRangeException__ctor(System_IndexOutOfRangeException* this__)
{
    System_Exception__ctor((System_Exception*)this__);
}

static inline void System_IndexOutOfRangeException__ctor__System_String(System_IndexOutOfRangeException* this__, System_String* message)
{
    System_Exception__ctor__System_String((System_Exception*)this__, message);
}

#ifdef __cplusplus
}
#endif

#endif
