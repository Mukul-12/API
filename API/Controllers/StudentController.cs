using API.Data;
using API.Models;
using API.Models.DTO;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        protected ApiResponses _response;

        public StudentController(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            this._response = new ApiResponses();    
        }

        [HttpGet]
        /*public async Task<ActionResult<IEnumerable<StudentDTO>>> getS tudents()*/
        public async Task<ActionResult<ApiResponses>> getStudents()
        {
            try
            {
                IEnumerable<Student> studentList = await _repository.GetAll();

                /*return Ok(_mapper.Map<List<StudentDTO>>(studentList));*/

                _response.Result = _mapper.Map<List<StudentDTO>>(studentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};
            }
            return _response;
        }


        [HttpGet("{id:int}", Name = "GetStu")]
        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        /*public async Task<ActionResult<StudentDTO>> GetStudent(int id)*/
        public async Task<ActionResult<ApiResponses>> GetStudent(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Id can not be zero" };
                    return BadRequest(_response);
                }
                var student = await _repository.GetById(x => x.Id == id);
                if (student == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                /*return Ok(_mapper.Map<StudentDTO>(student));*/

                _response.Result = _mapper.Map<StudentDTO>(student);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        /*public async Task<ActionResult<StudentDTO>> createStudent([FromBody] createStudentDTO student)*/
        public async Task<ActionResult<ApiResponses>> createStudent([FromBody] createStudentDTO student)
        {
            try
            {
                if (await _repository.GetById(u => u.Name.ToLower() == student.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Not Unique", "Name Already Exist");
                    return BadRequest(ModelState);
                }
                if (student == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Student model = _mapper.Map<Student>(student);
                model.createdDate = DateTime.Now;
                model.updatedDate = DateTime.Now;
                await _repository.Create(model);

                _response.Result = _mapper.Map<StudentDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                /*return CreatedAtRoute("GetStu", new { id = model.Id }, model);*/
                return CreatedAtRoute("GetStu", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }


        [HttpDelete("{id:int}", Name = "DelStu")]
        public async Task<ActionResult<ApiResponses>> deleteStudent(int id)
        {
            try { 
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var stu = await _repository.GetById(u => u.Id == id);
                if (stu == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _repository.Remove(stu);

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpPut("{id:int}", Name = "putStu")]
        public async Task<ActionResult<ApiResponses>> putStudent(int id, [FromBody] updateStudentDTO student)
        {
            try
            {
                if (student == null || id != student.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Student model = _mapper.Map<Student>(student);

                await _repository.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }


        [HttpPatch("{id:int}", Name = "patchStu")]
        public async Task<ActionResult<ApiResponses>> patchStudent(int id, JsonPatchDocument<updateStudentDTO> student)
        {
            try
            {
                if (student == null || id == 0)
                {
                    return BadRequest();
                }

                var stu = await _repository.GetById(u => u.Id == id, tracked: false);

                updateStudentDTO studentDTO = _mapper.Map<updateStudentDTO>(stu);

                if (stu == null)
                {
                    return BadRequest();
                }
                student.ApplyTo(studentDTO, ModelState);
                Student model = _mapper.Map<Student>(studentDTO);

                await _repository.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
