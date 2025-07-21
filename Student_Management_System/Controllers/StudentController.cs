using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using Student_Management_System.Application.Services;
using Student_Management_System.Domain.Entities;
using System.IO;
using System.Threading.Tasks;

namespace Student_Management_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        public IStudentService StudentService { get; set; }

        public StudentController(IStudentService studentService1)
        {
            StudentService = studentService1;
        }

        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await StudentService.GetAllStudents();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetStudentbyId([FromQuery] string id)
        {
            var student = await StudentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpPost]
        public async Task PickCourse(string studentid, string courseid)
        {
            await StudentService.AssignCourse(studentid, courseid);
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpGet]
        public async Task<IActionResult> CheckGrades(string studentid)
        {
            var grades = await StudentService.CheckGrades(studentid);
            if (grades == null)
            {
                return NotFound();
            }
            return Ok(grades);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task AddNewStudent([FromBody] StudentDTO student)
        {
            await StudentService.AddStudent(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task Update(string id, [FromQuery] StudentDTO value)
        {
            await StudentService.UpdateStudent(id, value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task DeleteStudent([FromQuery] string id)
        {
            await StudentService.DeleteStudent(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetExcelFile()
        {
            var StudList = await StudentService.GetAllStudentsForExcel();

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Students");

            ws.Cell("A1").Value = "Student ID";
            ws.Cell("B1").Value = "First Name";
            ws.Cell("C1").Value = "Last Name";
            ws.Cell("D1").Value = "Phone";
            ws.Cell("E1").Value = "Enrollment Date";
            ws.Cell("F1").Value = "Department";
            ws.Cell("G1").Value = "Semester";
            ws.Cell("H1").Value = "CGPA";

            int row = 2;

            foreach (var student in StudList)
            {
                ws.Cell(row, 1).Value = student.StudentId;
                ws.Cell(row, 2).Value = student.FirstName;
                ws.Cell(row, 3).Value = student.LastName;
                ws.Cell(row, 4).Value = student.Phone;
                ws.Cell(row, 5).Value = student.EnrollmentDate.ToString("yyyy-MM-dd");
                ws.Cell(row, 6).Value = student.Department;
                ws.Cell(row, 7).Value = student.Semester;
                ws.Cell(row, 8).Value = student.CGPA;
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Students.xlsx"
            );
        }
    }
}
