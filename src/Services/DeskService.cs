using Infrastructure.Entity;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Services
{
    public class DeskService : IDeskService
    {
        private readonly IDeskRepository _deskRepository;

        private readonly IWorkSpaceDeskRepository _workSpaceDeskRepository;
        public DeskService(IDeskRepository deskRepository, IWorkSpaceDeskRepository workSpaceDeskRepository)
        {
            _deskRepository = deskRepository;
            _workSpaceDeskRepository = workSpaceDeskRepository;
        }

        public async Task<IDesk?> CreateAsync(string name, IDeskVisibilityType deskVisibilityType, IUser user, IWorkSpace workSpace)
        {
            IDesk desk = default;
            await foreach (var deskInRepo in _workSpaceDeskRepository.ReadCollectionAsync(workSpace)) 
            {
                if (deskInRepo.Name.Equals(name))
                    desk = deskInRepo;
            }

            if (desk is null)
            {
                desk = await _deskRepository.InTransactionAsync(async () =>
                {
                    var desk = await _deskRepository.CreateAsync(name, deskVisibilityType, user);

                    await _workSpaceDeskRepository.CreateAsync(workSpace, desk);

                    return desk;
                });

                return desk;
            }

            return null;
        }
    }
}