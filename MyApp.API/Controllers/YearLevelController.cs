using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.YearLevel;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/yearlevel")]
    public class YearLevelController : ControllerBase
    {
        private readonly IYearLevelServices _yearLevelServices;
        public YearLevelController(IYearLevelServices yearLevelServices)
        {
            _yearLevelServices = yearLevelServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYearLevelsAsync([FromBody]PaginationDTO dto)
        {
            try
            {
                return Ok(await _yearLevelServices.GetAllYearLevelsAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetYearLevelByIdAsync(int id)
        {
            try
            {
                var response = await _yearLevelServices.GetYearLevelByIdAsync(id);
                if(!response.Success)
                    return NotFound(response);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateYearLevelAsync(CreateYearLevelDTO dto)
        {
            try
            {
                var response = await _yearLevelServices.CreateYearLevelAsync(dto);
                return Ok(response);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateYearLevelAsync(UpdateYearLevelDTO dto)
        {
            try
            {
                var response = await _yearLevelServices.UpdateYearLevelAsync(dto);
                if(!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYearLevelAsync(int id)
        {
            try
            {
                var response = await _yearLevelServices.DeleteYearLevelAsync(id);
                if(!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateYearLevelAsync(int id)
        {
            try
            {
                var response = await _yearLevelServices.ActivateYearLevelAsync(id);
                if(!response.Success)
                    return NotFound(response.Message);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateYearLevelAsync(int id)
        {
            try
            {
                var response = await _yearLevelServices.DeactivateYearLevelAsync(id);
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
