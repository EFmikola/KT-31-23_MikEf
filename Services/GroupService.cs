using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;

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

        public async Task<int?> CreateGroupAsync(CreateGroupRequest request)
        {
            var specialityExists = await _studentDbContext.Specialities
                .AnyAsync(speciality =>
                    speciality.SpecialityId == request.SpecialityId);

            if (!specialityExists)
            {
                return null;
            }

            var group = new Group
            {
                Name = request.Name.Trim(),
                Course = request.Course,
                SpecialityId = request.SpecialityId,
                IsDeleted = false
            };

            _studentDbContext.Groups.Add(group);

            await _studentDbContext.SaveChangesAsync();

            return group.GroupId;
        }

        public async Task<bool> UpdateGroupAsync(UpdateGroupRequest request)
        {
            var group = await _studentDbContext.Groups
                .SingleOrDefaultAsync(group =>
                    group.GroupId == request.GroupId);

            if (group is null)
            {
                return false;
            }

            var specialityExists = await _studentDbContext.Specialities
                .AnyAsync(speciality =>
                    speciality.SpecialityId == request.SpecialityId);

            if (!specialityExists)
            {
                return false;
            }

            group.Name = request.Name.Trim();
            group.Course = request.Course;
            group.SpecialityId = request.SpecialityId;

            await _studentDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGroupAsync(DeleteGroupRequest request)
        {
            var group = await _studentDbContext.Groups
                .Include(group => group.Students)
                .SingleOrDefaultAsync(group =>
                    group.GroupId == request.GroupId);

            if (group is null)
            {
                return false;
            }

            group.IsDeleted = true;

            foreach (var student in group.Students)
            {
                student.IsDeleted = true;
            }

            await _studentDbContext.SaveChangesAsync();

            return true;
        }
    }
}
