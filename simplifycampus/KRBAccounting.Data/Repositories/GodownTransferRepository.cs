using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class GodownTransferRepository : RepositoryBase<GodownTransfer>, IGodownTransferRepository{public GodownTransferRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IGodownTransferRepository : IRepository<GodownTransfer>{}}