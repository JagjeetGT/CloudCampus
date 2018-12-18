using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;


namespace KRBAccounting.Data.Repositories
{public class ExpiryBreakageMasterRepository : RepositoryBase<ExpiryBreakageMaster>, IExpiryBreakageMasterRepository{public ExpiryBreakageMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IExpiryBreakageMasterRepository : IRepository<ExpiryBreakageMaster>{}}