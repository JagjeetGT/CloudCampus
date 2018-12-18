using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class PyEmployeeSalaryAllowanceMappingRepository : RepositoryBase<PyEmployeeSalaryAllowanceMapping>, IPyEmployeeSalaryAllowanceMappingRepository
    {
        public PyEmployeeSalaryAllowanceMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPyEmployeeSalaryAllowanceMappingRepository : IRepository<PyEmployeeSalaryAllowanceMapping>
    {
    }
}
