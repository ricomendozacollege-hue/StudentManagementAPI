using StudentManagementAPI.DTOs;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Mappings
{
    public class StudentMappingProfile
    {
        public static StudentDto ToDto(Student student)
        {
            if (student == null) return null;

            return new StudentDto
            {
                StudentId = student.Id,
                StudentNumber = student.StudentNumber,
                LastName = student.LastName,
                FirstName = student.FirstName,
                Gender = student.Gender,
                Address = student.Address,
                Birthday = student.Birthday,
                BirthPlace = student.BirthPlace
            };
        }
        public static Student ToEntity(AddStudentDto addDto, int generatedId)
        {
            return new Student
            {
                Id = generatedId,
                StudentNumber = addDto.StudentNumber,
                LastName = addDto.LastName,
                FirstName = addDto.FirstName,
                Gender = addDto.Gender,
                Address = addDto.Address,
                Birthday = addDto.Birthday,
                BirthPlace = addDto.BirthPlace
            };
        }
        public static void UpdateEntity(Student existingStudent, EditStudentDto editDto)
        {
            existingStudent.LastName = editDto.LastName;
            existingStudent.FirstName = editDto.FirstName;
            existingStudent.Gender = editDto.Gender;
            existingStudent.Birthday = editDto.Birthday;
            existingStudent.BirthPlace = editDto.BirthPlace;

        }
    }
}
