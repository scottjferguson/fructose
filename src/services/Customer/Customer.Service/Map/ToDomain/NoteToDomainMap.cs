using AutoMapper;
using Core.Mapping;
using Core.Plugins.AutoMapper.Extensions;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;

namespace Customer.Microservice.Map.ToDomain
{
    [Map(Source = typeof(NoteDTO), Destination = typeof(CustomerNote))]
    public class NoteToDomainMap : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public NoteToDomainMap(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {
            _cfg.CreateMap<NoteDTO, CustomerNote>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerNoteTypeId, opt => opt.MapFrom(dest => 1))
                //.ForMember(dest => dest.CustomerNoteTypeId, opt => opt.ConvertUsing<LookupDataValueToKeyDatabaseResolver<int>, int>(src => src.CustomerAttributeTypeId))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(dest => dest.Value))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerNoteType, opt => opt.Ignore())
                .ForMember(dest => dest.RowVersion, opt => opt.Ignore())
                .IgnoreAuditFields()
                ;
        }
    }
}
