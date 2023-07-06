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

        [HttpGet("{id:int}", Name = "GetEmp")]
        [ProducesResponseType(200, Type =typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
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

        [HttpPost]
        public ActionResult<EmployeeDTO> createEmployee([FromBody] EmployeeDTO employee)
        {
            if (employee == null)
            {
                return BadRequest(employee);
            }

            if(employee.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            employee.Id = EmployeeData.EmployeeList.OrderByDescending(u=>u.Id).First().Id + 1;
            EmployeeData.EmployeeList.Add(employee);
            /*return Ok(employee);*/
            return CreatedAtRoute("GetEmp", new { id = employee.Id }, employee);
        }


    }
}
