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
    public class FurnitureTypeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FurnitureTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFurnitureTypes()
        {
            var furnitureTypes = await _unitOfWork.FurnitureTypeRepository.GetFurnitureTypeList();
            var keyValues = _mapper.Map<IEnumerable<KeyValuePairDTO>>(furnitureTypes);
            return Ok(keyValues);
        }
    }
}
