# Introduction

This project intends to tackle a common scenario where the identity in the WebApi 2 is not implemented using Entity Framework. 
In the current case I've implemented a Mongo Repository using OAuth. 

# Considerations

The project will evolve from a basic authentication to more complex scenarios. It will also provide a small API implementation to show how users might be created but it will not a restriction. 
There are several ways to implement it but I prefer centric-solutions where an user is only created through an unique method instead of accessing the repository for a method and the User Manager for a different approach.

# Dependencies

I mention the most important external dependencies. I use those packages because I feel comfortable implementing them but of course they can be replaced for a different package.

- NUnit 
- NInject
- NInject.Extensions.ChildKernel

I've used .Net Framework 4.5.2 because many new packages don't compile with versions lower than 4.5.x. 

# Development Environment

Due to I don't use Visual Studio to reach a largest audience (outside of US sometimes the price of a license is prohibitive) I have to include packages for the targets as external references. For developers using Visual Studio those packages are installed with Visual Studio.

# Design decisions

## Users

An user of the system is represented by the IUser interface which is the source of User entities in Microsoft.AspNet.Identity. Most of us have been used to the fact of using the derived class of the Entity Framework. The fields are intrinsecally as extensible as you want except for username, password and the id. 
The identity is the default identity using a string which in MongoDB represents the string manifestation of an ObjectID. I use the Mongo _id field for serialization and keep Id as the representation of the ObjectID because it's very useful.
Even when I could use the identity user to expose it to the controller I don't like the idea because it can contain a lot of information I don't want to compromise at all. For that reason I preferred to implement a Controller User that only exposes information I want to.

## Users Roles

At the time being the roles are not implemented. Only the authorization (plain) for an user sending username and password. It only represents how to log in and not how to extend it (for now!)

# Mongo Configuration

The current mongodb configuration is very simple and it can be extended to support more features. It only requires Host, Port, SSL use and Db Name. Collections are hard-coded.

