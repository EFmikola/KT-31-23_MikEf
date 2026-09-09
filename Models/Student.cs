using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Models;

public class Student
{
    public int StudentId { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    public int GroupId { get; set; }

    public bool IsDeleted { get; set; }

    public Group? Group { get; set; }
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}