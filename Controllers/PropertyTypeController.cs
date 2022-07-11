using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PropertyTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPropertyTypes()
        {
            var propertyTypes = await _unitOfWork.PropertyTypeRepository.GetPropertyTypeList();
            var keyValues = _mapper.Map<IEnumerable<KeyValuePairDTO>>(propertyTypes);
            return Ok(keyValues);
        }
    }
}
