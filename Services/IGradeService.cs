using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Dtos;

namespace ProjectPractice.Services
{
    public interface IGradeService
    {
        Task<List<AverageGradeDto>> GetStudentGradesAsync(
         StudentGradesFilter filter);

        Task<AverageGradeDto?> GetGroupDisciplineAsync(GroupDisciplineFilter filter);

        Task<List<AverageGradeDto>> GetCourseAverageAsync(CourseAverageFilter filter);
    }

}
