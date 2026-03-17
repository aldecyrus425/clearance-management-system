using MyApp.Application.DTO.Office;
using MyApp.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IOfficeServices
    {
        Task<ResponseDTO<IEnumerable<ShowOfficeDTO>>> getAllOfficeAsync();
        Task<ResponseDTO<ShowOfficeDTO>> getOfficeAsync(int id);
        Task<ResponseDTO<ShowOfficeDTO>> createOfficeAsync(CreateOfficeDTO dto);
        Task<ResponseDTO<ShowOfficeDTO>> activateOfficeAsync(int id);
        Task<ResponseDTO<ShowOfficeDTO>> deactivateOfficeAsync(int id);
        Task<ResponseDTO<ShowOfficeDTO>> updateOfficeAsync(UpdateOfficeDTO dto);
        Task<ResponseDTO<bool>> deleteOfficeAsync(int id);
    }
}
