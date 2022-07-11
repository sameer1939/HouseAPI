using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PropertyRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddProperty(Property property)
        {
            _applicationDbContext.Properties.Add(property);
        }

        public bool DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetProperties(int sellRent)
        {
            return await _applicationDbContext.Properties
                .Include(x=>x.PropertyType)
                .Include(x=>x.FurnitureType)
                .Include(x=>x.City)
                .Include(x => x.Photos)
                .Where(x=>x.SellRent==sellRent)
                .ToListAsync();
        }
        public async Task<Property> GetPropertyDetail(int id)
        {
            return await _applicationDbContext.Properties
                .Include(x=>x.PropertyType)
                .Include(x=>x.FurnitureType)
                .Include(x=>x.City)
                .Include(x => x.Photos)
                .Where(x=>x.Id==id)
                .FirstAsync();
        }
        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _applicationDbContext.Properties
                .Include(x=>x.Photos)
                .Where(x=>x.Id==id)
                .FirstOrDefaultAsync();
        }
    }
}
