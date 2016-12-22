# Amplified.CSharp

[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)]()

Provides a set of monadic types for use with C#.

 - `None`
 - `Some<T>`
 - `Maybe<T>`
 - `Either<TLeft, TRight>`
 - `Try<T>`

These types provide null safety and functional chaining, to provider a functional-like programming 
style in C#. All the monads are structs, which enforces their non-`null` behaviour.

## Installation

The project is available as a [NuGet](https://www.nuget.org/packages/Amplified.CSharp) package.

```
Install-Package Amplified.CSharp
```

## Null

The monads does not accept `null` values. Creating an instance of one of the monads, by passing 
`null` as the value parameter, will throw an `ArgumentNullException`.

## Type Token

 - `Type<T>`

## License

MIT License

Copyright (c) 2016 Nicklas Jensen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
