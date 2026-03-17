using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IClearanceStatusRepository
    {
        Task<ClearanceStatuses> addAsync(ClearanceStatuses clearanceStatus);

        Task<IEnumerable<ClearanceStatuses>> addRangeAsync(IEnumerable<ClearanceStatuses> clearanceStatuses);

        Task<ClearanceStatuses?> getByIdAsync(int id);

        Task<IEnumerable<ClearanceStatuses>> getByClearanceIdAsync(int clearanceId);

        Task<ClearanceStatuses?> getByClearanceAndOfficeAsync(int clearanceId, int officeId);

        Task<IEnumerable<ClearanceStatuses>> getByOfficeAsync(int officeId);

        Task<IEnumerable<ClearanceStatuses>> GetByStudentIdAsync(int studentId);

        Task<bool> removeAsync(int id);

        Task saveChangesAsync();
    }
}
