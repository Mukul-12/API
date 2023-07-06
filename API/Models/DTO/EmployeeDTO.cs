using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
