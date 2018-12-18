using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScExtraActivityRepository : RepositoryBase<ScExtraActivity>, IScExtraActivityRepository{public ScExtraActivityRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScExtraActivityRepository : IRepository<ScExtraActivity>{}}