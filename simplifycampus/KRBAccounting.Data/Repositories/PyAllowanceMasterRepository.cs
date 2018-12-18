using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyAllowanceMasterRepository : RepositoryBase<PyAllowanceMaster>, IPyAllowanceMasterRepository{public PyAllowanceMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyAllowanceMasterRepository : IRepository<PyAllowanceMaster>{}}