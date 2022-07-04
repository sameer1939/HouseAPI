using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserReqDTO userReqDTO)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(userReqDTO.Username, userReqDTO.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var resDTO = new UserResDTO();
            resDTO.Username = user.Username;
            resDTO.Token = CreateJWT(user);
            return Ok(resDTO);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserReqDTO userReqDTO)
        {
            var user = await _unitOfWork.UserRepository.UserAlreadyExists(userReqDTO.Username);
            if (user)
            {
                return BadRequest("User already exists please try something else");
            }

            _unitOfWork.UserRepository.Register(userReqDTO.Username, userReqDTO.Password);
            _unitOfWork.SaveAsync();
            return Ok();
        }

        public string CreateJWT(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
