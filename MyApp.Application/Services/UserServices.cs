using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.User;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseDTO<ShowUserDTO>> addUserAsync(CreateUserDTO dto)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                var user = new Users(dto.FirstName, dto.MiddleName, dto.LastName, dto.Email, hashedPassword, dto.RoleId);

                var response = await _userRepository.addUserAsync(user);


                return new ResponseDTO<ShowUserDTO>
                {
                    Success = true,
                    Message = "User added successfully.",
                    Data = new ShowUserDTO
                    {
                        UserId = response.UserId,
                        FullName = response.FirstName + " " +  response.MiddleName + " " + response.LastName,
                        Email = response.Email,
                        Role = response.Roles.RoleName,
                        IsActive = response.IsActive,
                    }
                };

            }
            catch (ArgumentException ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowUserDTO>> changePasswordAsync(UpdateUserPasswordDTO dto)
        {
            try
            {
                if (dto.CurrentPassword == dto.Password)
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "New password cannot be the same as the current password."
                    };
                }

                var user = await _userRepository.getUserByIDAsync(dto.UserId);
                if (user == null)
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }


                if(!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "User Password Invalid."
                    };
                }

                user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(dto.Password));
                await _userRepository.saveChangesAsync();

                return new ResponseDTO<ShowUserDTO>
                {
                    Success = true,
                    Message = "User change password successfully."
                };

            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowUserDTO>> deleteUserAsync(int id)
        {
            try
            {
                var response = await _userRepository.deleteUserAsync(id);
                if(!response)
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                await _userRepository.saveChangesAsync();

                return new ResponseDTO<ShowUserDTO>
                {
                    Success = true,
                    Message = "User deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowUserDTO>>> getAllUserAsync()
        {
            try
            {
                var response = await _userRepository.getAllUserAsync();
                var users = response.Select(u => new ShowUserDTO
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                    Email = u.Email,
                    Role = u.Roles.RoleName,
                    IsActive = u.IsActive,
                });

                return new ResponseDTO<IEnumerable<ShowUserDTO>>
                {
                    Success = true,
                    Message = "User's Lists.",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowUserDTO>>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseDTO<ShowUserDTO>> getUserByIdAsync(int id)
        {
            try
            {
                var response = await _userRepository.getUserByIDAsync(id);
                if(response == null)
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                return new ResponseDTO<ShowUserDTO>
                {
                    Success = true,
                    Message = "User information.",
                    Data = new ShowUserDTO
                    {
                        UserId = response.UserId,
                        FullName = response.FirstName + " " + response.MiddleName + " " + response.LastName,
                        Email = response.Email,
                        Role = response.Roles.RoleName,
                        IsActive = response.IsActive,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseDTO<string>> toggleUserStatusAsync(int id)
        {
            try
            {
                var user = await _userRepository.getUserByIDAsync(id);
                if(user == null)
                {
                    return new ResponseDTO<string>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                user.ToggleUser();
                await _userRepository.saveChangesAsync();

                return new ResponseDTO<string>
                {
                    Success = true,
                    Message = "User toggled successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseDTO<ShowUserDTO>> updateUserAsync(UpdateUserDTO dto)
        {
            try
            {
                var response = await _userRepository.getUserByIDAsync(dto.UserId);
                if (response == null)
                {
                    return new ResponseDTO<ShowUserDTO>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                response.UpdateUser(dto.FirstName, dto.MiddleName, dto.LastName, dto.Email, dto.RoleId);
                await _userRepository.saveChangesAsync();

                return new ResponseDTO<ShowUserDTO>
                {
                    Success = true,
                    Message = "User updated successfully.",
                    Data = new ShowUserDTO
                    {
                        UserId = response.UserId,
                        FullName = response.FirstName + " " + response.MiddleName + " " + response.LastName,
                        Email = response.Email,
                        Role = response.Roles.RoleName,
                        IsActive = response.IsActive,
                    }
                };

            }
            catch (ArgumentException ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }

            catch (Exception ex)
            {
                return new ResponseDTO<ShowUserDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
