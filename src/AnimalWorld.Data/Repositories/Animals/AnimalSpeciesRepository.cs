using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace AnimalWorld.Data.Repositories.Animals
{
    internal class AnimalSpeciesRepository : NamedBaseRepository<AnimalSpeciesData>, IAnimalSpeciesRepository
    {
        public const int START_PAGE_COUNT_ANIMAL_SPECIES = 3;

        public AnimalSpeciesRepository(WebContext context) : base(context) { }

        public List<AnimalSpeciesData> GetRandomElements()
        {
            return _dbSet
                .Include(p => p.Zoos)
                .OrderBy(p => EF.Functions.Random())
                .Take(START_PAGE_COUNT_ANIMAL_SPECIES)
                .ToList();
        }

        public List<string> GetAllAnimalSpeciesNames()
        {
            var sql = @$"SELECT Name
                         FROM animal_species";
            return _context.Database
                .SqlQueryRaw<string>(sql)
                .ToList();
        }

        public List<AnimalSpeciesData> GetAllWithFamily(string searchCategory, string searchQuery)
        {
            var dataSource = _dbSet
                .Include(s => s.AnimalFamily)
                .AsQueryable();
            if (string.IsNullOrEmpty(searchCategory) || string.IsNullOrEmpty(searchQuery))
            {
                return dataSource.ToList();
            }

            var queryValue = searchQuery.ToLower();
            var parameter = Expression.Parameter(typeof(AnimalSpeciesData), "animal");
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var constQuery = Expression.Constant(queryValue, typeof(string));
            Expression finalExpression;
            if (searchCategory == "Species")
            {
                var field = Expression.Property(parameter, nameof(AnimalSpeciesData.Name));
                finalExpression = BuildContainsExpression(field, toLowerMethod, containsMethod, constQuery);
            }
            else if (searchCategory == "Family")
            {
                var familyProp = Expression.Property(parameter, nameof(AnimalSpeciesData.AnimalFamily));
                var familyNameProp = Expression.Property(familyProp, "Name");
                finalExpression = BuildContainsExpression(familyNameProp, toLowerMethod, containsMethod, constQuery);
            }
            else if (searchCategory == "Range")
            {
                var field = Expression.Property(parameter, nameof(AnimalSpeciesData.NativeRange));
                finalExpression = BuildContainsExpression(field, toLowerMethod, containsMethod, constQuery);
            }
            else
            {
                var speciesField = Expression.Property(parameter, nameof(AnimalSpeciesData.Name));
                var familyProp = Expression.Property(parameter, nameof(AnimalSpeciesData.AnimalFamily));
                var familyNameProp = Expression.Property(familyProp, "Name");
                var rangeField = Expression.Property(parameter, nameof(AnimalSpeciesData.NativeRange));

                var speciesExp = BuildContainsExpression(speciesField, toLowerMethod, containsMethod, constQuery);
                var familyExp = BuildContainsExpression(familyNameProp, toLowerMethod, containsMethod, constQuery);
                var rangeExp = BuildContainsExpression(rangeField, toLowerMethod, containsMethod, constQuery);
                finalExpression = Expression.OrElse(Expression.OrElse(speciesExp, familyExp), rangeExp);
            }

            if (finalExpression != null)
            {
                var lambda = Expression.Lambda<Func<AnimalSpeciesData, bool>>(finalExpression, parameter);
                dataSource = dataSource.Where(lambda);
            }

            return dataSource.ToList();
        }

        private Expression BuildContainsExpression(Expression propertyField, MethodInfo toLowerMethod, MethodInfo containsMethod, ConstantExpression constQuery)
        {
            var toLowerCall = Expression.Call(propertyField, toLowerMethod);
            return Expression.Call(toLowerCall, containsMethod, constQuery);
        }
    }
}
