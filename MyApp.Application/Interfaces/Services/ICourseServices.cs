using MyApp.Application.DTO.Course;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface ICourseServices
    {

        Task<ResponseDTO<IEnumerable<ShowCourseDTO>>> GetAllCoursesAsync(PaginationDTO dto);
        Task<ResponseDTO<ShowCourseDTO?>> GetCourseByIDAsync(int id);
        Task<ResponseDTO<ShowCourseDTO>> CreateCourseAsync(CreateCourseDTO course);
        Task<ResponseDTO<ShowCourseDTO>> UpdateCourseAsync(UpdateCourseDTO dto);
        Task<ResponseDTO<bool>> DeleteCourseAsync(int id);
        Task<ResponseDTO<bool>> ActivateCourseAsync(int id);
        Task<ResponseDTO<bool>> DeactivateCourseAsync(int id);

    }
}
