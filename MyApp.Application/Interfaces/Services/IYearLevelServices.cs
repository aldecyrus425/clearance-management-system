using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.YearLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IYearLevelServices
    {
        Task<ResponseDTO<IEnumerable<ShowYearLevelDTO>>> GetAllYearLevelsAsync(PaginationDTO dto);
        Task<ResponseDTO<ShowYearLevelDTO>> GetYearLevelByIdAsync(int id);
        Task<ResponseDTO<ShowYearLevelDTO>> CreateYearLevelAsync(CreateYearLevelDTO dto);
        Task<ResponseDTO<ShowYearLevelDTO>> UpdateYearLevelAsync(UpdateYearLevelDTO dto);
        Task<ResponseDTO<bool>> DeleteYearLevelAsync(int id);
        Task<ResponseDTO<bool>> ActivateYearLevelAsync(int id);
        Task<ResponseDTO<bool>> DeactivateYearLevelAsync(int id);
    }
}
