using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]        //[Route("api/Employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        [HttpGet]
        public IEnumerable<EmployeeDTO> getEmployees()
        {
            var empList = new List<EmployeeDTO>()
            {
                new EmployeeDTO(){Id = 1, Name = "Mukul" },
                new EmployeeDTO(){Id = 2, Name = "Ram"}
            };
            return empList;
        }

    }
}
