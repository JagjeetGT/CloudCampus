using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyPayrollGenerationDetailRepository : RepositoryBase<PyPayrollGenerationDetail>, IPyPayrollGenerationDetailRepository{public PyPayrollGenerationDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyPayrollGenerationDetailRepository : IRepository<PyPayrollGenerationDetail>{}}