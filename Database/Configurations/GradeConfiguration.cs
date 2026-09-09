using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    // название таблицы в бд
    private const string TableName = "cd_grade";

    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable(TableName);

        // первичный ключ и автоинкремент
        builder.HasKey(p => p.GradeId)
               .HasName($"pk_{TableName}_grade_id");

        builder.Property(p => p.GradeId)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.GradeId)
               .HasColumnName("grade_id")
               .HasComment("Идентификатор оценки");

        // значение оценки
        builder.Property(p => p.Value)
               .IsRequired()
               .HasColumnName("n_value")
               .HasColumnType(ColumnType.Int)
               .HasComment("Оценка");

        // внешний ключ на студента
        builder.Property(p => p.StudentId)
               .IsRequired()
               .HasColumnName("f_student_id")
               .HasColumnType(ColumnType.Int)
               .HasComment("Идентификатор студента");

        // внешний ключ на дисциплину
        builder.Property(p => p.DisciplineId)
               .IsRequired()
               .HasColumnName("f_discipline_id")
               .HasColumnType(ColumnType.Int)
               .HasComment("Идентификатор дисциплины");

        // связь со студентом
        builder.HasOne(p => p.Student)
               .WithMany(t => t.Grades)
               .HasForeignKey(p => p.StudentId)
               .HasConstraintName("fk_f_student_id")
               .OnDelete(DeleteBehavior.Cascade);

        // связь с дисциплиной
        builder.HasOne(p => p.Discipline)
               .WithMany(t => t.Grades)
               .HasForeignKey(p => p.DisciplineId)
               .HasConstraintName("fk_f_discipline_id")
               .OnDelete(DeleteBehavior.Restrict);

        // индексы для ускорения поиска
        builder.HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");
        builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");
    }
}

