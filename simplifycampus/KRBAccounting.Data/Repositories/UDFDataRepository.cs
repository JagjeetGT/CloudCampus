using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class UDFDataRepository : RepositoryBase<UDFData>, IUDFDataRepository
    {
        public UDFDataRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IUDFDataRepository : IRepository<UDFData>
    {
        
    }
}
