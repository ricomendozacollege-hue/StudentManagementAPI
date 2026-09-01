
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.DTOs;
using StudentManagementAPI.Mappings;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static readonly List<Student> _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                StudentNumber = "202540024",
                LastName = "Robles",
                FirstName = "Rico",
                Gender = "Male",
                Address = "Ewan Gomen",
                Birthday = "04/29/2007",
                BirthPlace = "Quezon City"

            },
            new Student
            {
                Id = 2,
                StudentNumber = "2024463525",
                LastName = "Fang Li",
                FirstName = "Stuart",
                Gender = "Male",
                Address = "Chaoyang District",
                Birthday = "01/01/2001",
                BirthPlace = "Beijing"
            }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            var dtos = _students.Select(StudentMappingProfile.ToDto);
            return Ok(dtos);
        }
        [HttpGet("{id:int}")]
        public ActionResult<StudentDto> GetById([FromRoute] int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound(new { message = $"Student with {id} was not found." });
            }
            return Ok(StudentMappingProfile.ToDto(student));
        }
        [HttpGet("Search")]
        public ActionResult<IEnumerable<StudentDto>> Search([FromQuery] string LastName, [FromQuery] string FirstName)
        {
            var query = _students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                query = query.Where(s => s.LastName.Contains(LastName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                query = query.Where(s => s.FirstName.Contains(FirstName, StringComparison.OrdinalIgnoreCase));
            }
            var results = query.Select(StudentMappingProfile.ToDto).ToList();
            return Ok(results);

        }
        [HttpPost]
        public ActionResult<StudentDto> Create([FromBody] AddStudentDto addDto)
        {
            if (addDto == null)
            {
                return BadRequest("Invalid Student Data.");
            }
            int nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            var newStudent = StudentMappingProfile.ToEntity(addDto, nextId);

            _students.Add(newStudent);

            var createdDto = StudentMappingProfile.ToDto(newStudent);
            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, createdDto);
        }
        [HttpPut("{id;int}")]
        public ActionResult Update([FromRoute]int id, [FromBody] EditStudentDto editDto)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound(new { message = $"Student with {id} was not found." });
            }
            StudentMappingProfile.UpdateEntity(existingStudent, editDto);
            return Ok(StudentMappingProfile.ToDto(existingStudent));
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound(new { message = $"Student with {id} was not found." });
            }
            _students.Remove(existingStudent);
            return NoContent();
        }

        
    }
}
