using System.Linq;
using App.client;
using AutoMapper;
using Domain.entity;

namespace App
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Client, ClientDto>()
            .ForMember(x => x.Numbers, y => y.MapFrom(z => z.clientNumbers.Select(a=>a.client).ToList()));
            CreateMap<ClientNumber, ClientNumberDto>();
            CreateMap<Number, NumberDto>();
        }
    }
}