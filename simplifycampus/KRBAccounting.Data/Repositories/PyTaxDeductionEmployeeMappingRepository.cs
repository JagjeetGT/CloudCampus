using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class PyTaxDeductionEmployeeMappingRepository : RepositoryBase<PyTaxDeductionEmployeeMapping>, IPyTaxDeductionEmployeeMappingRepository
    {
        public PyTaxDeductionEmployeeMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPyTaxDeductionEmployeeMappingRepository : IRepository<PyTaxDeductionEmployeeMapping>
    {
    }
}
