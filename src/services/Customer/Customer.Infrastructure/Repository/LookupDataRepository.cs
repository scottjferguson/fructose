using Core.Caching;
using Core.Data;
using Core.Framework;
using Core.Framework.Attributes;
using Customer.Domain.Entity;
using Customer.Domain.Repository;
using Customer.Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace Customer.Infrastructure.Repository
{
    [Injectable]
    public class LookupDataRepository : RepositoryBase, ILookupDataRepository
    {
        private readonly IAbstractRepositoryFactory _abstractRepositoryFactory;
        private readonly ICache _cache;

        public LookupDataRepository(IAbstractRepositoryFactory abstractRepositoryFactory, ICacheFactory cacheFactory)
        {
            _abstractRepositoryFactory = abstractRepositoryFactory;
            _cache = cacheFactory.Create();
        }

        public List<LookupData> GetAllCustomerStatuses()
        {
            List<LookupData> customerStatuses =
                _cache.GetOrAdd(Constants.CacheKey.CustomerStatuses, () =>
                {
                    List<LookupData> lookupDataToCache = _abstractRepositoryFactory.Create(FructoseRepository)
                        .Create<CustomerStatus>().GetAll()
                        .Select(cs => new LookupData
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Description = cs.Description,
                            Code = cs.Code,
                            DisplayName = cs.DisplayName,
                            DisplayOrder = cs.DisplayOrder,
                            IsActive = cs.IsActive,
                            CreatedBy = cs.CreatedBy,
                            CreatedDate = cs.CreatedDate,
                            ModifiedBy = cs.ModifiedBy,
                            ModifiedDate = cs.ModifiedDate
                        }).ToList();

                    return lookupDataToCache;
                });

            return customerStatuses;
        }

        public List<LookupData> GetAllCustomerTypes()
        {
            List<LookupData> customerTypes =
                _cache.GetOrAdd(Constants.CacheKey.CustomerTypes, () =>
                {
                    List<LookupData> lookupDataToCache = _abstractRepositoryFactory.Create(FructoseRepository)
                        .Create<CustomerType>().GetAll()
                        .Select(cs => new LookupData
                        {
                            Id = cs.Id,
                            Name = cs.Name,
                            Description = cs.Description,
                            Code = cs.Code,
                            DisplayName = cs.DisplayName,
                            DisplayOrder = cs.DisplayOrder,
                            IsActive = cs.IsActive,
                            CreatedBy = cs.CreatedBy,
                            CreatedDate = cs.CreatedDate,
                            ModifiedBy = cs.ModifiedBy,
                            ModifiedDate = cs.ModifiedDate
                        }).ToList();

                    return lookupDataToCache;
                });

            return customerTypes;
        }
    }
}
