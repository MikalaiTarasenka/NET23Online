using AnimalWorld.Core.Services.Animals;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorld.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAnimalWorldCore(this IServiceCollection services)
        {
            services.AddScoped<IAnimalFamilyService, AnimalFamilyService>();
            services.AddScoped<IAnimalSpeciesService, AnimalSpeciesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            return services;
        }
    }
}
