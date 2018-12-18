using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{public class ExpiryBreakageDetailRepository : RepositoryBase<ExpiryBreakageDetail>, IExpiryBreakageDetailRepository{public ExpiryBreakageDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IExpiryBreakageDetailRepository : IRepository<ExpiryBreakageDetail>{}}