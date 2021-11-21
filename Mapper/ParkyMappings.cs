using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParksAPI.Models;
using ParksAPI.Models.Dtos;
using ParksAPI.Models.DTOs;

namespace ParksAPI.Mapper
{
    public class ParkyMappings:Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();
            CreateMap<Trail, TrailCreateDTO>().ReverseMap();
            CreateMap<Trail, TrailDTO>().ReverseMap();
            CreateMap<Trail, TrailUpdateDTO>().ReverseMap();


        }
    }
}
