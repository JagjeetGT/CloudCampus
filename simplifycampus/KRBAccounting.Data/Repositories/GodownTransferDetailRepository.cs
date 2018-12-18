using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class GodownTransferDetailRepository : RepositoryBase<GodownTransferDetail>, IGodownTransferDetailRepository{public GodownTransferDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IGodownTransferDetailRepository : IRepository<GodownTransferDetail>{}}