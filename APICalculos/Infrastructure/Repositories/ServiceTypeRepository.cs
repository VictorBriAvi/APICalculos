﻿using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICalculos.Infrastructure.Repositories
{
    public class ServiceTypeRepository :IServiceTypeRepository
    {
        private readonly MyDbContext _dbContext;

        public ServiceTypeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceType>> GetAllAsync()
        {
            return await _dbContext.ServiceTypes
                 .Include(st => st.ServiceCategories) 
                 .AsNoTracking()
                 .OrderByDescending(x => x.Id)
                 .ToListAsync();
        }

        public async Task<IEnumerable<ServiceType>> SearchAsync(Expression<Func<ServiceType, bool>> predicate)
        {
            return await _dbContext.ServiceTypes
                .Include(s => s.ServiceCategories)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<ServiceType> GetByIdAsync(int Id)
        {
            return await _dbContext.ServiceTypes.Include(st => st.ServiceCategories).AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task AddAsync(ServiceType serviceType)
        {
            await _dbContext.ServiceTypes.AddAsync(serviceType);
        }

        public void Update(ServiceType serviceType)
        {
            _dbContext.ServiceTypes.Update(serviceType);
        }

        public void Remove(ServiceType serviceType)
        {
            _dbContext.ServiceTypes.Remove(serviceType);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.ServiceTypes.AnyAsync(c => c.Name.Replace(" ", "").Trim() == convertName);
        }
    }
}
