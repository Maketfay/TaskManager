using Infrastructure.Entity;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Services
{
    public class WorkSpaceService: IWorkSpaceService
    {
        private readonly IUserRepository _userRepository;

        private readonly IWorkSpaceRepository _workSpaceRepository;

        private readonly IWorkSpaceUserRepository _workSpaceUserRepository;

        public WorkSpaceService(IUserRepository userRepository, IWorkSpaceRepository workSpaceRepository, IWorkSpaceUserRepository workSpaceUserRepository)
        {
            _userRepository = userRepository;
            _workSpaceRepository = workSpaceRepository;
            _workSpaceUserRepository = workSpaceUserRepository;
        }

        public async Task<IWorkSpace?> CreateAsync(Guid userId, string name) 
        {
            var user = await _userRepository.ReadAsync(userId);
            if(user is null)
                return null;

            var workSpace = await _workSpaceRepository.CreateAsync(name);

            await _workSpaceUserRepository.CreateAsync(user, workSpace);

            return workSpace;
        }
    }
}
