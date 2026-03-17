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
    public class ClearanceStatusRepository : IClearanceStatusRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ClearanceStatusRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ClearanceStatuses> addAsync(ClearanceStatuses clearanceStatus)
        {
            await _dbContext.ClearanceStatuses.AddAsync(clearanceStatus);
            await _dbContext.SaveChangesAsync();
            return clearanceStatus;
        }

        public async Task<IEnumerable<ClearanceStatuses>> addRangeAsync(IEnumerable<ClearanceStatuses> clearanceStatuses)
        {
            await _dbContext.ClearanceStatuses.AddRangeAsync(clearanceStatuses);
            await _dbContext.SaveChangesAsync();
            return clearanceStatuses;
        }

        public async Task<ClearanceStatuses?> getByIdAsync(int id)
        {
            return await _dbContext.ClearanceStatuses
                .Include(cs => cs.Clearances)
                .Include(cs => cs.Offices)
                .Include(cs => cs.Users)
                .FirstOrDefaultAsync(cs => cs.ClearanceStatusId == id);
        }

        public async Task<IEnumerable<ClearanceStatuses>> getByClearanceIdAsync(int clearanceId)
        {
            return await _dbContext.ClearanceStatuses
                .Where(cs => cs.ClearanceId == clearanceId)
                .Include(cs => cs.Offices)
                .Include(cs => cs.Users)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ClearanceStatuses?> getByClearanceAndOfficeAsync(int clearanceId, int officeId)
        {
            return await _dbContext.ClearanceStatuses
                .Include(cs => cs.Offices)
                .Include(cs => cs.Users)
                .FirstOrDefaultAsync(cs =>
                    cs.ClearanceId == clearanceId &&
                    cs.OfficeId == officeId);
        }


        public async Task<IEnumerable<ClearanceStatuses>> getByOfficeAsync(int officeId)
        {
            return await _dbContext.ClearanceStatuses
                .Where(cs => cs.OfficeId == officeId)
                .Include(cs => cs.Clearances)
                .Include(cs => cs.Users)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<ClearanceStatuses>> GetByStudentIdAsync(int studentId)
        {
            return await _dbContext.ClearanceStatuses
                .Include(cs => cs.Clearances)
                .Include(cs => cs.Offices)
                .Include(cs => cs.Users)
                .Where(cs => cs.Clearances.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> removeAsync(int id)
        {
            var entity = await _dbContext.ClearanceStatuses
                .FirstOrDefaultAsync(cs => cs.ClearanceStatusId == id);

            if (entity == null)
                return false;

            _dbContext.ClearanceStatuses.Remove(entity);
            return true;
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
