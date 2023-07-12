using API.Data;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> getStudents()
        {
            IEnumerable<Student> studentList = await _repository.GetAll();
            
            return Ok(_mapper.Map<List<StudentDTO>>(studentList));
        }


        [HttpGet("{id:int}", Name = "GetStu")]
        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var student =await _repository.GetById(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StudentDTO>(student));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<StudentDTO>> createStudent([FromBody] createStudentDTO student)
        {
            if (await _repository.GetById(u => u.Name.ToLower() == student.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Not Unique", "Name Already Exist");
                return BadRequest(ModelState);
            }
            if (student == null)
            {
                return BadRequest(student);
            }
            Student model = _mapper.Map<Student>(student);
            model.createdDate = DateTime.Now;
            model.updatedDate = DateTime.Now;
            await _repository.Create(model);

            return CreatedAtRoute("GetStu", new { id = model.Id }, model);
        }


        [HttpDelete("{id:int}", Name = "DelStu")]
        public async Task<IActionResult> deleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var stu =  await _repository.GetById(u => u.Id == id);
            if (stu == null)
            {
                return NotFound();
            }
            await _repository.Remove(stu);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "putStu")]
        public async Task<IActionResult> putStudent(int id, [FromBody] updateStudentDTO student)
        {
            if (student == null || id != student.Id)
            {
                return BadRequest();
            }
            Student model = _mapper.Map<Student>(student);
            model.updatedDate = DateTime.Now;
            await _repository.Update(model);
            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "patchStu")]
        public async Task<IActionResult> patchStudent(int id, JsonPatchDocument<updateStudentDTO> student)
        {
            if (student == null || id == 0)
            {
                return BadRequest();
            }

            var stu = await _repository.GetById(u => u.Id == id, tracked:false);

            updateStudentDTO studentDTO = _mapper.Map<updateStudentDTO>(stu);

            if (stu == null)
            {
                return BadRequest();
            }
            student.ApplyTo(studentDTO, ModelState);
            Student model = _mapper.Map<Student>(studentDTO);
            model.updatedDate = DateTime.Now;
            await _repository.Update(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
