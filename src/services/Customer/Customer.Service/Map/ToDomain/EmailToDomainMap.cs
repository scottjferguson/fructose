using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(EmailDTO), Destination = typeof(Email))]
    public class EmailToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public EmailToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<EmailDTO, Email>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.EmailTypeId, opt => opt.MapFrom(dest => 1))
                //.ForMember(dest => dest.EmailTypeId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.EmailType))
                .ForMember(dest => dest.EmailAddress, opt => opt.Ignore())
                .ForMember(dest => dest.EmailHistory, opt => opt.Ignore())
                .ForMember(dest => dest.PersonEmail, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
