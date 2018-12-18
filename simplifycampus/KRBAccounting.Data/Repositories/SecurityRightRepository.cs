using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SecurityRightRepository : RepositoryBase<SecurityRight>, ISecurityRightRepository
    {
        public SecurityRightRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISecurityRightRepository : IRepository<SecurityRight>
    {
        
    }
}
