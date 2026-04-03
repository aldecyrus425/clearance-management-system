using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Section;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class SectionServices : ISectionServices
    {
        private readonly ISectionRepository _repo;

        public SectionServices(ISectionRepository repo)
        {
            _repo = repo;
        }

        public async Task<ResponseDTO<ShowSectionDTO>> CreateSectionAsync(CreateSectionDTO dto)
        {
            try
            {
                var section = new Section(
                    dto.Name,
                    dto.CourseId,
                    dto.SchoolYearId,
                    dto.YearLevelId,
                    dto.IsActive
                );

                var result = await _repo.addSectionAsync(section);

                return new ResponseDTO<ShowSectionDTO>
                {
                    Success = true,
                    Message = "Section created successfully",
                    Data = MapToDTO(result)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowSectionDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowSectionDTO>>> GetAllSectionAsync(PaginationDTO dto)
        {
            var (sections, totalCounts) = await _repo.getAllSectionAsync(dto);

            return new ResponseDTO<IEnumerable<ShowSectionDTO>>
            {
                Success = true,
                Message = "Sections retrieved successfully",
                Data = sections.Select(MapToDTO),
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalRecords = totalCounts,
                TotalPages = (int)Math.Ceiling(totalCounts / (double)dto.PageSize)
            };
        }

        public async Task<ResponseDTO<ShowSectionDTO>> GetSectionByIDAsync(int id)
        {
            var section = await _repo.getSectionByIDAsync(id);

            if (section == null)
            {
                return new ResponseDTO<ShowSectionDTO>
                {
                    Success = false,
                    Message = "Section not found"
                };
            }

            return new ResponseDTO<ShowSectionDTO>
            {
                Success = true,
                Message = "Section retrieved successfully",
                Data = MapToDTO(section)
            };
        }

        public async Task<ResponseDTO<ShowSectionDTO>> UpdateSectionAsync(UpdateSectionDTO dto)
        {
            try
            {
                var section = await _repo.getSectionByIDAsync(dto.SectionID);

                if (section == null)
                {
                    return new ResponseDTO<ShowSectionDTO>
                    {
                        Success = false,
                        Message = "Section not found"
                    };
                }

                section.ChangeDetails(
                    dto.Name,
                    dto.CourseId,
                    dto.SchoolYearId,
                    dto.YearLevelId
                );

                if (dto.IsActive)
                    section.Activate();
                else
                    section.Deactivate();

                await _repo.saveChangesAsync();

                return new ResponseDTO<ShowSectionDTO>
                {
                    Success = true,
                    Message = "Section updated successfully",
                    Data = MapToDTO(section)
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowSectionDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<bool>> DeleteSectionAsync(int id)
        {
            var success = await _repo.deleteSectionAsync(id);

            if (!success)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Section not found",
                    Data = false
                };
            }

            await _repo.saveChangesAsync();

            return new ResponseDTO<bool>
            {
                Success = true,
                Message = "Section deleted successfully",
                Data = true
            };
        }

        public async Task<ResponseDTO<bool>> ActivateSectionAsync(int id)
        {
            try
            {
                var section = await _repo.getSectionByIDAsync(id);

                if (section == null)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Section not found",
                        Data = false
                    };
                }

                section.Activate();
                await _repo.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Section activated",
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

        public async Task<ResponseDTO<bool>> DeactivateSectionAsync(int id)
        {
            try
            {
                var section = await _repo.getSectionByIDAsync(id);

                if (section == null)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Section not found",
                        Data = false
                    };
                }

                section.Deactivate();
                await _repo.saveChangesAsync();

                return new ResponseDTO<bool>
                {
                    Success = true,
                    Message = "Section deactivated",
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

        private ShowSectionDTO MapToDTO(Section section)
        {
            return new ShowSectionDTO
            {
                SectionId = section.SectionId,
                Name = section.Name,
                CourseName = section.Course?.Name ?? "N/A",
                SchoolYear = section.SchoolYear.YearStarted + " - " + section.SchoolYear.YearEnd + " " + section.SchoolYear.Semester,
                YearLevel = section.YearLevel.Name,
                IsActive = section.IsActive
            };
        }
    }
}