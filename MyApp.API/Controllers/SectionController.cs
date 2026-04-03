using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Section;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/section")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionServices _services;

        public SectionController(ISectionServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSectionAsync([FromQuery] PaginationDTO dto)
        {
            try
            {
                return Ok(await _services.GetAllSectionAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSectionByIDAsync(int id)
        {
            try
            {
                var response = await _services.GetSectionByIDAsync(id);
                if (!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSectionAsync(int id)
        {
            try
            {
                var response = await _services.DeleteSectionAsync(id);
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
        public async Task<IActionResult> AddSectionAsync(CreateSectionDTO dto)
        {
            try
            {
                return Ok(await _services.CreateSectionAsync(dto));
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
        public async Task<IActionResult> UpdateSectionAsync(UpdateSectionDTO dto)
        {
            try
            {
                var response = await _services.UpdateSectionAsync(dto);
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

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateSectionAsync(int id)
        {
            try
            {
                var response = await _services.ActivateSectionAsync(id);
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
        public async Task<IActionResult> DeactivateAsync(int id)
        {
            try
            {
                var response = await _services.DeactivateSectionAsync(id);
                if(!response.Success)
                    return NotFound(response.Message);

                return Ok(response);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
