using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectPractice.Models
{
    public class Speciality
    {
        public int SpecialityId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
