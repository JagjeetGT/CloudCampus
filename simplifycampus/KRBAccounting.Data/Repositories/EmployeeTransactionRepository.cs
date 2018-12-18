using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class EmployeeTransactionRepository : RepositoryBase<EmployeeTransaction>, IEmployeeTransactionRepository
    {
        public EmployeeTransactionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
      
        
    }

    public interface IEmployeeTransactionRepository : IRepository<EmployeeTransaction>
    {
        
    }
}
