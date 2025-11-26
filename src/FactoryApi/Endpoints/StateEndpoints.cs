namespace FactoryApi.Endpoints;

public static class StateEndpoints
{
    public static WebApplication MapStateEndpoints(this WebApplication app)
    {
        var statesRoot = app.MapGroup("api/state")
            .WithTags("State")
            .WithDescription("GET and POST equipment state events");
        
        GetStates.MapEndpoint(statesRoot);
        PostState.MapEndpoint(statesRoot);
        
        return app;
    }
}