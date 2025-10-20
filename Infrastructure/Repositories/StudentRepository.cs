using AspDotNetCoreMVCProject.Data;
using AspDotNetCoreMVCProject.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await Save();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await GetStudentById(id);
            _context.Students.Remove(student);
            await Save();

        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id) ?? new Student();
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> SearchStudent(string searchString)
        {

            if (string.IsNullOrWhiteSpace(searchString))
            {
                return await GetAllStudents();
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

        public async Task UpdateStudent(Student student)
        {
            var existingStudent = await GetStudentById(student.Id);
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Email = student.Email;
            existingStudent.Mobile = student.Mobile;
            existingStudent.Gender = student.Gender;

            await Save();

        }
    }
}
