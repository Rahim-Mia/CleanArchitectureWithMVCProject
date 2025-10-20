
using AspDotNetCoreMVCProject.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreMVCProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string searchString)
        {
            var students = await _studentRepository.SearchStudent(searchString);
            var result =  students.Select(s => new
            {
                id = s.Id,
                name = s.Name,
                email = s.Email,
                mobile = s.Mobile,
                age = s.Age,
                gender = s.Gender
            });

            return Json(result);
            
        }
        //GET: All Student
        public async Task<IActionResult> Index()
        {


            var students = await _studentRepository.GetAllStudents();
            return View(students);
        }
        //GET: Create New Student
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //POST: Create New Student
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);

            }
            await _studentRepository.AddStudent(student);

            return RedirectToAction("Index");
        }

        //GET: Student Details
        public async Task<IActionResult> Details(int id)
        {
            Student student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Edit Student
        public async Task<IActionResult> Edit(int id)
        {
            Student student = await _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Edit Student
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var existingStudent = await _studentRepository.GetStudentById(student.Id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            await _studentRepository.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        //GET: Delete Student
        public async Task<IActionResult> Delete(int id)
        {

            var student = await _studentRepository.GetStudentById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        //POST: Delete Student
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var student = await _studentRepository.GetStudentById(id);
            if (student == null) return NotFound();
            await _studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    }
}
