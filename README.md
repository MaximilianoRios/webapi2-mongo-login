## Introduction

This project intends to tackle a common scenario where the identity in the WebApi 2 is not implemented using Entity Framework. 
In the current case I've implemented a Mongo Repository using OAuth. 

## Considerations

The project will evolve from a basic authentication to more complex scenarios. It will also provide a small API implementation to show how users might be created but it will not a restriction. 
There are several ways to implement it but I prefer centric-solutions where an user is only created through an unique method instead of accessing the repository for a method and the User Manager for a different approach.

## Dependencies

I mention the most important external dependencies. I use those packages because I feel comfortable implementing them but of course they can be replaced for a different package.

- NUnit 
- NInject
- NInject.Extensions.ChildKernel

I've used .Net Framework 4.5.2 because many new packages don't compile with versions lower than 4.5.x. 

## Development Environment

Due to I don't use Visual Studio to reach a largest audience (outside of US sometimes the price of a license is prohibitive) I have to include packages for the targets as external references. For developers using Visual Studio those packages are installed with Visual Studio.

