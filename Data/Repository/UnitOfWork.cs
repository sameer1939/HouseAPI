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

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            CityRepository = new CityRepository(_applicationDbContext);
            UserRepository = new UserRepository(_applicationDbContext);
        }

        public void SaveAsync()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
