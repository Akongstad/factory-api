# factory-state-management: State management and Visualisation project

factory-state-management exposes api endpoints to store and retrieve state update events for factory equipment.

## MVP Solution Components

- _FactoryAPI_: C# MinimalApi project
- _Prostgres_: Persistence for state events.
- _Grafana_: Visualization of current machine states and of states of the equipment.

### Architcture

### Packages

- Scalar.AspNetCore
- Microsoft.EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Design
- Microsoft.AspNetCore.Mvc.Testing
- FluentAssertions

## Quickstart

### Prereqs

```´bash
dotnet dev-certs https
dotnet dev-certs https --trust 
```

