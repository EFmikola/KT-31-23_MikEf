using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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


        public async Task<int?> CreateStudentAsync(CreateStudentRequest request)
        {
            var groupExists = await _studentDbContext.Groups
                .AnyAsync(group =>
                    group.GroupId == request.GroupId && !group.IsDeleted);

            if (!groupExists)
            {
                return null;
            }

            var student = new Student
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                GroupId = request.GroupId,
                IsDeleted = false
            };

            _studentDbContext.Students.Add(student);

            await _studentDbContext.SaveChangesAsync();

            return student.StudentId;
        }

        public async Task<bool> UpdateStudentAsync(UpdateStudentRequest request)
        {

            var student = await _studentDbContext.Students
                .SingleOrDefaultAsync(student =>
                    student.StudentId == request.StudentId);

            if (student is null)
            {
                return false;
            }


            var groupExists = await _studentDbContext.Groups
                .AnyAsync(group =>
                    group.GroupId == request.GroupId && !group.IsDeleted);

            if (!groupExists)
            {
                return false;
            }


            student.FirstName = request.FirstName.Trim();
            student.LastName = request.LastName.Trim();
            student.GroupId = request.GroupId;



            await _studentDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudentAsync(DeleteStudentRequest request)
        {
            var student = await _studentDbContext.Students
                .SingleOrDefaultAsync(student =>
                    student.StudentId == request.StudentId);

            if (student is null)
            {
                return false;
            }

            student.IsDeleted = true;


            await _studentDbContext.SaveChangesAsync();

            return true;
        }
    }
}
