using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScBoaderMappingRepository : RepositoryBase<ScBoaderMapping>, IScBoaderMappingRepository
    {
        public ScBoaderMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScBoaderMappingRepository : IRepository<ScBoaderMapping>
    {
    }
}
