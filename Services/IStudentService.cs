using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync(StudentFilter filter);
    }
}
