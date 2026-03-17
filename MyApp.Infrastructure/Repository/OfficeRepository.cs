using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces.Repository;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repository
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OfficeRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Offices> addOfficeAsync(Offices office)
        {
            await _dbContext.Offices.AddAsync(office);
            await _dbContext.SaveChangesAsync();

            return office;
        }

        public async Task<IEnumerable<Offices>> getAllOfficesAsync()
        {
            return await _dbContext.Offices
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Offices?> getOfficeByIDAsync(int id)
        {
            return await _dbContext.Offices.FirstOrDefaultAsync(o => o.OfficeId == id);
        }

        public async Task<bool> removeOfficeAsync(int id)
        {
            var office = await _dbContext.Offices.FirstOrDefaultAsync(o => o.OfficeId == id);
            if (office == null) return false;

            _dbContext.Offices.Remove(office);

            return true;
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
