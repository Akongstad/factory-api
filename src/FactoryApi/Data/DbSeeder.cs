using FactoryApi.Data.Model;

namespace FactoryApi.Data;

public static class DbSeeder
{
    /// <summary>
    /// Seed the database context if DB is empty
    /// </summary>
    /// <param name="context"></param>
    public static async Task SeedAsync(StateDbContext context)
    {
        if (context.StateEvents.Any())
        {
            return;
        }

        var events = new List<StateEvent>();
        var random = new Random();
        var workers = new[] { "KONG", "LARS", "TOM", "DJ" };
        
        // Generate data for the last 24 hours
        var startTime = DateTimeOffset.UtcNow.AddHours(-24);
        
        for (int equipmentId = 1; equipmentId <= 3; equipmentId++)
        {
            var currentTime = startTime;
            while (currentTime < DateTimeOffset.UtcNow)
            {
                // Random duration between 15 mins and 4 hours
                var duration = TimeSpan.FromMinutes(random.Next(15, 240));
                
                var status = (EquipmentStatus)random.Next(0, 3);
                
                events.Add(new StateEvent
                {
                    EquipmentId = equipmentId,
                    Status = status,
                    WorkerShortName = workers[random.Next(workers.Length)],
                    Timestamp = currentTime,
                    JobId = random.Next(1000, 9999)
                });

                currentTime = currentTime.Add(duration);
            }
        }

        await context.StateEvents.AddRangeAsync(events);
        await context.SaveChangesAsync();
    }
}
