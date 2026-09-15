using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPractice.Database;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _studentDbContext;

        public GradeService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<List<AverageGradeDto>> GetStudentGradesAsync(
            StudentGradesFilter filter)
        {
            return await _studentDbContext.Grades
                .AsNoTracking()
                .Where(grade => grade.StudentId == filter.StudentId)
                .GroupBy(grade => new
                {
                    grade.DisciplineId,
                    DisciplineName = grade.Discipline!.Name
                })
                .OrderBy(group => group.Key.DisciplineName)
                .Select(group => new AverageGradeDto
                {
                    DisciplineName = group.Key.DisciplineName,
                    AverageGrade = group.Average(grade => grade.Value)
                })
                .ToListAsync();
        }

        public async Task<AverageGradeDto?> GetGroupDisciplineAsync(
    GroupDisciplineFilter filter)
        {
            return await _studentDbContext.Grades
                .AsNoTracking()
                .Where(grade =>
                    grade.Student!.GroupId == filter.GroupId &&
                    grade.DisciplineId == filter.DisciplineId)
                .GroupBy(grade => new
                {
                    GroupName = grade.Student!.Group!.Name
                })
                .Select(group => new AverageGradeDto
                {
                    AverageGrade = group.Average(grade => grade.Value)
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<AverageGradeDto>> GetCourseAverageAsync(CourseAverageFilter filter)
        {
            return await _studentDbContext.Grades
                .AsNoTracking()
                .Where(grade => grade.Student!.Group!.Course == filter.Course)
                .GroupBy(grade => new
                {
                    grade.Student!.Group!.Course,
                    DisciplineName = grade.Discipline!.Name
                })
                .OrderBy(group => group.Key.DisciplineName)
                .Select(group => new AverageGradeDto
                {
                    DisciplineName = group.Key.DisciplineName,
                    AverageGrade = group.Average(grade => grade.Value)
                })
                .ToListAsync();
        }
    }
}
