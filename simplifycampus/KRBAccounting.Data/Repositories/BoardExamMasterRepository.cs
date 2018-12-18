using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class BoardExamMasterRepository : RepositoryBase<BoardExamMaster>, IBoardExamMasterRepository
    {
       public BoardExamMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBoardExamMasterRepository : IRepository<BoardExamMaster>
    {
    }
}
