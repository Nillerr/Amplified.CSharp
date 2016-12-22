# Amplified.CSharp

Provides a set of monadic types for use with C#.

 - `None`
 - `Some<T>`
 - `Maybe<T>`
 - `Either<TLeft, TRight>`
 - `Try<T>`

These types provide null safety and functional chaining, to provider a functional-like programming 
style in C#. All the monads are structs, which enforces their non-`null` behaviour.

## Null

The monads does not accept `null` values. Creating an instance of one of the monads, by passing 
`null` as the value parameter, will throw an `ArgumentNullException`.

## Type Token

 - `Type<T>`
