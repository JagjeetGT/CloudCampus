using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScFeeTermRepository : RepositoryBase<ScFeeTerm>, IScFeeTermRepository{public ScFeeTermRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScFeeTermRepository : IRepository<ScFeeTerm>{}}