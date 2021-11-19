using Backtracer.Persistence;
using Backtracer.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace Backtracer.Application.Test;

public class ServiceTest {
    protected ServiceTest(DbContextOptions<DataContext> contextOptions) {
        ContextOptions = contextOptions;

        Seed();
    }

    protected DbContextOptions<DataContext> ContextOptions { get; }

    private void Seed() {
        using var context = new DataContext(ContextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var type1 = new ConstantTypeEntity {
            Id = 1,
            Description = "test1",
        };

        var type2 = new ConstantTypeEntity {
            Id = 2,
            Description = "test2",
        };

        var type3 = new ConstantTypeEntity {
            Id = 3,
            Description = "test3",
        };

        var group = new ConstantGroupEntity {
            Id = 1,
            Name = "group1",
        };

        var constant1 = new ConstantEntity {
            ConstantTypeId = 1,
            ConstantGroupId = 1,
            Value = 1.0,
        };

        var constant2 = new ConstantEntity {
            ConstantTypeId = 2,
            ConstantGroupId = 1,
            Value = 2.0,
        };

        var constant3 = new ConstantEntity {
            ConstantTypeId = 3,
            ConstantGroupId = 1,
            Value = 3.0,
        };

        context.AddRange(type1, type2, type3, group, constant1, constant2, constant3);

        context.SaveChanges();
    }
}
