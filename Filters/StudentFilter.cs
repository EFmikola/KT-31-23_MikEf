using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Filters
{
    public class StudentFilter
    {

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int? GroupId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
