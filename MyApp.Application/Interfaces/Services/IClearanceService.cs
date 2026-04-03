using MyApp.Application.DTO.Clearance;
using MyApp.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IClearanceService
    {
        Task<ResponseDTO<ShowClearanceListDTO>> createClearanceAsync(CreateClearanceDTO dto);

        Task<ResponseDTO<ShowClearanceListDTO>> getClearanceAsync(int id);

        Task<ResponseDTO<IEnumerable<ShowClearanceListDTO>>> getAllClearancesAsync();

        Task<ResponseDTO<IEnumerable<ShowClearanceListDTO>>> getClearancesByStudentAsync(int studentId);

        Task<ResponseDTO<ShowClearanceListDTO>> completeClearanceAsync(int id);

        Task<ResponseDTO<ShowClearanceListDTO>> resetClearanceAsync(int id);

        Task<ResponseDTO<bool>> deleteClearanceAsync(int id);
    }
}
