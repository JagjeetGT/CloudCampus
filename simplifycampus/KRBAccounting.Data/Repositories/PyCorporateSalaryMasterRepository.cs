using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyCorporateSalaryMasterRepository : RepositoryBase<PyCorporateSalaryMaster>, IPyCorporateSalaryMasterRepository{public PyCorporateSalaryMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyCorporateSalaryMasterRepository : IRepository<PyCorporateSalaryMaster>{}}