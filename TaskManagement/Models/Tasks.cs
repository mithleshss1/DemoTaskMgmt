using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Tasks
    {
        [Key]
        [Required]
        public Guid TaskId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }

        public DateTime DueDate { get; set; }

        [RegularExpression("Active|Inactive", ErrorMessage = "Invalid Status")]
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
    }
}
