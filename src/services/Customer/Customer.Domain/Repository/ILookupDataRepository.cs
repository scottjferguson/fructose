using Core.Framework;
using System.Collections.Generic;

namespace Customer.Domain.Repository
{
    public interface ILookupDataRepository
    {
        List<LookupData> GetAllCustomerStatuses();
        List<LookupData> GetAllCustomerTypes();
    }
}
