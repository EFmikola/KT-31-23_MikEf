using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();

}