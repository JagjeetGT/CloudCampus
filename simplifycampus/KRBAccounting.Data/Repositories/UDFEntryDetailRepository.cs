using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class UdfEntryDetailRepository : RepositoryBase<UDFEntryDetail>, IUdfEntryDetailRepository
    {
        public UdfEntryDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }

    public interface IUdfEntryDetailRepository : IRepository<UDFEntryDetail>
    {
       
    }
}
