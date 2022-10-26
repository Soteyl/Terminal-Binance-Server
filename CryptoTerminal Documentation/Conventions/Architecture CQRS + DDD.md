We use CQRS + DDD (onion) architecture.

- **Controllers** responsible only for *transport*. They send data to **Handlers**.
- **Handlers** responsible only for *preparing request and response data*. They send data to **services**.
- **Services** responsible only for *business logic*. They send data to **repositories**.
- **Repositories** responsible only for *wrapping storage*. They send data to **contexts**.
- **Contexts** responsible for *storage*.

Every entity is independent. Controllers don't know about handlers and so on.

![[Main project structure.png]]

Our solution contains 5 projects:
- **Api** - Controllers and extensions.
- **Application** - Handlers (managers), services.
- **Domain** - Base rules, validation, business, data and transport models.
- **Infrastructure** - repositories.
- **Storage** - contexts.

![[Project architecture schema.png]]

**Api** and **Domain** are fully independent.
**Application** depends on **Infrastructure** and **Domain** *(repositories and models)*.
**Infrastructure** depends on **Storage** and **Domain** *(contexts and models).*
**Storage** depends on **Domain** *(models).*
