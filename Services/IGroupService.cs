using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;

namespace ProjectPractice.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroupsAsync(GroupFilter filter);
    
        Task<int?> CreateGroupAsync(CreateGroupRequest request);

        Task<bool> UpdateGroupAsync(UpdateGroupRequest request);

        Task<bool> DeleteGroupAsync(DeleteGroupRequest request);
    }
}
