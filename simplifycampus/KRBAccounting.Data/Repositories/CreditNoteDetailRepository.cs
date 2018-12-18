using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class CreditNoteDetailRepository : RepositoryBase<CreditNoteDetail>, ICreditNoteDetailRepository
    {
        public CreditNoteDetailRepository(IDatabaseFactory databaseFactory)
            :base(databaseFactory)
        {
        }
    }

    public interface ICreditNoteDetailRepository : IRepository<CreditNoteDetail>
    {
    }
}
