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
        public Task<IEnumerable<Students>> getAllStudentsAsync();
        public Task<Students?> getStudentsByIDAsync(int id);
        public Task<Students> addStudentAsync(Students student);
        public Task<bool> deleteStudentAsync(int id);
        public Task saveChangesAsync();
    }
}
