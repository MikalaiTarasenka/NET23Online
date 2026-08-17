using AnimalWorld.Data.Repositories.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Users;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using AnimalWorld.Data.Repositories.Users;
using AnimalWorld.Data.Repositories.Zoos;
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
            services.AddScoped<IAnimalSpeciesRepository, AnimalSpeciesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IZooRepository, ZooRepository>();
            return services;
        }
    }
}
