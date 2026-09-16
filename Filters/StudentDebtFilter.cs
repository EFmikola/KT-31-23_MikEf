using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Filters
{
    public class StudentDebtFilter
    {
        [Required]
        public string LastName { get; set; } = string.Empty;

    }
}
