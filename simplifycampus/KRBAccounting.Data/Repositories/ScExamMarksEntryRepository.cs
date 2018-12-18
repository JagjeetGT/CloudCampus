using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScExamMarksEntryRepository : RepositoryBase<ScExamMarksEntry>, IScExamMarksEntryRepository
    {
        public ScExamMarksEntryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScExamMarksEntryRepository : IRepository<ScExamMarksEntry>
    {
    }
}
