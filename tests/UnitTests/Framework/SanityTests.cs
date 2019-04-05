using AutoMapper;
using Core.Mapping;
using Core.Plugins.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.Base;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Framework
{
    public class SanityTests : xUnitTestBase
    {
        public SanityTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void Mappers_ShouldHaveValidConfiguration_UnderAllCondition()
        {
            InitializeMaps();

            NotThrows(() => Mapper.Configuration.AssertConfigurationIsValid());
        }

        #region Private

        private void InitializeMaps()
        {
            List<Type> maps = new AssemblyScanner().GetApplicationTypesWithAttribute<MapAttribute>();
            
            Mapper.Initialize(cfg =>
            {
                maps.Select(type => Activator.CreateInstance(type, cfg))
                    .OfType<IMap>().ToList()
                    .ForEach(map => map.Configure());
            });
        }

        #endregion
    }
}
