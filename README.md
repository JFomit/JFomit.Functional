# JFomit.Functional

This library provides support for functional types, namely:

*  Option&lt;T&gt; - wraps a value that might not exist in a safe manner;
*  Result&lt;T, E&gt; - wraps a value that might not exist due to some processing error;
*  OneOf&lt;T1, T2, T3&gt; - discriminated union on one of T1, T2 or T3.

# Building

Make shure to have .Net SDK installed, then clone the repo and build with `dotnet build` or or IDE of choice.