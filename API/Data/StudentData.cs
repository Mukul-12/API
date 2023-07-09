using API.Models.DTO;

namespace API.Data
{
    public class StudentData
    {
        public static List<StudentDTO> studentList = new List<StudentDTO>()
        {
            new StudentDTO(){Id = 1, Name = "Mukul", Fees= 10000, Email="mukul@gmail.com"},
            new StudentDTO(){Id = 2, Name = "Ram",  Fees= 20000, Email="ram@gmail.com"}
        };
    }
}
