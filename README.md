# Amplified.CSharp

[![NuGet](https://img.shields.io/nuget/v/Amplified.CSharp.svg)]()

Provides a set of types for use with C#.

 - `None`
 - `Some<T>`
 - `Maybe<T>`
 - `Either<TLeft, TRight>`
 - `Try<T>`

These types provide `null` safety and functional chaining, to provider a functional-like programming 
style in C#. All the types are structs, ensuring they themselves will never be `null` when passed around.

## Installation

The project is available as a [NuGet](https://www.nuget.org/packages/Amplified.CSharp) package.

```
Install-Package Amplified.CSharp
```
 
## Usage

### Some

`Some<T>` represents the presence of a value. In other words, for reference types, this represents a 
non-`null` value.

#### Usage

```C#
public interface IPriceRepository {
    Maybe<Price> PriceFor(Product product);
}

public class PriceService {
    
    private readonly IPriceRepository _priceRepository;
    
    public Some<Price> PriceFor(Product product) {
        return _priceRepository.PriceFor(product)
            // Returns Price, but is implicitly converted to Some<Price>.
            .OrReturn(() => new Price(0));
    }
    
}
```

### None

`None` represents the lack of a value. This is comparable to `void`, but unlike `void`, `None` is a value 
itself. Every instance of `None` is equal to any other instance of `None`. Other types can achieve `None` 
equality by implementing the interface `ICanBeNone`, which e.g. `Maybe<T>` does.

There's no difference between using `None._` vs `new None()` vs `default(None)` in terms of equality. Every 
one of those 3 operations return the same value.

### Maybe

`Maybe<T>` represents the possibility for the presence of a value, just like `null` does for reference 
types. A `Maybe<T>` can be one of two possibilities: `Some<T>`, or `None`, and either one is implicitly 
convertible to `Maybe<T>`.

#### Usage

```C#
public interface IProductRepository
{
    Maybe<Product> Product(ProductId id);
}

public class ProductService 
{
    private readonly IProductRepository _productRepository;

    public Maybe<ProductId[]> GetRelatedProducts(ProductId id)
    {
        return _productRepository.Product(productId)
            .Map(product => product.RelatedProducts);
    }
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
