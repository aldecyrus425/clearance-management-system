using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<ResponseDTO<IEnumerable<ShowUserDTO>>> getAllUserAsync(PaginationDTO dto);
        public Task<ResponseDTO<ShowUserDTO>> getUserByIdAsync(int id);
        public Task<ResponseDTO<ShowUserDTO>> addUserAsync(CreateUserDTO dto);
        public Task<ResponseDTO<ShowUserDTO>> deleteUserAsync(int id);
        public Task<ResponseDTO<ShowUserDTO>> updateUserAsync(UpdateUserDTO dto);
        public Task<ResponseDTO<ShowUserDTO>> changePasswordAsync(UpdateUserPasswordDTO dto);
        public Task<ResponseDTO<string>> toggleUserStatusAsync(int id);
    }
}
