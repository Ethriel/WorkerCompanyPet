﻿using AutoMapper;
using WorkerCompany.BLL.DTO;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.BLL.MapperProfiles
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<Worker, WorkerDTO>()
                .ForMember(wdto => wdto.CompanyName, o => o.MapFrom(w => w.Company.CompanyName));
            //.ForMember(wdto => wdto.TimeUpdated, o => o.MapFrom(w => w.TimeUpdated.ToShortDateString()))

            CreateMap<WorkerDTO, Worker>();
            //.ForMember(w => w.TimeUpdated, o => o.MapFrom(wdto => DateTime.Parse(wdto.TimeUpdated)))
        }
    }
}
