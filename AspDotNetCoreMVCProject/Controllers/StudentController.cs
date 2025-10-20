
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
        public IActionResult Search([FromQuery]string searchString)
        {
            var students = _studentRepository.SearchStudent(searchString);
            var result = students.Select(s => new
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
        public IActionResult Index()
        {


            var students = _studentRepository.GetAllStudents();
            return View(students);
        }
        //GET: Create New Student
        public IActionResult Create()
        {
            return View();
        }

        //POST: Create New Student
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);

            }
            _studentRepository.AddStudent(student);

            return RedirectToAction("Index");
        }

        //GET: Student Details
        public IActionResult Details(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Edit Student
        public IActionResult Edit(int id)
        {
            Student student = _studentRepository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Edit Student
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var existingStudent = _studentRepository.GetStudentById(student.Id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            _studentRepository.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        //GET: Delete Student
        public IActionResult Delete(int id)
        {

            var student = _studentRepository.GetStudentById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        //POST: Delete Student
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {

            var student = _studentRepository.GetStudentById(id);
            if (student == null) return NotFound();
            _studentRepository.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    }
}
