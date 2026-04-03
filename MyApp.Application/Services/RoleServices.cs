using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Roles;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRolesRepository _roleRepo;

        public RoleServices(IRolesRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<ResponseDTO<IEnumerable<ShowRoleDTO>>> GetAllRolesAsync(PaginationDTO dto)
        {
            try
            {
                var (roles, totalCounts) = await _roleRepo.getAllRolesAsync(dto);

                var result = roles.Select(r => new ShowRoleDTO
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                });

                return new ResponseDTO<IEnumerable<ShowRoleDTO>>
                {
                    Success = true,
                    Message = "Roles retrieved successfully",
                    Data = result,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize,
                    TotalRecords = totalCounts,
                    TotalPages = (int)Math.Ceiling(totalCounts / (double)dto.PageSize)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowRoleDTO>>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<ShowRoleDTO>> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _roleRepo.getRolesByIDAsync(roleId);

                if (role == null)
                {
                    return new ResponseDTO<ShowRoleDTO>
                    {
                        Success = false,
                        Message = "Role not found",
                        Data = null
                    };
                }

                var result = new ShowRoleDTO
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                };

                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = true,
                    Message = "Role retrieved successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<ShowRoleDTO>> CreateRoleAsync(CreateRoleDTO dto)
        {
            try
            {
                var role = new Roles
                {
                    RoleName = dto.RoleName
                };

                var createdRole = await _roleRepo.addRoleAsync(role);

                var result = new ShowRoleDTO
                {
                    RoleId = createdRole.RoleId,
                    RoleName = createdRole.RoleName
                };

                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = true,
                    Message = "Role created successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<ShowRoleDTO>> UpdateRoleAsync(UpdateRoleDTO dto)
        {
            try
            {
                var role = await _roleRepo.getRolesByIDAsync(dto.RoleId);

                if (role == null)
                {
                    return new ResponseDTO<ShowRoleDTO>
                    {
                        Success = false,
                        Message = "Role not found",
                        Data = null
                    };
                }

                role.RoleName = dto.RoleName;

                await _roleRepo.saveChangesAsync();

                var result = new ShowRoleDTO
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                };

                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = true,
                    Message = "Role updated successfully",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowRoleDTO>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<bool>> DeleteRoleAsync(int roleId)
        {
            try
            {
                var deleted = await _roleRepo.deleteRoleAsync(roleId);

                if (!deleted)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Role not found",
                        Data = false
                    };
                }

                await _roleRepo.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Role deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = false
                };
            }
        }
    }
}