using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utility;
using Web.Models;
using Web.Models.DTO;
using Web.Services.IServices;

namespace Web.Controllers
{
    public class StudentUiController : Controller
    {
        private readonly IStudentServices _studentService;
        private readonly IMapper _mapper;
        public StudentUiController(IStudentServices studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetAllStudents()
        {
            List<StudentDTO> list = new();

            var response = await _studentService.GetAllAsync<ApiResponses>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<StudentDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }


        public async Task<IActionResult> CreateStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(createStudentDTO model)
        {
            if (ModelState.IsValid)
            {

                var response = await _studentService.CreateAsync<ApiResponses>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(GetAllStudents));
                }
            }
            /*TempData["error"] = "Error encountered.";*/
            return View(model);
        }


    }
}
