using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyEmployeeSalaryMasterRepository : RepositoryBase<PyEmployeeSalaryMaster>, IPyEmployeeSalaryMasterRepository{public PyEmployeeSalaryMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyEmployeeSalaryMasterRepository : IRepository<PyEmployeeSalaryMaster>{}}