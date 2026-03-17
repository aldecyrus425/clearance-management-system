using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IOfficeRepository
    {
        Task<IEnumerable<Offices>> getAllOfficesAsync();
        Task<Offices?> getOfficeByIDAsync(int id);
        Task<Offices> addOfficeAsync(Offices office);
        Task<bool> removeOfficeAsync(int id);
        Task saveChangesAsync();
    }
}
