using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;

namespace ProjectPractice.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync(StudentFilter filter);

        Task<int?> CreateStudentAsync(CreateStudentRequest request);

        Task<bool> UpdateStudentAsync(UpdateStudentRequest request);

        Task<bool> DeleteStudentAsync(DeleteStudentRequest request);
    }
}
