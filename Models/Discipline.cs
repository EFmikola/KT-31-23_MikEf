using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Models;

public class Discipline
{
    public int DisciplineId { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

