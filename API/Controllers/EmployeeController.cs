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
        [ProducesResponseType(200, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        public ActionResult<EmployeeDTO> GetEmployee(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var emp = EmployeeData.EmployeeList.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<EmployeeDTO> createEmployee([FromBody] EmployeeDTO employee)
        {
            if (EmployeeData.EmployeeList.FirstOrDefault(u => u.Name.ToLower() == employee.Name.ToLower()) != null) {
                ModelState.AddModelError("Not Unique", "Name Already Exist");
                return BadRequest(ModelState);
            }
            if (employee == null)
            {
                return BadRequest(employee);
            }

            if (employee.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            employee.Id = EmployeeData.EmployeeList.OrderByDescending(u => u.Id).First().Id + 1;
            EmployeeData.EmployeeList.Add(employee);
            /*return Ok(employee);*/
            return CreatedAtRoute("GetEmp", new { id = employee.Id }, employee);
        }


        [HttpDelete("{id:int}", Name = "DelEmp")]
        public ActionResult<EmployeeDTO> deleteEmployee(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var emp = EmployeeData.EmployeeList.FirstOrDefault(u=>u.Id == id);  
            if(emp == null)
            {
                return NotFound();
            }
            EmployeeData.EmployeeList.Remove(emp);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "putEmp")]
        public ActionResult<EmployeeDTO> putEmployee(int id, [FromBody] EmployeeDTO employee)
        {
            if(employee == null || id != employee.Id)
            {
                return BadRequest();
            }
            var emp = EmployeeData.EmployeeList.FirstOrDefault(u=>u.Id==id);
            emp.Name = employee.Name;
            emp.salary  =  employee.salary;
            return NoContent();
        }

    }
}
