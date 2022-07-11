using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.IRepository;
using WebAPI.DTOs;
using WebAPI.IServices;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoUploadService _photoUploadService;

        public PropertyController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoUploadService photoUploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoUploadService = photoUploadService;
        }
        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties(int sellRent)
        {
            var properties = await _unitOfWork.PropertyRepository.GetProperties(sellRent);
            var propertyListDTO = _mapper.Map<IEnumerable<PropertyListDTO>>(properties);
            return Ok(propertyListDTO);
        }

        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var properties = await _unitOfWork.PropertyRepository.GetPropertyDetail(id);
            var propertyDetailDTO = _mapper.Map<PropertyDetailDTO>(properties);
            return Ok(propertyDetailDTO);
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult AddNewProperty(PropertyDTO propertyDTO)
        {
            var property = _mapper.Map<Property>(propertyDTO);
            int userId = GetUserId();
            property.LastUpdatedBy = userId;
            property.PostedBy = userId;
            _unitOfWork.PropertyRepository.AddProperty(property);
            _unitOfWork.SaveAsync();
            return StatusCode(201);
        }
        [HttpPost("uploadPhoto/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(IFormFile file,int propId)
        {
            var result = await _photoUploadService.UploadPhotoAsync(file);
            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);

            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }
            property.Photos.Add(photo);
            _unitOfWork.SaveAsync();
            return StatusCode(201);
        }
        [HttpPost("set-primary-photo/{propId}/{publicId}")]
        [Authorize]
        public async Task<IActionResult> SetPrimaryPhoto(int propId,string publicId)
        {
            var userId = GetUserId();

            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);
            if (property == null)
                return BadRequest("No such property available");

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised user to set primary photo");

            var photo = property.Photos.FirstOrDefault(x => x.PublicId == publicId);
            if (photo == null)
                return BadRequest("No such photo is available");

            if (photo.IsPrimary)
                return BadRequest("This is already a primary photo");

            var currentPrimary = property.Photos.FirstOrDefault(x => x.IsPrimary);
            if (currentPrimary != null) currentPrimary.IsPrimary = false;
            photo.IsPrimary = true;
            try
            {
                _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong, failed to set primary photo");
            }
        }

        [HttpDelete("delete-photo/{propId}/{publicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(int propId, string publicId)
        {
            var userId = GetUserId();

            var property = await _unitOfWork.PropertyRepository.GetPropertyByIdAsync(propId);
            if (property == null)
                return BadRequest("No such property available");

            if (property.PostedBy != userId)
                return BadRequest("You are not authorised user to set primary photo");

            var photo = property.Photos.FirstOrDefault(x => x.PublicId == publicId);
            if (photo == null)
                return BadRequest("No such photo is available");

            if (photo.IsPrimary)
                return BadRequest("You can not delete the primary photo");

            property.Photos.Remove(photo);

            var result = await _photoUploadService.DeletePhotoAsync(publicId);
            if (result.Error != null) return BadRequest(result.Error.Message);

            try
            {
                _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong, failed to set primary photo");
            }
        }
    }
}
