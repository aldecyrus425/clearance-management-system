using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface ISectionRepository
    {
        Task<(IEnumerable<Section>, int totalCounts)> getAllSectionAsync(PaginationDTO dto);
        Task<Section?> getSectionByIDAsync(int id);
        Task<Section> addSectionAsync(Section section);
        Task<bool> deleteSectionAsync(int id);
        Task saveChangesAsync();
    }
}
