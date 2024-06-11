using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class FilterDto
    {
        public int Priority { get; set; }
       
        [RegularExpression("Active|Inactive", ErrorMessage = "Invalid Status")]
        public string? Status { get; set; }
    }
}
