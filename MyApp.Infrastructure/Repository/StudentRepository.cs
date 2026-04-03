using Microsoft.EntityFrameworkCore;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.Interfaces.Repository;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repository
{
    public class StudentRepository : IStudentRespository
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Students> addStudentAsync(Students student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Students
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.StudentsId == student.StudentsId);
        }

        public async Task<bool> deleteStudentAsync(int id)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.StudentsId == id);
            if (student == null) return false;

            _dbContext.Students.Remove(student);


            return true;
        }

        public async Task<IEnumerable<Students>> getAllStudents()
        {
            return await _dbContext.Students
                .Include(s => s.Users)
                .Include(s => s.Course)
                .Include(s => s.YearLevel)
                .Include(s => s.Section)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(IEnumerable<Students>, int totalCounts)> getAllStudentsAsync(PaginationDTO dto)
        {
            var query = _dbContext.Students
                .Include(s => s.Users)
                .Include(s => s.Course)
                .Include(s => s.YearLevel)
                .Include(s => s.Section)
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrEmpty(dto.Search))
            {
                query = query.Where(s => 
                s.StudentNumber.Contains(dto.Search) ||
                s.Course.Name.Contains(dto.Search) ||
                s.YearLevel.Name.Contains(dto.Search) ||
                s.Section.Name.Contains(dto.Search) ||
                s.Users.FirstName.Contains(dto.Search) || 
                s.Users.LastName.Contains(dto.Search));
            }

            var totalCounts = await query.CountAsync();

            var students = await query
                .Skip((dto.PageNumber -1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (students, totalCounts);
        }

        public Task<Students?> getStudentsByIDAsync(int id)
        {
            return _dbContext.Students
                .Include(s => s.Users)
                .FirstOrDefaultAsync(s => s.StudentsId == id);
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
