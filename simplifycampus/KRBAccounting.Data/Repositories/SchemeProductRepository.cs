using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SchemeProductRepository : RepositoryBase<SchemeProduct>, ISchemeProductRepository{public SchemeProductRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISchemeProductRepository : IRepository<SchemeProduct>{}}