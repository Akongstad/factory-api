namespace FactoryApi.Endpoints;

public static class HealthEndpoints
{
    public static WebApplication MapHealthEndpoints(this WebApplication app)
    {
        var healthRoot = app.MapGroup("api/health")
            .WithTags("Health")
            .WithDescription("Api health endpoints");
        

        GetHealth.MapEndpoint(healthRoot);
        return app;
    }
    
}