using MyApp.Application.DTO.Pagination;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IClearancesRespository
    {
        Task<Clearances> AddClearanceAsync(Clearances clearance);

        Task<Clearances?> GetClearanceByIdAsync(int id);

        Task<(IEnumerable<Clearances>, int totalCounts)> GetAllClearancesAsync(PaginationDTO dto);

        Task<IEnumerable<Clearances>> GetClearancesByStudentAsync(int studentId);

        Task<bool> RemoveClearanceAsync(int id);

        Task SaveChangesAsync();
    }
}
