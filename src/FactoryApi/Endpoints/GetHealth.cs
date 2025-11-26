namespace FactoryApi.Endpoints;

public static class GetHealth
{
    public static void MapEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", Handler)
            .WithName("Health");
    }

    private static IResult Handler ()
    {
        return TypedResults.Ok(new { status = "Healthy :)" });
    }
}