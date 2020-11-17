using AutoMapper;
using WorkerCompany.BLL.DTO;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.BLL.MapperProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>();

            CreateMap<CompanyDTO, Company>();
        }
    }
}
