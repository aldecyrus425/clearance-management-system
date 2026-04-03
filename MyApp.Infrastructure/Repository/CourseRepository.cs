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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CourseRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Course> addCourseAsync(Course course)
        {
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();

            return course;

        }

        public async Task<bool> deleteCourseAsync(int id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null) return false;

            _dbContext.Courses.Remove(course);

            return true;
        }

        public async Task<(IEnumerable<Course>, int totalCounts)> getAllCoursesAsync(PaginationDTO dto)
        {
            var query = _dbContext.Courses.AsQueryable();

            if(!string.IsNullOrEmpty(dto.Search))
            {
                query = query.Where(c => c.Name.Contains(dto.Search));
            }

            var totalCount = await query.CountAsync();

            var course = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();
            
            return (course, totalCount);
        }

        public async Task<Course?> getCourseByIDAsync(int id)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
