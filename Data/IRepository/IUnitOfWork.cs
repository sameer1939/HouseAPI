using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.IRepository
{
    public interface IUnitOfWork
    {
        public ICityRepository CityRepository { get; }
        public IUserRepository UserRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IFurnitureTypeRepository FurnitureTypeRepository { get; }
        public IPropertyTypeRepository PropertyTypeRepository { get; }

        void SaveAsync();
    }
}
