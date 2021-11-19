using Backtracer.Application.Model;
using Backtracer.Persistence;
using Backtracer.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace Backtracer.Application.Services;

public interface IConstantsService {
    Task<IReadOnlyList<ConstantGroup>> GetConstantGroups();
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
