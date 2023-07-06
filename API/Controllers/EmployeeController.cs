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
        public ActionResult<IEnumerable<EmployeeDTO>> getEmployees()
        {
            return Ok(EmployeeData.EmployeeList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EmployeeDTO> GetEmployee(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var emp = EmployeeData.EmployeeList.FirstOrDefault(x => x.Id == id);
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

    }
}
