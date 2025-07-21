using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Student_Management_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
  //  [Authorize(Roles = "Admin,Teacher,Student")]
    public class CoursesController : ControllerBase
    {
        private ICourseService courseService;

        public CoursesController(ICourseService courseService1)
        {
            courseService = courseService1;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var course = await courseService.GetAllCourses();

            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);

        }
        [HttpGet]
        public async Task<IActionResult> GetCourseById([FromQuery] string id)
        {
            var course = await courseService.GetCourse(id);

            return course == null ? NotFound() : Ok(course);
        }

      //  [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddNewCourse([FromQuery] CourseDTO value)
        {
           await courseService.AddCourse(value);

            return Ok("Added Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCourses(string id, [FromQuery] CourseDTO value)
        {
           await courseService.UpdateCourse(id, value);

            return Ok("Updated Successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse([FromQuery] string id)
        {
            await courseService.DeleteCourse(id);

            return Ok("Deleted Successfully");
        }
    }
}
