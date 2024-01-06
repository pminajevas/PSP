using Microsoft.EntityFrameworkCore;
using PoS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Data.Repositories
{
    public class ServicesRepository
    {
        private readonly PoSDbContext _dbContext;

        public ServicesRepository(PoSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Service?> CreateServiceAsync(Service service)
        {
            _dbContext.Services.Add(service);
            await _dbContext.SaveChangesAsync();
            return service;
        }

        public async Task<bool> DeleteServiceAsync(Guid serviceId)
        {
            var deletedService = await _dbContext.Services.FindAsync(serviceId);
            if (deletedService != null)
            {
                _dbContext.Services.Remove(deletedService);
            }
            else
            {
                return false;
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _dbContext.Services.ToListAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            return await _dbContext.Services.FindAsync(serviceId);
        }

        public async Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate)
        {
            var service = await _dbContext.Services.FindAsync(serviceId);
            if (service != null)
            {
                _dbContext.Entry(service).CurrentValues.SetValues(serviceUpdate);
                await _dbContext.SaveChangesAsync();
            }

            return service;
        }

    }
}
