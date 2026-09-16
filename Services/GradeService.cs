using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPractice.Database;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Requests;

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
                .Where(grade =>
                    grade.StudentId == filter.StudentId &&
                    !grade.Discipline!.IsDeleted)
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
                    grade.DisciplineId == filter.DisciplineId &&
                    !grade.Discipline!.IsDeleted)
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
                .Where(grade =>
                    grade.Student!.Group!.Course == filter.Course &&
                    !grade.Discipline!.IsDeleted)
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






        public async Task<int?> CreateGradeAsync(CreateGradeRequest request)
        {
                   // public int StudentId { get; set; }

       // public int DisciplineId { get; set; }

            var studentExists = await _studentDbContext.Students
                .AnyAsync(student =>
                    student.StudentId == request.StudentId && !student.IsDeleted);

            if (!studentExists)
            {
                return null;
            }


            var disciplineExists = await _studentDbContext.Disciplines
                .AnyAsync(discipline =>
                    discipline.DisciplineId == request.DisciplineId && !discipline.IsDeleted);

            if (!disciplineExists)
            {
                return null;
            }

            var grade = new Grade
            {
                //public int Value { get; set; }

                //    public int StudentId { get; set; }

                //    public int DisciplineId { get; set; }
                Value = request.Value,
                StudentId = request.StudentId,
                DisciplineId = request.DisciplineId,
            };

            _studentDbContext.Grades.Add(grade);

            await _studentDbContext.SaveChangesAsync();

            return grade.GradeId;
        }

        public async Task<bool> UpdateGradeAsync(UpdateGradeRequest request)
        {

            var grade = await _studentDbContext.Grades
                .SingleOrDefaultAsync(grade =>
                    grade.GradeId == request.GradeId);

            if (grade is null)
            {
                return false;
            }


            var studentExists = await _studentDbContext.Students
                .AnyAsync(student =>
                    student.StudentId == request.StudentId && !student.IsDeleted);

            if (!studentExists)
            {
                return false;
            }


            var disciplineExists = await _studentDbContext.Disciplines
                .AnyAsync(discipline =>
                    discipline.DisciplineId == request.DisciplineId && !discipline.IsDeleted);

            if (!disciplineExists)
            {
                return false;
            }

            //public int Value { get; set; }

            //    public int StudentId { get; set; }

            //    public int DisciplineId { get; set; }
            grade.Value = request.Value;
            grade.DisciplineId = request.DisciplineId;
            grade.StudentId = request.StudentId;



            await _studentDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGradeAsync(DeleteGradeRequest request)
        {
            var grade = await _studentDbContext.Grades
                .SingleOrDefaultAsync(grade =>
                    grade.GradeId == request.GradeId);

            if (grade is null)
            {
                return false;
            }

            _studentDbContext.Remove(grade);


            await _studentDbContext.SaveChangesAsync();

            return true;
        }





        public async Task<List<String>> GetStudentDebtsAsync(StudentDebtFilter filter)
        {
             var debts = _studentDbContext.Grades.AsNoTracking()
               .Where(grade => grade.Value <= 2 &&
                    grade.Student!.LastName == filter.LastName &&
                    !grade.Student.IsDeleted &&
                    !grade.Discipline!.IsDeleted)
                .Select(grade => new StudentDebtDto
                {
                    LastName = grade.Student!.LastName,
                    DisciplineName = grade.Discipline!.Name
                })
                .Distinct().OrderBy(debt => debt.DisciplineName).Select(dto => dto.DisciplineName)
                .ToListAsync();

            return await debts;
        }
    }
}
