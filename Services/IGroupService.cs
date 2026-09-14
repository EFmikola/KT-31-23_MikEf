using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroupsAsync(GroupFilter filter);
    }
}
