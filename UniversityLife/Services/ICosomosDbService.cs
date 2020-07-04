using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityLife.Models;

namespace UniversityLife.Services
{
    public interface ICosomosDbService
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string query);
        Task<Student> GetStudentAsync(string id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(string id, Student student);
        Task DeleteStudentAsync(string id);
    }
}
