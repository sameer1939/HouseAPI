using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }


        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null || user?.SaltKey == null)
                return null;

            if (!MatchPasswordHash(password, user.Password, user.SaltKey))
            {
                return null;
            }
            return user;

        }

        private bool MatchPasswordHash(string passwordtext, byte[] password, byte[] saltKey)
        {
            using (var hmac = new HMACSHA512(saltKey))
            {
                var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordtext));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void Register(UserReqDTO userReqDTO)
        {
            byte[] passwordHash, SaltKey;

            using (var hmca = new HMACSHA512())
            {
                passwordHash = hmca.ComputeHash(Encoding.UTF8.GetBytes(userReqDTO.Password));
                SaltKey = hmca.Key;
            }

            User user = new User();
            user.Password = passwordHash;
            user.Username = userReqDTO.Username;
            user.SaltKey = SaltKey;
            user.Email = userReqDTO.Email;
            user.Mobile = userReqDTO.Mobile;
            _applicationDbContext.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string username)
        {
            return await _applicationDbContext.Users.AnyAsync(x => x.Username == username);
        }
    }
}
