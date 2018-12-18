using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{public class SchemeRepository : RepositoryBase<Scheme>, ISchemeRepository{public SchemeRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISchemeRepository : IRepository<Scheme>{}}