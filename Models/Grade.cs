using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Models;

public class Grade
{
    public int GradeId { get; set; }

    [Range(1, 5)]
    public int Value { get; set; }

    public int StudentId { get; set; }

    public int DisciplineId { get; set; }

    public Student? Student { get; set; }
    public Discipline? Discipline { get; set; }

}