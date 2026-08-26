using AnimalWorld.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorld.Core.Services.Interfaces.Users
{
    public interface IUserProfileService
    {
        UserData Get();

        void Update(UserData userData);
    }
}
