# WCharT.NetStandard
[![Build Status](https://img.shields.io/github/actions/workflow/status/badcel/WCharT.Net/ci.yml?branch=main)](https://github.com/badcel/WCharT.Net/actions/workflows/ci.yml)[![NuGet](https://img.shields.io/nuget/v/WCharT.Net)](https://www.nuget.org/packages/WCharT.Net/)[![License (MIT)](https://img.shields.io/github/license/badcel/WCharT.Net)](https://github.com/badcel/WCharT.Net/blob/main/license.txt)

Welcome to WCharT.NetStandard (a legacy cross platform package to interop with WCharT data.

## Use
To work with WCharT data create a new instance of `WCharTString`:

```csharp
//Read data from a byte*
byte* pointer = NativeCall();
var str = new WCharTString(pointer).GetString();

//Create a buffer for a native library and read the data after it was filled
ReadOnlySpan<byte> data = new WCharTString(int bufferCharSize);
NativeCall(...);
var str = data.GetString();

//Pass a string to a native library
ReadOnlySpan<byte> data = new WCharTString(string str);
NativeCall(data);

//Can be used in fixed statements
var data = new WCharTString(string str);
fixed (byte* ptr = data)
{
    NativeCall(ptr);
}
```

## Build
To build the solution locally execute the following commands:

```sh
$ git clone https://github.com/jmoyola/WCharT.NetStandard
$ cd WCharT.Net/src
$ dotnet fsi build.fsx
```

## Licensing terms
HidApi.NetStandard is licensed under the terms of the MIT-License. Please see the [license file][license] for further information.

[license]:https://github.com/jmoyola/WCharT.NetStandard/blob/main/license.txt

## Thanks to

Original code programming fork: https://github.com/badcel/WCharT.Net