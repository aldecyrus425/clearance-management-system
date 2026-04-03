using MyApp.Application.DTO.Office;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class OfficeServices : IOfficeServices
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeServices(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        public async Task<ResponseDTO<ShowOfficeDTO>> activateOfficeAsync(int id)
        {
            try
            {
                var office = await _officeRepository.getOfficeByIDAsync(id);

                if (office == null)
                {
                    return new ResponseDTO<ShowOfficeDTO>
                    {
                        Success = false,
                        Message = "Office not found"
                    };
                }

                office.Activate();
                await _officeRepository.saveChangesAsync();

                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = true,
                    Message = "Office activated successfully",
                    Data = MapToDTO(office)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowOfficeDTO>> deactivateOfficeAsync(int id)
        {
            try
            {
                var office = await _officeRepository.getOfficeByIDAsync(id);

                if (office == null)
                {
                    return new ResponseDTO<ShowOfficeDTO>
                    {
                        Success = false,
                        Message = "Office not found"
                    };
                }

                office.Deactivate();
                await _officeRepository.saveChangesAsync();

                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = true,
                    Message = "Office deactivated successfully",
                    Data = MapToDTO(office)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowOfficeDTO>> createOfficeAsync(CreateOfficeDTO dto)
        {
            try
            {
                var office = new Offices(dto.OfficeName, dto.Description);

                await _officeRepository.addOfficeAsync(office);

                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = true,
                    Message = "Office created successfully",
                    Data = MapToDTO(office)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> deleteOfficeAsync(int id)
        {
            try
            {
                var removed = await _officeRepository.removeOfficeAsync(id);

                if (!removed)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Office not found",
                        Data = false
                    };
                }

                await _officeRepository.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Office deleted successfully",
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

        public async Task<ResponseDTO<IEnumerable<ShowOfficeDTO>>> getAllOfficeAsync(PaginationDTO dto)
        {
            try
            {
                var (offices, totalCounts) = await _officeRepository.getAllOfficesAsync(dto);

                var data = offices.Select(o => MapToDTO(o));

                return new ResponseDTO<IEnumerable<ShowOfficeDTO>>
                {
                    Success = true,
                    Data = data,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize,
                    TotalRecords = totalCounts,
                    TotalPages = (int)Math.Ceiling(totalCounts / (double)dto.PageSize)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowOfficeDTO>>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowOfficeDTO>> getOfficeAsync(int id)
        {
            try
            {
                var office = await _officeRepository.getOfficeByIDAsync(id);

                if (office == null)
                {
                    return new ResponseDTO<ShowOfficeDTO>
                    {
                        Success = false,
                        Message = "Office not found"
                    };
                }

                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = true,
                    Data = MapToDTO(office)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowOfficeDTO>> updateOfficeAsync(UpdateOfficeDTO dto)
        {
            try
            {
                var office = await _officeRepository.getOfficeByIDAsync(dto.OfficeID);

                if (office == null)
                {
                    return new ResponseDTO<ShowOfficeDTO>
                    {
                        Success = false,
                        Message = "Office not found"
                    };
                }

                office.UpdateOffice(dto.OfficeName, dto.Description);
                await _officeRepository.saveChangesAsync();

                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = true,
                    Message = "Office updated successfully",
                    Data = MapToDTO(office)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowOfficeDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        private ShowOfficeDTO MapToDTO(Offices office)
        {
            return new ShowOfficeDTO
            {
                OfficeId = office.OfficeId,
                OfficeName = office.OfficeName,
                OfficeDescription = office.Description
            };
        }
    }
}