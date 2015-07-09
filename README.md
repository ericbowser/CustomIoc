# CustomIoc
An custom IOC:

This IOC container will be capable of registering types as well as resolving
them in two different ways. Either use generic types to have the types pulled
off or pass in type objects. Each of these methods takes a Life Cycle to
determine weather the instance returned will be a new instance or referened.

This project has an MVC front-end that will use this container to inject 
dependencies through constructor injection.
