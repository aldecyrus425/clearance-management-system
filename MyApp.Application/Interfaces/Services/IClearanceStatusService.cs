using MyApp.Application.DTO.Clearance;
using MyApp.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IClearanceStatusService
    {
        Task<ResponseDTO<ClearanceStatusDTO>> CreateAsync(int clearanceId, int officeId);

        Task<ResponseDTO<ClearanceStatusDTO>> ApproveAsync(int clearanceStatusId, int userId, string? remarks = null);

        Task<ResponseDTO<ClearanceStatusDTO>> RejectAsync(int clearanceStatusId, int userId, string remarks);

        Task<ResponseDTO<ClearanceStatusDTO>> ResetAsync(int clearanceStatusId);

        Task<ResponseDTO<ClearanceStatusDTO>> GetByIdAsync(int clearanceStatusId);

        Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByClearanceIdAsync(int clearanceId);

        Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByOfficeAsync(int officeId);

        Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByStudentAsync(int studentId);

        Task<ResponseDTO<bool>> DeleteAsync(int clearanceStatusId);
    }
}
