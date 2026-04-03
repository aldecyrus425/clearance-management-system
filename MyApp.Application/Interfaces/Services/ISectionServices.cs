using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface ISectionServices
    {
        Task<ResponseDTO<IEnumerable<ShowSectionDTO>>> GetAllSectionAsync(PaginationDTO dto);
        Task<ResponseDTO<ShowSectionDTO>> GetSectionByIDAsync(int id);
        Task<ResponseDTO<ShowSectionDTO>> CreateSectionAsync(CreateSectionDTO dto);
        Task<ResponseDTO<ShowSectionDTO>> UpdateSectionAsync(UpdateSectionDTO dto);
        Task<ResponseDTO<bool>> DeleteSectionAsync(int id);
        Task<ResponseDTO<bool>> ActivateSectionAsync(int id);
        Task<ResponseDTO<bool>> DeactivateSectionAsync(int id);
    }
}
