using AnimalWorld.Core.Services.Animals;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Core.Services.Users;
using AnimalWorld.Core.Services.Zoos;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalWorld.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAnimalWorldCore(this IServiceCollection services)
        {
            services.AddScoped<IAnimalFamilyService, AnimalFamilyService>();
            services.AddScoped<IAnimalSpeciesService, AnimalSpeciesService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IZooService, ZooService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IAnimalGalleryService, AnimalGalleryService>();
            services.AddScoped<ITicketService, TicketService>();
            return services;
        }
    }
}
