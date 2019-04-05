using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(PhoneDTO), Destination = typeof(Phone))]
    public class PhoneToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public PhoneToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<PhoneDTO, Phone>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.PhoneTypeId, opt => opt.MapFrom(dest => 1))
                //.ForMember(dest => dest.PhoneTypeId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.PhoneType))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(dest => dest.Value))
                .ForMember(dest => dest.PhoneNumberNumeric, opt => opt.MapFrom(dest => dest.ValueNumeric))
                .ForMember(dest => dest.PersonPhone, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneHistory, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
