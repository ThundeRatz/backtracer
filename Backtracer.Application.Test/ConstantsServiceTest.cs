using System;
using System.Data.Common;
using Backtracer.Application.Services;
using Backtracer.Persistence;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace Backtracer.Application.Test;

public class ConstantsServiceTest : ServiceTest, IDisposable {
    private readonly DbConnection _connection;

    public ConstantsServiceTest()
        : base(
            new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options) {
        _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection!;
    }

    [Fact]
    public async void GetConstantGroups_ReturnsList() {
        using var context = new DataContext(ContextOptions);
        var service = new ConstantsService(context);

        var items = await service.GetConstantGroups();

        items.Should().HaveCount(1);
    }

    [Theory]
    [InlineData("group1", true)]
    [InlineData("NA", false)]
    public async void GetSpecificGroup_ReturnsNullIfDontExist(string name, bool exists) {
        using var context = new DataContext(ContextOptions);
        var service = new ConstantsService(context);

        var group = await service.GetConstantGroupByName(name);

        if (exists) {
            group.Should().NotBeNull();
        } else {
            group.Should().BeNull();
        }
    }

    private static DbConnection CreateInMemoryDatabase() {
        var connection = new SqliteConnection("Filename=:memory:");

        connection.Open();

        return connection;
    }

    public void Dispose() {
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
