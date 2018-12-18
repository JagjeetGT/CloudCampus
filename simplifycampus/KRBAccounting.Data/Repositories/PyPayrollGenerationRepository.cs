using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyPayrollGenerationRepository : RepositoryBase<PyPayrollGeneration>, IPyPayrollGenerationRepository{public PyPayrollGenerationRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyPayrollGenerationRepository : IRepository<PyPayrollGeneration>{}}