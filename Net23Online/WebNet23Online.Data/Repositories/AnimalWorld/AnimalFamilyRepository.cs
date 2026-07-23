using WebNet23Online.Data.Models.AnimalWorld;
using WebNet23Online.Data.Repositories.Interfaces.AnimalWorld;

namespace WebNet23Online.Data.Repositories.AnimalWorld
{
    public class AnimalFamilyRepository : BaseRepository<AnimalFamilyData>, IAnimalFamilyRepository
    {
        public const int START_PAGE_COUNT_ANIMAL_FAMILIES = 3;

        public AnimalFamilyRepository(WebContext webContext) : base(webContext)
        {
        }

        public List<AnimalFamilyData> GetRandomElements()
        {
            return _dbSet.ToList().OrderBy(animal => Guid.NewGuid()).Take(START_PAGE_COUNT_ANIMAL_FAMILIES).ToList();
        }

        public AnimalFamilyData GetElementByName(string name)
        {
            return _dbSet.FirstOrDefault(animal => animal.AnimalFamilyName.ToLower() == name.ToLower());
        }
    }
}
