using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Requests
{
    public class UpdateGroupRequest
    {
        public int GroupId { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        public int Course { get; set; }

        public int SpecialityId { get; set; }
    }
}
