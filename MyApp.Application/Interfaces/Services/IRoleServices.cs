using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Roles;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<ResponseDTO<IEnumerable<ShowRoleDTO>>> GetAllRolesAsync();

        Task<ResponseDTO<ShowRoleDTO>> GetRoleByIdAsync(int roleId);

        Task<ResponseDTO<ShowRoleDTO>> CreateRoleAsync(CreateRoleDTO dto);

        Task<ResponseDTO<ShowRoleDTO>> UpdateRoleAsync(UpdateRoleDTO dto);

        Task<ResponseDTO<bool>> DeleteRoleAsync(int roleId);
    }
}
