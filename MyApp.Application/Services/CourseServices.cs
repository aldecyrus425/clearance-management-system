using MyApp.Application.DTO.Course;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _repo;

        public CourseServices(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDTO<ShowCourseDTO>> CreateCourseAsync(CreateCourseDTO dto)
        {
            var course = new Course(name: dto.Name, isActive: dto.IsActive);
            var result = await _repo.addCourseAsync(course);

            return new ResponseDTO<ShowCourseDTO>
            {
                Success = true,
                Message = "Course created successfully",
                Data = MapToShowDTO(result)
            };
        }

        public async Task<ResponseDTO<IEnumerable<ShowCourseDTO>>> GetAllCoursesAsync(PaginationDTO dto)
        {
            var (courses, totalCounts) = await _repo.getAllCoursesAsync(dto);

            return new ResponseDTO<IEnumerable<ShowCourseDTO>>
            {
                Success = true,
                Message = "Courses retrieved successfully",
                Data = courses.Select(MapToShowDTO),
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalRecords = totalCounts,
                TotalPages = (int)Math.Ceiling(totalCounts / (double)dto.PageSize)
            };
        }

        public async Task<ResponseDTO<ShowCourseDTO?>> GetCourseByIDAsync(int id)
        {
            var course = await _repo.getCourseByIDAsync(id);

            if (course == null)
            {
                return new ResponseDTO<ShowCourseDTO?>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = null
                };
            }

            return new ResponseDTO<ShowCourseDTO?>
            {
                Success = true,
                Message = "Course retrieved successfully",
                Data = MapToShowDTO(course)
            };
        }

        public async Task<ResponseDTO<ShowCourseDTO>> UpdateCourseAsync(UpdateCourseDTO dto)
        {
            var course = await _repo.getCourseByIDAsync(dto.CourseID);

            if (course == null)
            {
                return new ResponseDTO<ShowCourseDTO>
                {
                    Success = false,
                    Message = "Course not found"
                };
            }

            course.Rename(dto.Name);

            await _repo.saveChangesAsync();

            return new ResponseDTO<ShowCourseDTO>
            {
                Success = true,
                Message = "Course updated successfully",
                Data = MapToShowDTO(course)
            };
        }

        public async Task<ResponseDTO<bool>> DeleteCourseAsync(int id)
        {
            var success = await _repo.deleteCourseAsync(id);

            if (!success)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = false
                };
            }

            await _repo.saveChangesAsync();

            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Course deleted successfully",
                Data = true
            };
        }

        public async Task<ResponseDTO<bool>> ActivateCourseAsync(int id)
        {
            var course = await _repo.getCourseByIDAsync(id);

            if (course == null)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = false
                };
            }

            course.ActivateCourse();
            await _repo.saveChangesAsync();

            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Course activated",
                Data = true
            };
        }

        public async Task<ResponseDTO<bool>> DeactivateCourseAsync(int id)
        {
            var course = await _repo.getCourseByIDAsync(id);

            if (course == null)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Course not found",
                    Data = false
                };
            }

            course.Deactivate();
            await _repo.saveChangesAsync();

            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Course deactivated",
                Data = true
            };
        }

        private ShowCourseDTO MapToShowDTO(Course course)
        {
            return new ShowCourseDTO
            {
                CourseId = course.CourseId,
                Name = course.Name,
                IsActive = course.IsActive
            };
        }
    }
}