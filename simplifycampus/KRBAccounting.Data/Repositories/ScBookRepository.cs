using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBookRepository : RepositoryBase<ScBook>, IScBookRepository{public ScBookRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBookRepository : IRepository<ScBook>{}}