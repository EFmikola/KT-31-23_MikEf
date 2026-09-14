using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _studentDbContext;

        public GroupService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<List<Group>> GetGroupsAsync(GroupFilter filter)
        {
            IQueryable<Group> query = _studentDbContext.Groups.AsNoTracking();

            if (filter.SpecailityID.HasValue)
            {
                query = query.Where(group => group.SpecialityId == filter.SpecailityID);
            }

            if (filter.Course.HasValue)
            {
                query = query.Where(group => group.Course == filter.Course);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(group => group.IsDeleted == filter.IsDeleted);
            }

            return await query.OrderBy(group => group.Name).ToListAsync();
        }
    }
}
