using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IYearLevelRepository
    {
        Task<(IEnumerable<YearLevel>, int totalCounts)> getAllYearLevelAsync(PaginationDTO dto);
        Task<YearLevel?> getYearLevelByIDAsync(int id);
        Task<YearLevel> addYearLevelAsync(YearLevel yearLevel);
        Task<bool> deleteYearLevelAsync(int id);
        Task saveChangesAsync();
    }
}
