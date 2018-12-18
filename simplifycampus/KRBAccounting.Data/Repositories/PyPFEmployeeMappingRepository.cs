using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class PyPFEmployeeMappingRepository : RepositoryBase<PyPFEmployeeMapping>, IPyPFEmployeeMappingRepository
    {
        public PyPFEmployeeMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPyPFEmployeeMappingRepository : IRepository<PyPFEmployeeMapping>
    {
    }
}
