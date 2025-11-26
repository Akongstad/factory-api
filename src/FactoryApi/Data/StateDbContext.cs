using FactoryApi.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace FactoryApi.Data;

public class StateDbContext : DbContext
{
    public StateDbContext(DbContextOptions<StateDbContext> options) : base(options) { }

    public DbSet<StateEvent> Postures => Set<StateEvent>();
}