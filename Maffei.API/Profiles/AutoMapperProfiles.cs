using AutoMapper;
using Maffei.API.DataModels;
using Maffei.API.Dtos;

namespace Maffei.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Abone, AboneDto>()
                .ForMember(x => x.Kdv, o => o.MapFrom(s => s.Kdv.KdvRatio))
                  .ForMember(x => x.CurrencyUnit, o => o.MapFrom(s => s.CurrencyUnit.Money));

            CreateMap<Kdv, KdvDto>();
            CreateMap<CurrencyUnit , CurrencyUnitDto>();
            CreateMap<Abone, AddAboneDto>().ReverseMap();
            CreateMap<Abone, UpdateAboneDto>().ReverseMap();
      
        }
    }
}
