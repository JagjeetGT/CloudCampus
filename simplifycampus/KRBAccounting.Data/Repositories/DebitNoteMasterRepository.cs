using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class DebitNoteMasterRepository : RepositoryBase<DebitNoteMaster>, IDebitNoteMasterRepository
    {
        public DebitNoteMasterRepository(IDatabaseFactory databaseFactory)
            :base(databaseFactory)
        {
        }
    }

    public interface IDebitNoteMasterRepository : IRepository<DebitNoteMaster>
    {
    }
}
