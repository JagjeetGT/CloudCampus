using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class BoardExamDetailRepository : RepositoryBase<BoardExamDetail>, IBoardExamDetailRepository
    {
       public BoardExamDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBoardExamDetailRepository : IRepository<BoardExamDetail>
    {
    }
}