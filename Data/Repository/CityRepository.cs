using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CityRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void AddCity(City city)
        {
            _applicationDbContext.Cities.Add(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _applicationDbContext.Cities.ToListAsync();
        }

        public void RemoveCity(int id)
        {
            var entity = _applicationDbContext.Cities.Find(id);
            _applicationDbContext.Cities.Remove(entity);
        }
        public async Task<City> GetById(int id)
        {
            return await _applicationDbContext.Cities.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
