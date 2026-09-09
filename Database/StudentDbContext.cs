using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database.Configurations;
using ProjectPractice.Models;

namespace ProjectPractice.Database;

public class StudentDbContext : DbContext
{
    // таблицы в бд
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Grade> Grades { get; set; }

    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // подключаем конфигурации таблиц
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new SpecialityConfiguration());
        modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
        modelBuilder.ApplyConfiguration(new GradeConfiguration());
    }
}