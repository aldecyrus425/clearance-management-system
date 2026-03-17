using MyApp.Application.DTO.Clearance;
using MyApp.Application.DTO.Response;
using MyApp.Application.Interfaces.Repository;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class ClearanceServices
    {
        private readonly IClearancesRespository _clearanceRepository;

        public ClearanceServices(IClearancesRespository clearanceRepository)
        {
            _clearanceRepository = clearanceRepository;
        }

        public async Task<ResponseDTO<ShowClearanceListDTO>> createClearanceAsync(CreateClearanceDTO dto)
        {
            try
            {
                var clearance = new Clearances(dto.StudentId, dto.SchoolYearId);

                await _clearanceRepository.AddClearanceAsync(clearance);

                var result = await _clearanceRepository.GetClearanceByIdAsync(clearance.ClearanceId);

                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = true,
                    Message = "Clearance created successfully",
                    Data = MapToDTO(result!)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowClearanceListDTO>> getClearanceAsync(int id)
        {
            try
            {
                var clearance = await _clearanceRepository.GetClearanceByIdAsync(id);

                if (clearance == null)
                {
                    return new ResponseDTO<ShowClearanceListDTO>
                    {
                        Success = false,
                        Message = "Clearance not found"
                    };
                }

                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = true,
                    Data = MapToDTO(clearance)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowClearanceListDTO>>> getAllClearancesAsync()
        {
            try
            {
                var clearances = await _clearanceRepository.GetAllClearancesAsync();

                return new ResponseDTO<IEnumerable<ShowClearanceListDTO>>
                {
                    Success = true,
                    Data = clearances.Select(MapToDTO)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowClearanceListDTO>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowClearanceListDTO>>> getClearancesByStudentAsync(int studentId)
        {
            try
            {
                var clearances = await _clearanceRepository.GetClearancesByStudentAsync(studentId);

                return new ResponseDTO<IEnumerable<ShowClearanceListDTO>>
                {
                    Success = true,
                    Data = clearances.Select(MapToDTO)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowClearanceListDTO>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowClearanceListDTO>> completeClearanceAsync(int id)
        {
            try
            {
                var clearance = await _clearanceRepository.GetClearanceByIdAsync(id);

                if (clearance == null)
                {
                    return new ResponseDTO<ShowClearanceListDTO>
                    {
                        Success = false,
                        Message = "Clearance not found"
                    };
                }

                clearance.Complete();
                await _clearanceRepository.SaveChangesAsync();

                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = true,
                    Message = "Clearance completed successfully",
                    Data = MapToDTO(clearance)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowClearanceListDTO>> resetClearanceAsync(int id)
        {
            try
            {
                var clearance = await _clearanceRepository.GetClearanceByIdAsync(id);

                if (clearance == null)
                {
                    return new ResponseDTO<ShowClearanceListDTO>
                    {
                        Success = false,
                        Message = "Clearance not found"
                    };
                }

                clearance.Reset();
                await _clearanceRepository.SaveChangesAsync();

                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = true,
                    Message = "Clearance reset successfully",
                    Data = MapToDTO(clearance)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowClearanceListDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> deleteClearanceAsync(int id)
        {
            try
            {
                var removed = await _clearanceRepository.RemoveClearanceAsync(id);

                if (!removed)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Clearance not found",
                        Data = false
                    };
                }

                await _clearanceRepository.SaveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Clearance deleted successfully",
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

        private ShowClearanceListDTO MapToDTO(Clearances c)
        {
            return new ShowClearanceListDTO
            {
                ClearanceId = c.ClearanceId,
                StudentName = c.Students.Users.FirstName + " " + c.Students.Users.LastName ?? "N/A",
                StudentNumber = c.Students?.StudentNumber ?? "N/A",
                SchoolYear = c.SchoolYears.YearStarted + " - " + c.SchoolYears.YearEnd + " " + c.SchoolYears.Semester ?? "N/A",
                OverallStatus = c.OverallStatus
            };
        }
    }
}
