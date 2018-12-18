using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyDeductionMasterRepository : RepositoryBase<PyDeductionMaster>, IPyDeductionMasterRepository{public PyDeductionMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyDeductionMasterRepository : IRepository<PyDeductionMaster>{}}