using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Requests
{
    public class CreateStudentRequest
    {

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public int GroupId { get; set; }
    }
}
