using System.ComponentModel.DataAnnotations;
using FactoryApi.Data;
using FactoryApi.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApi.Endpoints;

public static class PostState
{
    public record Request(
        [Required] long EquipmentId,
        [Required] EquipmentStatus Status,
        [Required, MinLength(2), MaxLength(5)] string WorkerShortName,
        long? JobId
    );

    public record Response(
        long Id,
        long EquipmentId,
        string Status,
        DateTimeOffset Timestamp
    );

    public static void MapEndpoint(IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", Handler)
            .ProducesValidationProblem()
            .WithName("PostState")
            .WithSummary("Create a new equipment state event")
            .Produces<Response>();
    }

    public static async Task<IResult> Handler(Request request, [FromServices] StateDbContext db)
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