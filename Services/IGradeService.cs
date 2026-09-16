using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;

namespace ProjectPractice.Services
{
    public interface IGradeService
    {
        Task<List<AverageGradeDto>> GetStudentGradesAsync(
         StudentGradesFilter filter);

        Task<AverageGradeDto?> GetGroupDisciplineAsync(GroupDisciplineFilter filter);

        Task<List<AverageGradeDto>> GetCourseAverageAsync(CourseAverageFilter filter);

        Task<int?> CreateGradeAsync(CreateGradeRequest request);

        Task<bool> UpdateGradeAsync(UpdateGradeRequest request);

        Task<bool> DeleteGradeAsync(DeleteGradeRequest request);

        Task<List<String>> GetStudentDebtsAsync(StudentDebtFilter filter);
    }

}
