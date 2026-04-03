using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Course;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync([FromBody] PaginationDTO dto)
        {
            try
            {
                return Ok(await _courseServices.GetAllCoursesAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseByIDAsync(int id)
        {
            try
            {
                var response = await _courseServices.GetCourseByIDAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseAsync(CreateCourseDTO dto)
        {
            try
            {
                return Ok(await _courseServices.CreateCourseAsync(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourseAsync(UpdateCourseDTO dto)
        {
            try
            {
                var response = await _courseServices.UpdateCourseAsync(dto);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            try
            {
                var response = await _courseServices.DeleteCourseAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateCourseAsync(int id)
        {
            try
            {
                var response = await _courseServices.ActivateCourseAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateCourseAsync(int id)
        {
            try
            {
                var response = await _courseServices.DeactivateCourseAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
