using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SchemeFreeProductRepository : RepositoryBase<SchemeFreeProduct>, ISchemeFreeProductRepository{public SchemeFreeProductRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISchemeFreeProductRepository : IRepository<SchemeFreeProduct>{}}