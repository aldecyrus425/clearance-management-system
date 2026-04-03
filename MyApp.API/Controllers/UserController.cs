using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.User;
using MyApp.Application.Interfaces.Services;

namespace MyApp.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices) 
        {
            _userServices = userServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromBody]PaginationDTO dto)
        {
            try
            {
                var user = await _userServices.getAllUserAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            try
            {
                var user = await _userServices.getUserByIdAsync(id);
                if (!user.Success) return NotFound(user.Message);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var user = await _userServices.addUserAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UpdateUserPasswordDTO dto)
        {
            try
            {
                var user = await _userServices.changePasswordAsync(dto);
                if (!user.Success) return BadRequest(user.Message);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(int id)
        {
            try
            {
                var user = await _userServices.deleteUserAsync(id);
                if (!user.Success) return NotFound(user.Message);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

