using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public interface IDisciplineService
    {
        Task<List<Discipline>> GetDisciplinesAsync(DisciplineFilter filter);
    }
}
