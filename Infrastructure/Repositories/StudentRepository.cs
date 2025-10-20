using AspDotNetCoreMVCProject.Data;
using AspDotNetCoreMVCProject.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            Save();
        }

        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            _context.Students.Remove(student);
            Save();

        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id) ?? new Student();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Student> SearchStudent(string searchString)
        {

            if (string.IsNullOrWhiteSpace(searchString))
            {
                return GetAllStudents();
            }
            bool isAgeSearch = int.TryParse(searchString, out int age);
            var lowerSearchString = searchString.ToLower();

           return _context.Students.Where(
                s => s.Name.ToLower().Contains(lowerSearchString) ||
                     s.Email.ToLower().Contains(lowerSearchString) ||
                     s.Mobile.ToLower().Contains(lowerSearchString) ||
                     s.Gender.ToLower().Contains(lowerSearchString) ||
                     (isAgeSearch && s.Age == age)
                ).ToList();

            

        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Email = student.Email;
            existingStudent.Mobile = student.Mobile;
            existingStudent.Gender = student.Gender;

            Save();

        }
    }
}
