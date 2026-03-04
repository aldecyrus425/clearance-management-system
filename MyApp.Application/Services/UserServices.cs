using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.User;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
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

        public Task<ResponseDTO<ShowUserDTO>> addUserAsync(CreateUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<ShowUserDTO>> changePasswordAsync(UpdateUserPasswordDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<ShowUserDTO>> deleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<IEnumerable<ShowUserDTO>>> getAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<ShowUserDTO>> getUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<string>> toggleUserStatusAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<ShowUserDTO>> updateUserAsync(UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
