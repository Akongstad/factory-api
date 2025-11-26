using System.ComponentModel.DataAnnotations;

namespace FactoryApi.Data.Model;

public record StateEvent
{
    [Key] public long Id { get; init; }

    [Required] required public long EquipmentId { get; init; }

    [Required] required public EquipmentStatus Status { get; init; } // 

    [Required, MinLength(2), MaxLength(5)] required public string WorkerShortName { get; init; } // Worker ID
    
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    public long? JobId { get; set; } // Link to order?
}