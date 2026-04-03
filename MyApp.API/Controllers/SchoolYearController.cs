using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.SchoolYear;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/schoolyear")]
    public class SchoolYearController : ControllerBase
    {

        private readonly ISchoolYearServices _schoolYearServices;

        public SchoolYearController(ISchoolYearServices schoolYearServices)
        {
            _schoolYearServices = schoolYearServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchoolYearsAsync([FromBody] PaginationDTO dto)
        {
            try
            {
                return Ok(await _schoolYearServices.getAllSchoolYearAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolYearByIDAsync(int id)
        {
            try
            {
                var response = await _schoolYearServices.getSchoolYearByIdAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveSchoolYearAsync()
        {
            try
            {
                return Ok(await _schoolYearServices.GetActiveSchoolYearAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchoolyearAsync(CreateSchoolYearDTO dto)
        {
            try
            {
                return Ok(await _schoolYearServices.addSchoolYearAsync(dto));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchoolYearAsync(UpdateSchoolYearDTO dto)
        {
            try
            {
                var response = await _schoolYearServices.updateSchoolYearAsync(dto);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolYearAsync(int id)
        {
            try
            {
                var response = await _schoolYearServices.deleteSchoolYearAsync(id);
                if (!response.Success)
                    return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateSchoolYearAsync(int id)
        {
            try
            {
                var response = await _schoolYearServices.activateSchoolYearAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("deactivate/{id}")]
        public async Task<IActionResult> DeactivateSchoolYearAsync(int id)
        {
            try
            {
                var response = await _schoolYearServices.deactivateSchoolYearAsync(id);
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
