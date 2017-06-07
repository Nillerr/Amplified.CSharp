# Amplified.CSharp

[![NuGet](https://img.shields.io/nuget/v/Amplified.CSharp.svg)]()[![CI](https://ci.appveyor.com/api/projects/status/penxirmcfh2mhjxt/branch/develop?svg=true)]()

## Installation

The project is available as a [NuGet](https://www.nuget.org/packages/Amplified.CSharp) package.

```
Install-Package Amplified.CSharp -Pre
```
 
## Usage

### Static imports

It is recommended to add the following using statement to files where a instances of the types within this 
library are created.
```C#
using static Amplified.CSharp.Maybe;
```

This allows you to use the library as it was intended. Example:
```C#
using Amplified.CSharp;
...
    Maybe<int> foo = Maybe.Some(1);
    Maybe<int> bar = Maybe.None();
...
```

Becomes:
```C#
using Amplified.CSharp;
using static Amplified.CSharp.Maybe;
...
    Maybe<int> foo = Some(1);
    Maybe<int> bar = None();
...
``` 

### Unit

`Unit` is represents the lack of a value, sort of like `void`. Unlike `void`, it is an actual type, and can 
be referenced and operated upon.

#### Examples

```C#
using System;
using Amplified.CSharp;

class Program
{
    public static void Main(string[] args)
    {
        var arg1 = args.FirstOrNone().OrThrow(() => new ArgumentException());
        
        Unit ignored = ParseInt(arg1)
            .Map(intArg => Store(intArg).Return(intArg))
            .Match(
                intArg => Console.WriteLine($"Stored: {intArg}"),
                none => Console.WriteLine("Invalid input arguments") 
            );
    }
    
    public static Unit Store(int value)
    { ... }
}
```

### Maybe

`Maybe<T>` represents the possibility for the presence of a value, sort of like `null` does for reference 
types, but more like `Nullable<T>` or for value types. The idea is to prevent runtime errors and exceptions, 
by forcing you, the developer, to consider all cases before operating on the value within a `Maybe<T>`.

It is a discriminated union / sum type, that consist of 2 types: `Some<T>` and `None`. Due to 
the way type inference works in C#, we must create a separate type `None`, which is implicitly convertible 
to `Maybe<T>`, without having any type arguments itself. The reason this is required is to support the 
`None()` syntax, creating a `None` without specifying a type argument. 

#### Usage

```C#
using System;
using Amplified.CSharp;

class Program {
    public static void Main(string[] args) 
    {
        var productId = new ProductId(67748);
        
        Unit ignored = GetRelatedProducts(productId)
            .Match(
                relatedProducts => DisplayRelatedProducts(productId, relatedProducts),
                none => DisplayNoRelatedProducts(productId)
            );
    }
    
    private Unit DisplayNoRelatedProducts(ProductId source)
    {
        Console.WriteLine($"Product {source} has no related products");
    }
    
    private void DisplayRelatedProducts(ProductId source, ProductId[] relatedProducts) 
    {
        var relatedProductsString = string.Join(", ", relatedProducts); 
        Console.WriteLine($"Product {source} has these related products: {relatedProductsString}");
    }

    private Maybe<ProductId[]> GetRelatedProducts(ProductId id)
    {
        return _productRepository.Product(productId)
            .Map(product => product.RelatedProducts);
    }
    
    private Maybe<Product> Product(ProductId id)
    { ... }
}
```

#### Some

`Maybe.Some<T>(T value)` represents the presence of a value. It is recommended to use a static import of 
`Amplified.CSharp.Maybe` to achieve a fluent functional syntax.

#### None

`None` represents the lack of a value. This is comparable to `void`, but unlike `void`, `None` is a value 
itself. Every instance of `None` is equal to any other instance of `None`. Other types can achieve `None` 
equality by implementing the interface `ICanBeNone`, which e.g. `Maybe<T>` does.

There's no difference between using `new None()` vs `default(None)` vs `Maybe.None()` 
in terms of equality. Every one of those 3 operations return the same value.

#### Async

To support seamless integration with the Task Parallel Library and the `async` / `await` keywords, the type 
`AsyncMaybe<T>` was created. This type acts as a deferred `Maybe<T>`, and is really just a wrapper around it, 
using `Task` objects.

`AsyncMaybe<T>` will be migrated to use the new `ValueTask` in the future.

All methods operating asynchronously has been named with the `Async` suffix, e.g. 
```C#
public static AsyncMaybe<TResult> MapAsync<T, TResult>(this Maybe<T> source, Func<T, Task<TResult>> mapper);
public static AsyncMaybe<TResult> MapAsync<T, TResult>(this AsyncMaybe<T> source, Func<T, Task<TResult>> mapper);
```

As you can tell from the method signature in the first line in the block above, whenever you operate 
asynchronously on an instance of `Maybe<T>`, an `AsyncMaybe<T>` is returned. In order to return to `Maybe<T>`, 
you must simply `await` the instance of `AsyncMaybe<T>`.
```C#
public async Task<string> FetchToken()
{
    AsyncMaybe<string> asyncToken = CachedToken()
        .OrAsync(() => ObtainToken())
        .MapAsync(token => token.ToString())
        
    Maybe<string> token = await asyncToken;
    return token.OrReturn("Unable to obtain token");
}

public Maybe<Token> CachedToken()
{ ... }

public async Task<Token> ObtainToken()
{
    Token token = await ...;
    StoreTokenInCache(token);
    return token;
}
```

#### Converions

```C#
using System;
using System.Threading.Tasks;
using Amplified.CSharp;
using static Amplified.CSharp.Maybe;

public void Demonstration()
{
    // Maybe<T>
    Maybe<int> none = None(); // Implicit conversion from None to Maybe<T>
    Maybe<int> noneFromNullable = OfNullable((int?) null);

    Maybe<int> some = Some(1);
    Maybe<int> someFromNullable = OfNullable((int?) 1);
    
    // AsyncMaybe<T>
    AsyncMaybe<int> someFromMaybe = Some(1); // Implicit conversion from Maybe<T> to AsyncMaybe<T>
    AsyncMaybe<int> someFromTask = Some(Task.FromResult(1));
    AsyncMaybe<int> someFromNullable = OfNullable(Task.FromResult<int?>(1));
    
    AsyncMaybe<int> noneFromMaybe = None(); // Implicit conversion from None to AsyncMaybe<T>
    AsyncMaybe<int> noneFromNullable = OfNullable(Task.FromResult<int?>(null));
    
    // Maybe<T> --> AsyncMaybe<T>
    Some(1) // Maybe<int>
        .{Operator}(...) // Maybe<TResult>
        .{Operator}Async(...) // AsyncMaybe<TResult>
        .ToAsync() // AsyncMaybe<T>
        
    /// AsyncMaybe<T> --> Maybe<T>
    AsyncMaybe<int> asyncInt = Some(1).ToAsync(); 
    Maybe<int> nonAsyncInt = await asyncInt;
}
```

### LINQ

In order to provide a familiar interface for C# developers, we've provided LINQ-like synonyms for a few 
extension methods. We recommend you use the native `Map` and `Fitler` operators, to easily distinquis 
between `IEnumerable` / `IQueryable` and `Maybe`.

The following methods are synonyms:
```C#
// LINQ synonym for Map
Maybe<TResult> Select<T, TResult>(Func<T, TResult> mapper);

// LINQ synonym for Filter
Maybe<T> Where<T>(Func<T, bool> predicate);
```

```C#
// Asynchronous LINQ synonyms for Map
AsyncMaybe<TResult> SelectAsync<T, TResult>(Func<T, TResult> mapper);
AsyncMaybe<TResult> SelectAsync<T, TResult>(Func<T, Task<TResult>> mapper);

// Asynchronous LINQ synonyms for Filter
AsyncMaybe<T> WhereAsync<T>(Func<T, bool> predicate);
AsyncMaybe<T> WhereAsync<T>(Func<T, Task<bool>> predicate);
```

### Null

None of the types accepts `null` values as parameters. Creating an instance of one of the types, by 
passing `null` as the value parameter, will throw an `ArgumentNullException`. Similarly, returning 
`null` from a lambda passed to any of the extension methods will also throw an 
`ArgumentNullException`. The exceptions to this are `OrReturn<T>(T defaultValue)` and any of the 
`Match()` methods on `Maybe<T>`. Basically any non-terminating operators.

### Implicit Conversion (Needs revision)

To create a seamless API, 

```C#
using System;
using Amplified.CSharp;
using static Amplified.CSharp.Maybe;

public class Program
{
    public static void Main(string[] args)
    {
        // None is implicitly convertible to Maybe<T>, resulting in a Maybe<T> without a value.
        Maybe<int> noInt = None();
        
        // T is implicitly convertible to Some<T>.
        Some<int> someInt = 2;
        
        // Throws an exception, because Some<T> cannot contain a null value.
        Some<object> someObject = null;
        
        // Some<T> is implicitly convertible to Maybe<T>, resulting in a Maybe<T> with a value.
        Maybe<int> maybeSomeInt = someInt;
    }
}
```

### Equality

```C#
using System;
using Amplified.CSharp;
using static Amplified.CSharp.Maybe;

public class Program
{
    public static void Main(string[] args)
    {
        // Maybe<T> == None
        None none = None();
        Maybe<int> someInt = Some(2);
        
        if (none != someInt)
            Console.WriteLine($"{noneInt} != {someInt}");
        
        // Maybe<T> is comparable to Some<T>
        Some<int> someIntNotEqual = 3;
        Some<int> someIntEqual = 2;
        Assert.NotEqual(maybeSomeInt, someIntNotEqual);
        Assert.Equal(maybeSomeInt, someIntEqual);
        
        // Maybe<T> is comparable to Maybe<T>
        Maybe<int> maybeSomeOtherIntEqual = 2;
        Maybe<int> maybeSomeOtherIntNotEqual = 3;
        Assert.Equal(maybeSomeInt, maybeSomeOtherIntEqual);
        Assert.NotEqual(maybeSomeInt, maybeSomeOtherIntNotEqual);   
        
        // Some<T> is comparable to None, but is always false.
        Assert.NotEqual(maybeSomeInt, None._);
        
        // All of these are also comparable to T, which causes T to be implicitly converted to Some<T>. This 
        // also means that comparing to null will throw an exception.
    }
}
```

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
