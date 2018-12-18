using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class UdfEntryRepository : RepositoryBase<UDFEntry>, IUdfEntryRepository
    {
        public UdfEntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }

    public interface IUdfEntryRepository : IRepository<UDFEntry>
    {
       
    }
}
