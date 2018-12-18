using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBookReceivedDetailsRepository : RepositoryBase<ScBookReceivedDetails>, IScBookReceivedDetailsRepository{public ScBookReceivedDetailsRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBookReceivedDetailsRepository : IRepository<ScBookReceivedDetails>{}}