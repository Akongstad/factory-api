using System.ComponentModel.DataAnnotations;

namespace FactoryApi.Data.Model;

public record StateEvent
{
    [Key] public long Id { get; init; }

    [Required] public long EquipmentId { get; init; }

    [Required] public EquipmentStatus Status { get; init; } // 

    [Required, MaxLength(5), MinLength(2)] public string WorkerShortName { get; init; } // Worker ID
    
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    public long? JobId { get; set; } // Link to order?
}