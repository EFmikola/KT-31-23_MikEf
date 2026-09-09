using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
{
    // название таблицы в бд
    private const string TableName = "cd_speciality";

    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.ToTable(TableName);

        // первичный ключ и автоинкремент
        builder.HasKey(p => p.SpecialityId)
               .HasName($"pk_{TableName}_speciality_id");

        builder.Property(p => p.SpecialityId)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.SpecialityId)
               .HasColumnName("speciality_id")
               .HasComment("Идентификатор специальности");

        // название специальности
        builder.Property(p => p.Title)
               .IsRequired()
               .HasColumnName("c_speciality_title")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(200)
               .HasComment("Название специальности");

        // код специальности (например 09.03.02)
        builder.Property(p => p.Code)
               .IsRequired()
               .HasColumnName("c_speciality_code")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(50)
               .HasComment("Код специальности");
    }
}

