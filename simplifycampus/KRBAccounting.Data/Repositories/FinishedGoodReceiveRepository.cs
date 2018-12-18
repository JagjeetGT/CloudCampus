using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{public class FinishedGoodReceiveRepository : RepositoryBase<FinishedGoodReceive>, IFinishedGoodReceiveRepository{public FinishedGoodReceiveRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IFinishedGoodReceiveRepository : IRepository<FinishedGoodReceive>{}}