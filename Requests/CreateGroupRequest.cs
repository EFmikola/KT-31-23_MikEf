using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Requests
{
    public class CreateGroupRequest
    {
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        public int Course { get; set; }

        public int SpecialityId { get; set; }
    }
}
