using FactoryApi.Data;
using FactoryApi.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FactoryApi.Endpoints;

public static class GetStates
{
    public static void MapEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/states", Handler)
            .WithName("GetStates")
            .WithSummary("Lists state events")
            .Produces<List<StateEvent>>(StatusCodes.Status200OK);
    }

    private static async Task<IResult> Handler([FromServices] StateDbContext db)
    {
        // TODO use DTO
        var states = await db.StateEvents.ToListAsync();

        return TypedResults.Ok(states);
    }
}