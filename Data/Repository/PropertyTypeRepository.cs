using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PropertyTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypeList()
        {
            return await _applicationDbContext.PropertyTypes.ToListAsync();
        }
    }
}
