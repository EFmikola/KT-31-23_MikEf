using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    // название таблицы в бд
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        builder.ToTable(TableName);

        // первичный ключ и автоинкремент
        builder.HasKey(p => p.DisciplineId)
               .HasName($"pk_{TableName}_discipline_id");

        builder.Property(p => p.DisciplineId)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.DisciplineId)
               .HasColumnName("discipline_id")
               .HasComment("Идентификатор дисциплины");

        // название дисциплины
        builder.Property(p => p.Name)
               .IsRequired()
               .HasColumnName("c_discipline_name")
               .HasColumnType(ColumnType.String)
               .HasMaxLength(200)
               .HasComment("Название дисциплины");

        // статус удаления
        builder.Property(p => p.IsDeleted)
               .IsRequired()
               .HasColumnName("b_deleted")
               .HasColumnType(ColumnType.Bool)
               .HasDefaultValue(false)
               .HasComment("Статус удаления");
    }
}

