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

        public void AddAnimalSpecies(int zooId, List<int> animalSpeciesIds)
        {
            var zoo = _dbSet
                .Include(animal => animal.AnimalSpecies)
                .First(zoo => zoo.Id == zooId);
            var existsAnimalSpeciesIds = zoo.AnimalSpecies.Select(a => a.Id);
            var animalSpeciesToAdd = animalSpeciesIds
                .Except(existsAnimalSpeciesIds)
                .ToList();
            if (!animalSpeciesToAdd.Any())
            {
                return;
            }

            var animalSpecies = _context.AnimalSpecies
                .Where(animalSpecies => animalSpeciesToAdd.Contains(animalSpecies.Id))
                .ToList();
            zoo.AnimalSpecies.AddRange(animalSpecies);
            _context.SaveChanges();
        }

        public List<ZooAnimalFamilyDto> GetAnimalFamiliesByZooIds(List<int> ids)
        {
            var sql = @$"SELECT [BZAAS].ZooId, [AF].AnimalFamilyName
            FROM 
                BindZooAndAnimalSpecies [BZAAS]
            JOIN 
                AnimalSpecies [AS] ON [AS].Id = [BZAAS].AnimalSpeciesId
            JOIN 
                AnimalFamilies [AF] ON [AF].Id = [AS].AnimalFamilyId
            WHERE 
                [BZAAS].ZooDataId IN ({string.Join(",", ids)})";
            return _context.Database
                .SqlQueryRaw<ZooAnimalFamilyDto>(sql)
                .ToList();
        }

        public ZooData GetWithAnimals(int id)
        {
            var zoo = _dbSet.Include(z => z.AnimalSpecies).First(z => z.Id == id);
            return zoo;
        }

        public List<ZooData> GetZoos(int page, int count)
        {
            return _dbSet
                .Skip((page - 1) * count)
                .Take(count)
                .ToList();
        }

        public int GetZoosCount()
        {
            return _dbSet
                .Count();
        }
    }
}
