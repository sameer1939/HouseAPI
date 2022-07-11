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
    public class FurnitureTypeRepository : IFurnitureTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FurnitureTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<FurnitureType>> GetFurnitureTypeList()
        {
            return await _applicationDbContext.FurnitureTypes.ToListAsync();
        }
    }
}
