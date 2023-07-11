using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class updateStudentDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Fees { get; set; }
    }
}
