using API.Data;
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
            
            return EmployeeData.EmployeeList;
        }

        [HttpGet("{id:int}")]
        public EmployeeDTO GetEmployee(int id)
        {
            return EmployeeData.EmployeeList.FirstOrDefault(x => x.Id == id);
        }

    }
}
