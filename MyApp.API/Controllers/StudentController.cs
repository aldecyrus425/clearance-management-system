using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Student;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents([FromBody] PaginationDTO dto)
        {
            try
            {
                return Ok(await _studentServices.getAllStudentAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentByIDAsync(int id)
        {
            try
            {
                return Ok(await _studentServices.getStudentByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentAsync(CreateStudentDTO dto)
        {
            try
            {
                return Ok(await _studentServices.addStudentAsync(dto));
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
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            try
            {
                var response = await _studentServices.deleteStudentAsync(id);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentAsync(UpdateStudentDTO dto)
        {
            try
            {
                var response = await _studentServices.updateStudentAsync(dto);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
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

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateStudentAsync(int id)
        {
            try
            {
                var response = await _studentServices.activateStudentAsync(id);
                if(!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateStudentAsync(int id)
        {
            try
            {
                var response = await _studentServices.deactivateStudentAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("drop/{id}")]
        public async Task<IActionResult> DropStudentAsync(int id)
        {
            try
            {
                var response = await _studentServices.dropStudentAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("graduate/{id}")]
        public async Task<IActionResult> GraduateStudentAsync(int id)
        {
            try
            {
                var response = await _studentServices.graduateStudentAsync(id);
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
