using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }

        public DateTime DueDate { get; set; }

        [RegularExpression("Active|Inactive", ErrorMessage = "Invalid Status")]
        [Required]
        public string Status { get; set; }
    }
}
