using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;

namespace AnimalWorld.Core.Services.Zoos
{
    internal class ZooService : IZooService
    {
        private IZooRepository _zooRepository;
        private IAuthService _authService;

        public ZooService(IZooRepository zooRepository, IAuthService authService)
        {
            _zooRepository = zooRepository;
            _authService = authService;
        }

        public void Create(ZooData zooData)
        {
            if (_zooRepository.GetByName(zooData.Name) != null)
            {
                return;
            }

            var user = _authService.GetUser();
            zooData.Creator = user;
            zooData.CreatorId = user.Id;
            _zooRepository.Create(zooData);
        }
    }
}
