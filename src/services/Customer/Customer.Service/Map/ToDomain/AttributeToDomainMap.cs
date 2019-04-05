using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(AttributeDTO), Destination = typeof(CustomerAttribute))]
    public class AttributeToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public AttributeToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<AttributeDTO, CustomerAttribute>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAttributeTypeId, opt => opt.MapFrom(dest => 1))
                //.ForMember(dest => dest.CustomerAttributeTypeId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.CustomerAttributeTypeId))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(dest => dest.Value))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerAttributeType, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
