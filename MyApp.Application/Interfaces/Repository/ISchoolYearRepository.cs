using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface ISchoolYearRepository
    {
        Task<(IEnumerable<SchoolYears>, int totalCounts)> getAllSchoolYearAsync(PaginationDTO dto);
        Task<IEnumerable<SchoolYears>> getAllActiveSchoolYear();
        Task<SchoolYears?> getSchoolYearByIDAsync(int id);
        Task<SchoolYears> addSchoolYearAsync(SchoolYears schoolYear);
        Task<bool> deleteSchoolYearAsync(int id);
        Task saveChangesAsync();
    }
}
