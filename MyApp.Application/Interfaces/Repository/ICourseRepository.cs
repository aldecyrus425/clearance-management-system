using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface ICourseRepository
    {
        Task<(IEnumerable<Course>, int totalCounts)> getAllCoursesAsync(PaginationDTO dto);
        Task<Course?> getCourseByIDAsync(int id);
        Task<Course> addCourseAsync(Course course);
        Task<bool> deleteCourseAsync(int id);
        Task saveChangesAsync();
    }
}
