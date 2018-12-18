using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class FinishedGoodReturnDetailRepository : RepositoryBase<FinishedGoodReturnDetail>, IFinishedGoodReturnDetailRepository{public FinishedGoodReturnDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IFinishedGoodReturnDetailRepository : IRepository<FinishedGoodReturnDetail>{}}