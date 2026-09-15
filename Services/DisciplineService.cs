using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services
{
    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContext _studentDbContext;

        public DisciplineService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public async Task<List<Discipline>> GetDisciplinesAsync(DisciplineFilter filter)
        {
            IQueryable<Discipline> query = _studentDbContext.Disciplines.AsNoTracking();

            if (filter.Name != "")
            {
                query = query.Where(Discipline => Discipline.Name == filter.Name);
            }


            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(Discipline => Discipline.IsDeleted == filter.IsDeleted);
            }

            return await query.OrderBy(Discipline => Discipline.Name).ToListAsync();
        }
    }
}
