using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using students_Api.Data;
using students_Api.DTOs;
using students_Api.Entities;

namespace students_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DataContext _context;
        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            if (students is null)
                return NotFound("No students found.");

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student is null)
                return NotFound("Student not found.");
            
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(CreateStudentDTO newStudent)
        {
            Student student = new Student()
            {
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                Email = newStudent.Email,
                ClassName = newStudent.ClassName,
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<Student>> Apdatetudent(Student UpdatedStudent)
        {
            var DbStudent = await _context.Students.FindAsync(UpdatedStudent.Id);
            if (DbStudent is null)
                return NotFound("Student not found.");
            
            DbStudent.FirstName = UpdatedStudent.FirstName;
            DbStudent.LastName = UpdatedStudent.LastName;
            DbStudent.Email = UpdatedStudent.Email;
            DbStudent.ClassName = UpdatedStudent.ClassName;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.FindAsync(UpdatedStudent.Id));
        }
        [HttpDelete]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            var DbStudent = await _context.Students.FindAsync(id);
            if (DbStudent is null)
                return NotFound("Student not found.");

            _context.Students.Remove(DbStudent);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
    }
}
