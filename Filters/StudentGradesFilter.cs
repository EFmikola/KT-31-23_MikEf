using ProjectPractice.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Filters
{
    public class StudentGradesFilter
    {

        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

    }
}
