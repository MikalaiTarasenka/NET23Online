using AnimalWorld.Data.Repositories.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalWorld.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAnimalWorldData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IAnimalFamilyRepository, AnimalFamilyRepository>();
            return services;
        }
    }
}
