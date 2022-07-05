using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Data.IRepository
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username,string password);
        void Register(UserReqDTO userReqDTO);
        Task<bool> UserAlreadyExists(string username);
    }
}
