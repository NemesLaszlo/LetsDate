using AutoMapper;
using LestDate_API.DTOs;
using LestDate_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LestDate_API.MapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}
