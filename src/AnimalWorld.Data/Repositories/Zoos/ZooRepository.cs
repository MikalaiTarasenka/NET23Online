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
            var sql = @$"SELECT [BZAAS].ZooId, [AF].AnimalFamilyName
            FROM 
                zoo_species_bindings [BZAAS]
            JOIN 
                animal_species [AS] ON [AS].Id = [BZAAS].AnimalSpeciesId
            JOIN 
                animal_families [AF] ON [AF].Id = [AS].AnimalFamilyId
            WHERE 
                [BZAAS].ZooDataId IN ({string.Join(",", ids)})";
            return await _context.Database
                .SqlQueryRaw<ZooAnimalFamilyDto>(sql)
                .ToListAsync();
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
