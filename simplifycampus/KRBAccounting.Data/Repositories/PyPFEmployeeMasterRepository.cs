using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyPFEmployeeMasterRepository : RepositoryBase<PyPFEmployeeMaster>, IPyPFEmployeeMasterRepository{public PyPFEmployeeMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyPFEmployeeMasterRepository : IRepository<PyPFEmployeeMaster>{}}