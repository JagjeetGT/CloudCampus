using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBookIssuedRepository : RepositoryBase<ScBookIssued>, IScBookIssuedRepository{public ScBookIssuedRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBookIssuedRepository : IRepository<ScBookIssued>{}}