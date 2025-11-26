using FactoryApi.Data;
using FactoryApi.Data.Model;
using FactoryApi.Endpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FactoryApi.Tests;

public class PostStateTests
{
    private StateDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<StateDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb").Options;
        return new StateDbContext(options);
    }

    [Fact]
    public async Task PostState_ShouldSaveToDatabase_AndReturnCreated()
    {

        using var db = CreateDbContext();
        
        var request = new PostState.Request(
            EquipmentId: 123,
            Status: EquipmentStatus.Green,
            WorkerShortName: "kong",
            JobId: 456
        );

        // Act
        var result = await PostState.Handler(request, db);

        // Assert
        var createdResult = result.Should().BeOfType<Created<PostState.Response>>().Subject;
        createdResult.Value.Should().NotBeNull();
        createdResult.Value!.EquipmentId.Should().Be(123);
        createdResult.Value.Status.Should().Be("Green");

        var savedEvent = await db.StateEvents.FirstOrDefaultAsync();
        savedEvent.Should().NotBeNull();
        savedEvent!.EquipmentId.Should().Be(123);
        savedEvent.Status.Should().Be(EquipmentStatus.Green);
        savedEvent.WorkerShortName.Should().Be("kong");
    }
}
