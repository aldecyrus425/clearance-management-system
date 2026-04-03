using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IStudentRespository
    {
        public Task<(IEnumerable<Students>, int totalCounts)> getAllStudentsAsync(PaginationDTO dto);
        public Task<IEnumerable<Students>> getAllStudents();
        public Task<Students?> getStudentsByIDAsync(int id);
        public Task<Students> addStudentAsync(Students student);
        public Task<bool> deleteStudentAsync(int id);
        public Task saveChangesAsync();
    }
}
