using AnimalWorld.Data.Models.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalFamilyService : IBaseService<AnimalFamilyData>
    {
        List<AnimalFamilyData> GetRandomAnimals();
    }
}
