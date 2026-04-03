using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Roles;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoleAsync([FromBody] PaginationDTO dto)
        {
            try
            {
                return Ok(await _roleServices.GetAllRolesAsync(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRollAsync(int id)
        {
            try
            {
                var response = await _roleServices.GetRoleByIdAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            try
            {
                var response = await _roleServices.DeleteRoleAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            try
            {
                var response = await _roleServices.UpdateRoleAsync(dto);
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

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleDTO dto)
        {
            try
            {
                var response = await _roleServices.CreateRoleAsync(dto);
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
    }
}
