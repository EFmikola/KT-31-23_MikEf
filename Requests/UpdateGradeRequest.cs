using ProjectPractice.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectPractice.Requests
{
    public class UpdateGradeRequest
    {
        public int GradeId { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public int StudentId { get; set; }

        public int DisciplineId { get; set; }

    }
}
