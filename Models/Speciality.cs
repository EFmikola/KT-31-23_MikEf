using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
