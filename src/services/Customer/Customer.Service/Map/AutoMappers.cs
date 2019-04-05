using AutoMapper;
using Core.Mapping;

namespace Customer.Microservice.Map
{
    [Map]
    public class AutoMappers : IMap
    {
        private readonly IMapperConfigurationExpression _cfg;

        public AutoMappers(IMapperConfigurationExpression cfg)
        {
            _cfg = cfg;
        }

        public void Configure()
        {

        }
    }
}
