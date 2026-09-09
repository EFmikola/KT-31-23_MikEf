using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    // название таблицы в бд
    private const string TableName = "cd_group";

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(TableName);

        // первичный ключ и автоинкремент
        builder.HasKey(p => p.GroupId)
               .HasName($"pk_{TableName}_group_id");

        builder.Property(p => p.GroupId)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.GroupId)
               .HasColumnName("group_id")
               .HasComment("Идентификатор группы");

        // название группы
        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnName("c_group_name")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(20)
               .HasComment("Название группы");

        // номер курса
        builder.Property(p => p.Course)
               .IsRequired()
               .HasColumnName("n_course")
               .HasColumnType(ColumnType.Int)
               .HasComment("Номер курса");

        // внешний ключ на специальность
        builder.Property(p => p.SpecialityId)
               .IsRequired()
               .HasColumnName("f_speciality_id")
               .HasColumnType(ColumnType.Int)
               .HasComment("Идентификатор специальности");

        // статус удаления
        builder.Property(p => p.IsDeleted)
               .IsRequired()
               .HasColumnName("b_deleted")
               .HasColumnType(ColumnType.Bool)
               .HasDefaultValue(false)
               .HasComment("Статус удаления");

        // связь со специальностью
        builder.HasOne(p => p.Speciality)
               .WithMany(t => t.Groups)
               .HasForeignKey(p => p.SpecialityId)
               .HasConstraintName("fk_f_speciality_id")
               .OnDelete(DeleteBehavior.Restrict);

        // индекс для быстрого поиска по специальности
        builder.HasIndex(p => p.SpecialityId, $"idx_{TableName}_fk_f_speciality_id");

        // автоподгрузка специальности
        builder.Navigation(p => p.Speciality)
               .AutoInclude();
    }
}

