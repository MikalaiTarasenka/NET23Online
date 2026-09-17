using AnimalWorld.Data.Dtos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Zoos
{
    internal class ZooRepository : NamedBaseRepository<ZooData>, IZooRepository
    {
        public ZooRepository(WebContext context) : base(context) { }

        public async Task BindAnimalSpecies(ZooData zooData, List<int> idsToAdd, List<int> idsToRemove)
        {
            if (!idsToAdd.Any() && !idsToRemove.Any())
            {
                return;
            }
            
            if (idsToAdd.Any())
            {
                var animalSpecies = await _context.AnimalSpecies.Where(animalSpecies => idsToAdd.Contains(animalSpecies.Id)).ToListAsync();
                zooData.AnimalSpecies.AddRange(animalSpecies);
            }

            if (idsToRemove.Any())
            {
                var animalSpecies = zooData.AnimalSpecies.Where(animalSpecies => idsToRemove.Contains(animalSpecies.Id)).ToList();
                foreach (var animal in animalSpecies)
                {
                    zooData.AnimalSpecies.Remove(animal);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ZooAnimalFamilyDto>> GetAnimalFamiliesByZooIds(List<int> ids)
        {
            var families = await _context.Set<ZooAnimalFamilyDto>()
            .FromSqlRaw(@"
                SELECT DISTINCT z.id AS zoo_id, f.name AS animal_family_name
                FROM zoos z
                JOIN zoo_species_bindings b ON z.id = b.zoos_id
                JOIN animal_species s ON b.animal_species_id = s.id
                JOIN animal_families f ON s.animal_family_id = f.id
                WHERE z.id = ANY({0})", ids)
            .ToListAsync();
            return families;
        }

        public async Task<ZooData> GetWithAnimals(int id)
        {
            var zoo = await _dbSet.Include(z => z.AnimalSpecies).FirstAsync(z => z.Id == id);
            return zoo;
        }

        public async Task<List<ZooData>> GetZoos(int page, int count)
        {
            return await _dbSet
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetZoosCount()
        {
            return await _dbSet
                .CountAsync();
        }
    }
}
