using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class BillingTermRepository : RepositoryBase<BillingTerm>, IBillingTermRepository
    {
        public BillingTermRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsSalesBillingNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.Description.ToLower() == Name && x.Type=="S").Any();
            return !groupName;
        }
        public bool IsSalesBillingCodeAvailable(int code)
        {

            var groupName = this.GetMany(x => x.Code == code && x.Type=="S").Any();
            return !groupName;
        }
        public bool IsPurchaseBillingNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.Description.ToLower() == Name && x.Type=="P").Any();
            return !groupName;
        }
        public bool IsPurchaseBillingCodeAvailable(int code)
        {

            var groupName = this.GetMany(x => x.Code == code && x.Type == "P").Any();
            return !groupName;
        }
    }

    public interface IBillingTermRepository : IRepository<BillingTerm>
    {
        bool IsSalesBillingNameAvailable(string name);
        bool IsSalesBillingCodeAvailable(int code);
        bool IsPurchaseBillingNameAvailable(string name);
        bool IsPurchaseBillingCodeAvailable(int code);
    }
}
