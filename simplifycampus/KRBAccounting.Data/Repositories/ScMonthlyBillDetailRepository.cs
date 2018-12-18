using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScMonthlyBillStudentRepository : RepositoryBase<ScMonthlyBillStudent>, IScMonthlyBillStudentRepository
    {
        public ScMonthlyBillStudentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScMonthlyBillStudentRepository : IRepository<ScMonthlyBillStudent>
    {
    }
}
