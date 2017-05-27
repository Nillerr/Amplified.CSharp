# Amplified.CSharp

[![NuGet](https://img.shields.io/nuget/v/Amplified.CSharp.svg)]()

## Installation

The project is available as a [NuGet](https://www.nuget.org/packages/Amplified.CSharp) package.

```
Install-Package Amplified.CSharp
```
 
## Usage

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

### None

`None` represents the lack of a value. This is comparable to `void`, but unlike `void`, `None` is a value 
itself. Every instance of `None` is equal to any other instance of `None`. Other types can achieve `None` 
equality by implementing the interface `ICanBeNone`, which e.g. `Maybe<T>` does.

There's no difference between using `None.Instance` vs `new None()` vs `default(None)` in terms of equality. Every 
one of those 3 operations return the same value.

### Maybe

`Maybe<T>` represents the possibility for the presence of a value, sort of like `null` does for reference 
types, but more like `Nullable<T>` or for value types. The idea is to prevent runtime errors and exceptions, 
by forcing you, the developer, to consider all cases before being able to extract the value within a `Maybe<T>`.

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

#### Async

To support seamless integration with the Task Parallel Library and the `async` / `await` keywords, the type 
`AsyncMaybe<T>` was created. This type acts as a deferred `Maybe<T>`, and is really just a wrapper around it, 
using `Task` objects. This will be migrated to use the new `ValueTask` in the future.

All methods operating asynchronously has been named with the `Async` suffix, e.g. 
```C#
public AsyncMaybe<TResult> MapAsync<T, TResult>(this Maybe<T> source, Func<T, Task<TResult>> mapper)
public AsyncMaybe<TResult> MapAsync<T, TResult>(this AsyncMaybe<T> source, Func<T, Task<TResult>> mapper)
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
using static Amplified.CSharp.Maybe;

public async Task Demonstration()
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
}
```

### LINQ

In order to provide a familiar interface for C# development, we've provided synonyms for a few extension methods.

The following methods are synonyms:
```C#
Maybe<TResult> Map<T, TResult>(Func<T, TResult> mapper);
Maybe<TResult> Select<T, TResult>(Func<T, TResult> mapper); // LINQ Synonym for Map
```

```C#
Maybe<T> Filter<T>(Func<T, bool> predicate);
Maybe<T> Where<T>(Func<T, bool> predicate); // LINQ Synonym for Filter
```

### Null

None of the types accepts `null` values as parameters. Creating an instance of one of the types, by passing 
`null` as the value parameter, will throw an `ArgumentNullException`.

### Implicit Conversion

```C#
public class Program {
    public static void Main(string[] args) {
        // None is implicitly convertible to Maybe<T>, resulting in a Maybe<T> without a value.
        Maybe<int> noInt = None._;
        
        // T is implicitly convertible to Some<T>.
        Some<int> someInt = 2;
        
        // Throws an exception, because Some<T> cannot contain a null value.
        Some<object> someObject = null;
        
        // Some<T> is implicitly convertible to Maybe<T>, resulting in a Maybe<T> with a value.
        Maybe<int> maybeSomeInt = someInt;
        
        // T is implicitly convertible to Maybe<T>, resulting in a Maybe<T> with a value.
        Maybe<int> maybeSomeInt = 201;
        
        // Throws an exception, because Maybe<T> cannot contain a null value.
        Maybe<object> maybeObject = null;
    }
}
```

### Equality

```C#
public class Program {
    public static void Main(string[] args) {
        // Maybe<T> is comparably to None
        Maybe<int> maybeNoInt = None._;
        Maybe<int> maybeSomeInt = 2;
        Assert.NotEqual(noInt, someInt);
        Assert.Equal(noInt, None._);
        
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
