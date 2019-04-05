using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(CustomerDTO), Destination = typeof(Domain.Entity.Customer))]
    public class CustomerToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public CustomerToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<CustomerDTO, Domain.Entity.Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                //.ForMember(dest => dest.OrganizationId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.OrganizationId))
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => 1))
                //.ForMember(dest => dest.CustomerStatusId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.CustomerStatusId))
                .ForMember(dest => dest.CustomerStatusId, opt => opt.MapFrom(src => 1))
                //.ForMember(dest => dest.CustomerTypeId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.CustomerTypeId))
                .ForMember(dest => dest.CustomerTypeId, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => (PersonDTO)src))
                .ForMember(dest => dest.CustomerAttribute, opt => opt.MapFrom(src => src.Attributes))
                .ForMember(dest => dest.CustomerNote, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .ForMember(dest => dest.PersonId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerStatus, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerType, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerHistory, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerSearch, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
