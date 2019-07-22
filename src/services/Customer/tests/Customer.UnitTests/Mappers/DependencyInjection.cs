using AutoMapper;
using Core.Caching;
using Core.Data;
using Core.IoC;
using Core.Plugins.AutoMapper.Data.Resolvers.DatabaseResolver;
using Core.Plugins.Castle.Windsor.Wrappers;
using Customer.Domain.Entity;
using Customer.Microservice.DTO;
using Customer.Microservice.Map.ToDomain;
using Moq;
using System;
using System.Data;
using UnitTests.Base;
using Xunit;
using Xunit.Abstractions;

namespace Customer.UnitTests.Mappers
{
    public class DependencyInjection : xUnitTestBase
    {
        public DependencyInjection(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void MapFrom_ShouldUseResolverDependencies_WhenMapIsCalled()
        {
            const int expectedID = 2;
            string expectedValue = "Shipping";

            var container = new WindsorIoCContainer();

            var databaseMock = new Mock<IDatabase>();
            var databaseFactoryMock = new Mock<IDatabaseFactory>();
            var cacheMock = new Mock<ICache>();
            var cacheFactoryMock = new Mock<ICacheFactory>();

            databaseMock
                .Setup(mock => mock.Execute(It.IsAny<string>(), null))
                .Returns(GetMockData(expectedID, expectedValue));

            databaseFactoryMock
                .Setup(mock => mock.Create(It.IsAny<string>()))
                .Returns(databaseMock.Object);

            cacheFactoryMock
                .Setup(mock => mock.Create(null))
                .Returns(cacheMock.Object);

            var iocContainerSettings = new IoCContainerSettings
            {
                RegistrationLifetime = RegistrationLifetime.Transient,
                RegistrationName = nameof(LookupDataValueToKeyDatabaseResolver<int>),
                RegistrationNameFormat = "{0}"
            };

            container.Settings = iocContainerSettings;

            container.Register(new LookupDataValueToKeyDatabaseResolver<int>(databaseFactoryMock.Object, cacheFactoryMock.Object));

            Mapper.Initialize(cfg => {
                var addressToDomainMap = new AddressToDomainMap(cfg);
                addressToDomainMap.Configure();

                cfg.AddProfile(addressToDomainMap);
                cfg.ConstructServicesUsing(container.Resolve);
            });

            var input = new AddressDTO
            {
                AddressType = expectedValue
            };

            Address output = Mapper.Map<AddressDTO, Address>(input);

            Assert.NotNull(output);
            Assert.NotNull(output.AddressType);
            Assert.Equal(expectedID, output.AddressType.Id);
            Assert.Equal(expectedValue, output.AddressType.Name);

            Output(output);
        }

        #region Private

        private DataTable GetMockData(int expectedID, string expectedValue)
        {
            var dataTable = new DataTable();
            
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("DisplayName", typeof(string));
            dataTable.Columns.Add("DisplayOrder", typeof(int));
            dataTable.Columns.Add("IsActive", typeof(bool));
            dataTable.Columns.Add("CreatedBy", typeof(string));
            dataTable.Columns.Add("CreatedDate", typeof(DateTime));
            dataTable.Columns.Add("ModifiedBy", typeof(string));
            dataTable.Columns.Add("ModifiedDate", typeof(DateTime));

            DataRow dataRow = dataTable.NewRow();

            dataRow["ID"] = 1;
            dataRow["Name"] = "Home";
            dataRow["Description"] = "Indicates a Home address";
            dataRow["DisplayName"] = "Home";

            dataTable.Rows.Add(dataRow);

            DataRow dataRow2 = dataTable.NewRow();

            dataRow2["ID"] = expectedID;
            dataRow2["Name"] = expectedValue;
            dataRow2["Description"] = "Indicates a Billing address";
            dataRow2["DisplayName"] = "Billing";

            dataTable.Rows.Add(dataRow2);

            return dataTable;
        }

        #endregion
    }
}
