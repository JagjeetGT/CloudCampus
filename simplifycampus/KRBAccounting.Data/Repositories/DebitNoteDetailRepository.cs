using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class DebitNoteDetailRepository : RepositoryBase<DebitNoteDetail>, IDebitNoteDetailRepository
    {
        public DebitNoteDetailRepository(IDatabaseFactory databaseFactory)
            :base(databaseFactory)
        {
        }
    }

    public interface IDebitNoteDetailRepository : IRepository<DebitNoteDetail>
    {
    }
}
