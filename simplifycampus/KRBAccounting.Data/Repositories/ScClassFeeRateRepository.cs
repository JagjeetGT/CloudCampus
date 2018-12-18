using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScClassFeeRateRepository : RepositoryBase<ScClassFeeRate>, IScClassFeeRateRepository{public ScClassFeeRateRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScClassFeeRateRepository : IRepository<ScClassFeeRate>{}}