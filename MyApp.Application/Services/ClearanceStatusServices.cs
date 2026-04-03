using MyApp.Application.DTO.Clearance;
using MyApp.Application.DTO.Response;
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
    public class ClearanceStatusServices : IClearanceStatusService
    {
        private readonly IClearanceStatusRepository _repository;

        public ClearanceStatusServices(IClearanceStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<ClearanceStatusDTO>> CreateAsync(int clearanceId, int officeId)
        {
            try
            {
                var status = new ClearanceStatuses(clearanceId, officeId);
                await _repository.addAsync(status);
                await _repository.saveChangesAsync();

                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = true,
                    Message = "Clearance status created successfully",
                    Data = MapToDTO(status)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ClearanceStatusDTO>> ApproveAsync(int clearanceStatusId, int userId, string? remarks = null)
        {
            try
            {
                var status = await _repository.getByIdAsync(clearanceStatusId);
                if (status == null)
                    return new ResponseDTO<ClearanceStatusDTO> 
                    { 
                        Success = false, 
                        Message = "Status not found" 
                    };

                status.Approve(userId, remarks);
                await _repository.saveChangesAsync();

                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = true,
                    Message = "Status approved successfully",
                    Data = MapToDTO(status)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ClearanceStatusDTO>> RejectAsync(int clearanceStatusId, int userId, string remarks)
        {
            try
            {
                var status = await _repository.getByIdAsync(clearanceStatusId);
                if (status == null)
                    return new ResponseDTO<ClearanceStatusDTO> { Success = false, Message = "Status not found" };

                status.Reject(userId, remarks);
                await _repository.saveChangesAsync();

                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = true,
                    Message = "Status rejected successfully",
                    Data = MapToDTO(status)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ClearanceStatusDTO>> ResetAsync(int clearanceStatusId)
        {
            try
            {
                var status = await _repository.getByIdAsync(clearanceStatusId);
                if (status == null)
                    return new ResponseDTO<ClearanceStatusDTO> { Success = false, Message = "Status not found" };

                status.Reset();
                await _repository.saveChangesAsync();

                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = true,
                    Message = "Status reset successfully",
                    Data = MapToDTO(status)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ClearanceStatusDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ClearanceStatusDTO>> GetByIdAsync(int clearanceStatusId)
        {
            var status = await _repository.getByIdAsync(clearanceStatusId);
            if (status == null)
                return new ResponseDTO<ClearanceStatusDTO> { Success = false, Message = "Status not found" };

            return new ResponseDTO<ClearanceStatusDTO>
            {
                Success = true,
                Data = MapToDTO(status)
            };
        }

        public async Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByClearanceIdAsync(int clearanceId)
        {
            var statuses = await _repository.getByClearanceIdAsync(clearanceId);
            return new ResponseDTO<IEnumerable<ClearanceStatusDTO>>
            {
                Success = true,
                Data = statuses.Select(MapToDTO)
            };
        }

        public async Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByOfficeAsync(int officeId)
        {
            var statuses = await _repository.getByOfficeAsync(officeId);
            return new ResponseDTO<IEnumerable<ClearanceStatusDTO>>
            {
                Success = true,
                Data = statuses.Select(MapToDTO)
            };
        }

        public async Task<ResponseDTO<IEnumerable<ClearanceStatusDTO>>> GetByStudentAsync(int studentId)
        {
            var statuses = await _repository.GetByStudentIdAsync(studentId);
            return new ResponseDTO<IEnumerable<ClearanceStatusDTO>>
            {
                Success = true,
                Data = statuses.Select(MapToDTO)
            };
        }

        public async Task<ResponseDTO<bool>> DeleteAsync(int clearanceStatusId)
        {
            var removed = await _repository.removeAsync(clearanceStatusId);
            if (!removed)
                return new ResponseDTO<bool> { Success = false, Message = "Status not found", Data = false };

            await _repository.saveChangesAsync();
            return new ResponseDTO<bool> { Success = true, Message = "Status deleted", Data = true };
        }

        private ClearanceStatusDTO MapToDTO(ClearanceStatuses status)
        {
            return new ClearanceStatusDTO
            {
                ClearanceStatusId = status.ClearanceStatusId,
                ClearanceId = status.ClearanceId,
                OfficeId = status.OfficeId,
                OfficeName = status.Offices?.OfficeName ?? "N/A",
                Status = status.Status,
                Remarks = status.Remarks,
                ClearedBy = status.ClearedBy,
                ClearedByName = status.Users != null ? $"{status.Users.FirstName} {status.Users.LastName}" : null,
                ClearedAt = status.ClearedAt
            };
        }
    }
}
