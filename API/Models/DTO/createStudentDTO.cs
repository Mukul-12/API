using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class createStudentDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Fees { get; set; }
    }
}
