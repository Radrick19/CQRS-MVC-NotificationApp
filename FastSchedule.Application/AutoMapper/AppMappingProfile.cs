using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.AutoMapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<OnetimeTask, OnetimeTaskDto>().ReverseMap();
            CreateMap<WeeklyTask, WeeklyTaskDto>().ReverseMap();
            CreateMap<MonthlyTask, MonthlyTaskDto>().ReverseMap();
            CreateMap<EverydayTask, EverydayTaskDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
