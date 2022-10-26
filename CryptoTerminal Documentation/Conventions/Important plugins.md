We use MediatR library in our project. Controllers just send queries, Handlers catches them.
We also use FluentValidation, which replaces default validation system.

- **MediatR**. Sends queries from **Controller** to **Handler**. We use specific implementations of **IRequest** (**IRequestBase**) and **IRequestHandler** (**IRequestHandlerBase**), which use **Response** class.

- **FluentValidation**. Alternative validation method. Validation implementations stores near the model which to validate.

- **SignalR**. Hubs for notifications and realtime responses. We use custom **SubscriberHub** implementations.

- **Binance.NET**. Dotnet implementation of Binance Rest API.

- **Serilog**. Alternative logging.

- EntityFramework Core. SQL queries wrapping.

- Swashbuckle (Swagger).