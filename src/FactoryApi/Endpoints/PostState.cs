using System.ComponentModel.DataAnnotations;
using FactoryApi.Data;
using FactoryApi.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApi.Endpoints;

public static class PostState
{
    private record Request(
        [Required] long EquipmentId,
        [Required] EquipmentStatus Status,
        [Required, MaxLength(5), MinLength(2)] string WorkerShortName,
        long? JobId
    );

    private record Response(
        long Id,
        long EquipmentId,
        string Status,
        DateTimeOffset Timestamp
    );
    
    public static void MapEndpoint(IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", Handler)
            .WithName("PostState")
            .WithSummary("Create a new equipment state event")
            .WithDescription("Creates a new equipment state event in the system.")
            .Produces<Response>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handler([FromBody] Request request, [FromServices] StateDbContext db)
    {
        var stateEvent = new StateEvent()
        {
            EquipmentId = request.EquipmentId,
            Status = request.Status,
            WorkerShortName = request.WorkerShortName,
            JobId = request.JobId
        };
        db.StateEvents.Add(stateEvent);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/states/{stateEvent.Id}", new Response(
            Id: stateEvent.Id,
            EquipmentId: stateEvent.EquipmentId,
            Status: stateEvent.Status.ToString(),
            Timestamp: stateEvent.Timestamp
        ));
    }
}