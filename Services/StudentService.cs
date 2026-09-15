using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _studentDbContext;

        public StudentService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<List<Student>> GetStudentsAsync(StudentFilter filter)
        {
            IQueryable<Student> query = _studentDbContext.Students.AsNoTracking();

            if (filter.FirstName != "")
            {
                query = query.Where(Student => Student.FirstName == filter.FirstName);
            }

            if (filter.LastName != "")
            {
                query = query.Where(Student => Student.LastName == filter.LastName);
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(Student => Student.IsDeleted == filter.IsDeleted);
            }

            if (filter.GroupId.HasValue)
            {
                query = query.Where(Student => Student.GroupId == filter.GroupId);
            }

            return await query.OrderBy(Student => Student.FirstName).ToListAsync();
        }
    }
}
