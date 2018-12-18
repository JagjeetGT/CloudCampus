using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBookReceivedRepository : RepositoryBase<ScBookReceived>, IScBookReceivedRepository{public ScBookReceivedRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBookReceivedRepository : IRepository<ScBookReceived>{}}