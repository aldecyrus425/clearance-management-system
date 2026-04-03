using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Office;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/office")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeServices _officeService;
        public OfficeController(IOfficeServices officeService)
        {
            _officeService = officeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOfficesAsync(PaginationDTO dto)
        {
            try
            {
                return Ok(await _officeService.getAllOfficeAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficeByIDAsync(int id)
        {
            try
            {
                var response = await _officeService.getOfficeAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfficeAsync(CreateOfficeDTO dto)
        {
            try
            {
                return Ok(await _officeService.createOfficeAsync(dto));
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOfficeAsync(UpdateOfficeDTO dto)
        {
            try
            {
                var response = await _officeService.updateOfficeAsync(dto);
                if (!response.Success)
                    return NotFound(response);

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
        public async Task<IActionResult> DeleteOfficeAsync(int id)
        {
            try
            {
                var response = await _officeService.deleteOfficeAsync(id);
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
        public async Task<IActionResult> ActivateOfficeAsync(int id)
        {
            try
            {
                var response = await _officeService.activateOfficeAsync(id);
                if (!response.Success)
                    return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateOfficeAsync(int id)
        {
            try
            {
                var response = await _officeService.deactivateOfficeAsync(id);
                if (!response.Success)
                    return NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
