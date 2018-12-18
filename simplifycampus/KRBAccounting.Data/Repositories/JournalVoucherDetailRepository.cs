using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class JournalVoucherDetailRepository : RepositoryBase<JournalVoucherDetail>, IJournalVoucherDetailRepository
    {
        public JournalVoucherDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IJournalVoucherDetailRepository : IRepository<JournalVoucherDetail>
    {
        
    }
}
