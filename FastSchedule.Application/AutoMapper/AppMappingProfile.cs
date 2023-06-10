using AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Domain.Models;
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
            CreateMap<DailyTask, DailyTaskDto>().ReverseMap();
            CreateMap<WeeklyTask, WeeklyTaskDto>().ReverseMap();
            CreateMap<MonthlyTask, MonthlyTaskDto>().ReverseMap();
            CreateMap<User, User>().ReverseMap();
        }
    }
}
