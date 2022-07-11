using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();

            CreateMap<Property, PropertyListDTO>()
                .ForMember(d => d.City, opt => opt.MapFrom(x => x.City.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(x => x.City.Country))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(x => x.PropertyType.Name))
                .ForMember(d => d.FurnitureType, opt => opt.MapFrom(x => x.FurnitureType.Name))
                .ForMember(d => d.Photo, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsPrimary).ImageUrl));


            CreateMap<Property, PropertyDetailDTO>()
                .ForMember(d => d.City, opt => opt.MapFrom(x => x.City.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(x => x.City.Country))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(x => x.PropertyType.Name))
                .ForMember(d => d.FurnitureType, opt => opt.MapFrom(x => x.FurnitureType.Name));

            CreateMap<PropertyType, KeyValuePairDTO>().ReverseMap();
            CreateMap<FurnitureType, KeyValuePairDTO>().ReverseMap();
        }
    }
}
