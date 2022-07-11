using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;

namespace WebAPI.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ICityRepository CityRepository { get; }
        public IUserRepository UserRepository { get; }
        public IPropertyRepository PropertyRepository { get; }
        public IFurnitureTypeRepository FurnitureTypeRepository { get; }
        public IPropertyTypeRepository PropertyTypeRepository { get; }

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            CityRepository = new CityRepository(_applicationDbContext);
            UserRepository = new UserRepository(_applicationDbContext);
            PropertyRepository = new PropertyRepository(_applicationDbContext);
            FurnitureTypeRepository = new FurnitureTypeRepository(_applicationDbContext);
            PropertyTypeRepository = new PropertyTypeRepository(_applicationDbContext);
        }

        public void SaveAsync()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
