using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScFeeReceiptRepository : RepositoryBase<ScFeeReceipt>, IScFeeReceiptRepository{public ScFeeReceiptRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScFeeReceiptRepository : IRepository<ScFeeReceipt>{}}