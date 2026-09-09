using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    // название таблицы в бд
    private const string TableName = "cd_student";

    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(TableName);

        // первичный ключ и автоинкремент
        builder.HasKey(p => p.StudentId)
               .HasName($"pk_{TableName}_student_id");

        builder.Property(p => p.StudentId)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.StudentId)
               .HasColumnName("student_id")
               .HasComment("Идентификатор записи студента");

        // имя и фамилия студента
        builder.Property(p => p.FirstName)
               .IsRequired()
               .HasColumnName("c_student_firstname")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(100)
               .HasComment("Имя студента");

        builder.Property(p => p.LastName)
               .IsRequired()
               .HasColumnName("c_student_lastname")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(100)
               .HasComment("Фамилия студента");

        // внешний ключ на группу
        builder.Property(p => p.GroupId)
               .IsRequired()
               .HasColumnName("f_group_id")
               .HasColumnType(ColumnType.Int)
               .HasComment("Идентификатор группы");

        // статус удаления
        builder.Property(p => p.IsDeleted)
               .IsRequired()
               .HasColumnName("b_deleted")
               .HasColumnType(ColumnType.Bool)
               .HasDefaultValue(false)
               .HasComment("Статус удаления");

        // связь со студентами и группой
        builder.HasOne(p => p.Group)
               .WithMany(t => t.Students)
               .HasForeignKey(p => p.GroupId)
               .HasConstraintName("fk_f_group_id")
               .OnDelete(DeleteBehavior.Cascade);

        // индекс на группу для ускорения запросов
        builder.HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

        // автоподгрузка группы вместе со студентом
        builder.Navigation(p => p.Group)
               .AutoInclude();
    }
}

