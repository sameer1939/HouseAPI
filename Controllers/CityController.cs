using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IActionResult> GetCity()
        {
            IEnumerable<City> city = await _unitOfWork.CityRepository.GetCitiesAsync();
            var cityDTO = _mapper.Map<IEnumerable<CityDTO>>(city);
            return Ok(cityDTO);
        }

        [HttpPost("add")]
        public IActionResult AddCity(CityDTO citydto)
        {
            var city = _mapper.Map<City>(citydto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedDate = DateTime.Now;
            _unitOfWork.CityRepository.AddCity(city);
            _unitOfWork.SaveAsync();
            return Ok(city);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id,CityDTO citydto)
        {
            var city = await _unitOfWork.CityRepository.GetById(id);
            city.LastUpdatedBy = 1;
            city.LastUpdatedDate = DateTime.Now;
            _mapper.Map(citydto, city);
            _unitOfWork.SaveAsync();
            return Ok(city);
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteCity(int id)
        {
            _unitOfWork.CityRepository.RemoveCity(id);
            _unitOfWork.SaveAsync();
            return Ok(id);
        }
    }
}
