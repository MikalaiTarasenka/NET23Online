using AnimalWorld.Core.Dtos.Users;
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

        public ResponseDto Create(ZooData zooData)
        {
            if (_zooRepository.GetByName(zooData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой зоопарк существует"
                };
            }

            var user = _authService.GetUser();
            zooData.Creator = user;
            zooData.CreatorId = user.Id;
            _zooRepository.Create(zooData);
            return new ResponseDto { Success = true };
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ZooData Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ZooData> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ZooData model)
        {
            throw new NotImplementedException();
        }
    }
}
