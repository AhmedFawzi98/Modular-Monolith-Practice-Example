# NPay

## About

NPay is a simple virtual payments app built as a modular monolith.

**How to start the solution?**
----------------

Start the infrastructure using [Docker](https://docs.docker.com/get-docker/):

```
docker-compose up -d
```

Start API located under Bootstrapper project:

```
cd src/Bootstrapper/NPay.Bootstrapper
dotnet run
```
### Credits to @devmentors – This repository is based on their original work.

#### master Branch:
- I’ve restructured the projects and refactored some internal details (schema separation for migration history, composition root for the Wallets module to follow Clean Architecture guidelines, etc.) to align with my personal style and understanding of modular monolith architecture.
- This version serves as a reference for my own learning and future use.

#### mediator-and-Masstransit Branch:
- This is a branch I added where I:
1. replaced custom in-house mediator implementations with the **MediatR** library for dispatching commands and queries to handlers
2. switched inter-module asynchronous communication to use **MassTransit** with underlying RabbitMQ transport 
3. switched in memory events dispatching to use MediatR instead of Channels
