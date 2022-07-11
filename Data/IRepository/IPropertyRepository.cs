using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data.IRepository
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetProperties(int sellRent);
        Task<Property> GetPropertyDetail(int id);
        Task<Property> GetPropertyByIdAsync(int id);
        void AddProperty(Property property);
        bool DeleteProperty(int id);
    }
}
