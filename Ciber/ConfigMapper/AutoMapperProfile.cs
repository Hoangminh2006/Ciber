using AutoMapper;
using CiberCommon.Dto;
using CiberCommon.Model;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}
