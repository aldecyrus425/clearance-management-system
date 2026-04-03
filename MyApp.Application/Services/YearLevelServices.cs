using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.YearLevel;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class YearLevelServices : IYearLevelServices
    {
        private readonly IYearLevelRepository _repo;

        public YearLevelServices(IYearLevelRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDTO<ShowYearLevelDTO>> CreateYearLevelAsync(CreateYearLevelDTO dto)
        {
            try
            {
                var yearLevel = new YearLevel(dto.Name, dto.IsActive);

                var result = await _repo.addYearLevelAsync(yearLevel);

                return new ResponseDTO<ShowYearLevelDTO>
                {
                    Success = true,
                    Message = "Year level created successfully",
                    Data = MapToDTO(result)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowYearLevelDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowYearLevelDTO>>> GetAllYearLevelsAsync(PaginationDTO dto)
        {
            var (yearLevels, totalCounts) = await _repo.getAllYearLevelAsync(dto);

            return new ResponseDTO<IEnumerable<ShowYearLevelDTO>>
            {
                Success = true,
                Message = "Year levels retrieved successfully",
                Data = yearLevels.Select(MapToDTO),
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalRecords = totalCounts,
                TotalPages = (int)Math.Ceiling((double)totalCounts / dto.PageSize)
            };
        }

        public async Task<ResponseDTO<ShowYearLevelDTO>> GetYearLevelByIdAsync(int id)
        {
            var yearLevel = await _repo.getYearLevelByIDAsync(id);

            if (yearLevel == null)
            {
                return new ResponseDTO<ShowYearLevelDTO>
                {
                    Success = false,
                    Message = "Year level not found"
                };
            }

            return new ResponseDTO<ShowYearLevelDTO>
            {
                Success = true,
                Message = "Year level retrieved successfully",
                Data = MapToDTO(yearLevel)
            };
        }

        public async Task<ResponseDTO<ShowYearLevelDTO>> UpdateYearLevelAsync(UpdateYearLevelDTO dto)
        {
            try
            {
                var yearLevel = await _repo.getYearLevelByIDAsync(dto.YearLevelId);

                if (yearLevel == null)
                {
                    return new ResponseDTO<ShowYearLevelDTO>
                    {
                        Success = false,
                        Message = "Year level not found"
                    };
                }

                yearLevel.RenameYearLevel(dto.Name);

                if (dto.IsActive)
                    yearLevel.Activate();
                else
                    yearLevel.Deactivate();

                await _repo.saveChangesAsync();

                return new ResponseDTO<ShowYearLevelDTO>
                {
                    Success = true,
                    Message = "Year level updated successfully",
                    Data = MapToDTO(yearLevel)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowYearLevelDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> DeleteYearLevelAsync(int id)
        {
            var success = await _repo.deleteYearLevelAsync(id);

            if (!success)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Year level not found",
                    Data = false
                };
            }

            await _repo.saveChangesAsync();

            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Year level deleted successfully",
                Data = true
            };
        }

        public async Task<ResponseDTO<bool>> ActivateYearLevelAsync(int id)
        {
            try
            {
                var yearLevel = await _repo.getYearLevelByIDAsync(id);

                if (yearLevel == null)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Year level not found",
                        Data = false
                    };
                }

                yearLevel.Activate();
                await _repo.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Year level activated",
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

        public async Task<ResponseDTO<bool>> DeactivateYearLevelAsync(int id)
        {
            try
            {
                var yearLevel = await _repo.getYearLevelByIDAsync(id);

                if (yearLevel == null)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Year level not found",
                        Data = false
                    };
                }

                yearLevel.Deactivate();
                await _repo.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Year level deactivated",
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

        private ShowYearLevelDTO MapToDTO(YearLevel entity)
        {
            return new ShowYearLevelDTO
            {
                YearLevelId = entity.YearLevelId,
                Name = entity.Name,
                IsActive = entity.IsActive
            };
        }
    }
}