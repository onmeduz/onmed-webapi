using AutoMapper;
using OnMed.Domain.Entities.Categories;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.WebApi.Configurations;

public class MapperConfiration : Profile
{
    public MapperConfiration()
    {
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<HospitalCreateDto, Hospital>().ReverseMap();
    }

}