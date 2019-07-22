using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Data.LookupData;
using Core.Plugins.AutoMapper.Data.Resolvers.DatabaseResolver;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Infrastructure;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(AddressDTO), Destination = typeof(Address))]
    public class AddressToDomainMap : Profile, IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public AddressToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<AddressDTO, Address>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.AddressTypeId, opt => opt.MapFrom<LookupDataValueToKeyDatabaseResolver<int>, LookupDataByValue>(src => new LookupDataByValue(src.AddressType, Constants.Configuration.ApplicationSettings.DatabaseConnections.Fructose, nameof(AddressType))))
                .ForMember(dest => dest.GeographyId, opt => opt.Ignore())
                .ForMember(dest => dest.AddressHistory, opt => opt.Ignore())
                .ForMember(dest => dest.PersonAddress, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .ForMember(dest => dest.AddressType, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
