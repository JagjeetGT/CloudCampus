using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBoaderRepository : RepositoryBase<ScBoader>, IScBoaderRepository{public ScBoaderRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBoaderRepository : IRepository<ScBoader>{}}