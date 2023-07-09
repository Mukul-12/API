using API.Data;
using API.Models;
using API.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {

            _context = context;

        }
        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> getStudents()
        {
            return Ok(_context.Students.ToList());
        }


        [HttpGet("{id:int}", Name = "GetStu")]
        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<StudentDTO> createStudent([FromBody] StudentDTO student)
        {
            if (_context.Students.FirstOrDefault(u => u.Name.ToLower() == student.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Not Unique", "Name Already Exist");
                return BadRequest(ModelState);
            }
            if (student == null)
            {
                return BadRequest(student);
            }

            if (student.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Student model = new()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Fees = student.Fees,
            };

            _context.Students.Add(model);
            _context.SaveChanges();
            /*return Ok(employee);*/
            return CreatedAtRoute("GetStu", new { id = student.Id }, student);
        }


        [HttpDelete("{id:int}", Name = "DelStu")]
        public ActionResult<StudentDTO> deleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var stu = _context.Students.FirstOrDefault(u => u.Id == id);
            if (stu == null)
            {
                return NotFound();
            }
            _context.Students.Remove(stu);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "putStu")]
        public ActionResult<StudentDTO> putStudent(int id, [FromBody] StudentDTO student)
        {
            if (student == null || id != student.Id)
            {
                return BadRequest();
            }
            Student model = new()
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Fees = student.Fees,
            };
            _context.Students.Update(model);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "patchStu")]
        public ActionResult<StudentDTO> patchStudent(int id, JsonPatchDocument<StudentDTO> student)
        {
            if (student == null || id == 0)
            {
                return BadRequest();
            }

            var stu = _context.Students.AsNoTracking().FirstOrDefault(u => u.Id == id);

            StudentDTO studentDTO = new()
            {
                Id = stu.Id,
                Name = stu.Name,
                Email = stu.Email,
                Fees = stu.Fees
            };
            if (stu == null)
            {
                return BadRequest();
            }
            student.ApplyTo(studentDTO, ModelState);
            Student model = new()
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                Email = studentDTO.Email,
                Fees = studentDTO.Fees,
            };
            _context.Students.Update(model);
            _context.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
