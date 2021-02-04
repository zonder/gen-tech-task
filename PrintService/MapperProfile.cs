using System;
using AutoMapper;
using PrintService.Api.ViewModels;
using PrintService.Domain.Dtos;

namespace PrintService.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PrintAtRequestViewModel, PrintAtTaskDto>()
                .ForMember(x => x.PrintAt, opt => opt.MapFrom(x => x.PrintAt))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
        }
    }
}
