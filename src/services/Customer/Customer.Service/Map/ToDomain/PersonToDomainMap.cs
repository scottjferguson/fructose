using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(PersonDTO), Destination = typeof(Person))]
    public class PersonToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public PersonToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<PersonDTO, Person>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.PersonAddress, opt => opt.Ignore())
                .ForMember(dest => dest.PersonEmail, opt => opt.Ignore())
                .ForMember(dest => dest.PersonHistory, opt => opt.Ignore())
                .ForMember(dest => dest.PersonPhone, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
