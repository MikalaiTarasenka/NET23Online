using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Animals
{
    internal class AnimalFamilyRepository : NamedBaseRepository<AnimalFamilyData>, IAnimalFamilyRepository
    {
        public const int START_PAGE_COUNT_ANIMAL_FAMILIES = 3;

        public AnimalFamilyRepository(WebContext context) : base(context) { }

        public async Task<List<AnimalFamilyData>> GetRandomElements()
        {
            return await _dbSet
                .OrderBy(p => EF.Functions.Random())
                .Take(START_PAGE_COUNT_ANIMAL_FAMILIES)
                .ToListAsync();
        }
    }
}
