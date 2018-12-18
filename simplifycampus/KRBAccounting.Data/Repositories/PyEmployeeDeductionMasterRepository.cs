using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyEmployeeDeductionMasterRepository : RepositoryBase<PyEmployeeDeductionMaster>, IPyEmployeeDeductionMasterRepository{public PyEmployeeDeductionMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyEmployeeDeductionMasterRepository : IRepository<PyEmployeeDeductionMaster>{}}