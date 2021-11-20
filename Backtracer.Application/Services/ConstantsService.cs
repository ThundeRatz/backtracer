using Backtracer.Application.Exceptions;
using Backtracer.Application.Model;
using Backtracer.Persistence;
using Backtracer.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backtracer.Application.Services;

public interface IConstantsService {
    Task<IReadOnlyList<ConstantGroup>> GetConstantGroups();
    Task<ConstantGroup> CreateConstantGroup(ConstantGroup group, IDictionary<int, double> constants);
    Task<IReadOnlyList<ConstantType>> GetConstantTypes();
    Task<ConstantGroupWithValues?> GetConstantGroupByName(string name);
}

public class ConstantsService : IConstantsService {
    private readonly DataContext _context;

    public ConstantsService(DataContext context) {
        _context = context;
    }

    public async Task<IReadOnlyList<ConstantGroup>> GetConstantGroups() {
        var entities = await _context.ConstantGroups
            .ToListAsync();

        return entities.Select(e => new ConstantGroup {
            Id = e.Id,
            Name = e.Name,
            Created = e.Created,
        }).ToList();
    }

    public async Task<ConstantGroup> CreateConstantGroup(ConstantGroup group, IDictionary<int, double> constants) {
        var createdGroup = new ConstantGroupEntity {
            Name = group.Name,
        };

        await _context.ConstantGroups.AddAsync(createdGroup);

        var ctes = constants.Select(c => new ConstantEntity {
            ConstantTypeId = c.Key,
            Value = c.Value,
            ConstantGroup = createdGroup,
        });

        await _context.Constants.AddRangeAsync(ctes);

        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateException e) {
            if (e.InnerException is PostgresException pe) {
                if (pe.ConstraintName?.Contains("ConstantTypes") ?? false) {
                    throw new ConstantTypeNotFoundException();
                }

                if (pe.ConstraintName?.Contains("ConstantGroup") ?? false) {
                    throw new ConstantGroupUniqueException();
                }
            }

            throw new ConstantsServiceException();
        }

        return new ConstantGroup {
            Id = createdGroup.Id,
            Name = createdGroup.Name,
            Created = createdGroup.Created,
        };
    }

    public async Task<IReadOnlyList<ConstantType>> GetConstantTypes() {
        var entities = await _context.ConstantTypes
            .ToListAsync();

        return entities.Select(e => new ConstantType {
            Id = e.Id,
            Description = e.Description,
        }).ToList();
    }

    public async Task<ConstantGroupWithValues?> GetConstantGroupByName(string name) {
        var entity = await _context.ConstantGroups
            .Include(c => c.Constants)
            .FirstOrDefaultAsync(c => c.Name == name);

        if (entity == null) {
            return null;
        }

        return new ConstantGroupWithValues {
            Id = entity.Id,
            Name = entity.Name,
            Created = entity.Created,
            Constants = entity.Constants.ToDictionary(c => c.ConstantTypeId, c => c.Value),
        };
    }
}
