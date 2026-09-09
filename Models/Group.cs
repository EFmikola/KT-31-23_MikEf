using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Models;

public class Group
{
    public int GroupId { get; set; }

    [Required, MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public int Course { get; set; }

    public int SpecialityId { get; set; }

    public bool IsDeleted { get; set; }

    public Speciality? Speciality { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();

}