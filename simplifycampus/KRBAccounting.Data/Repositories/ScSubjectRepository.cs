using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScSubjectRepository : RepositoryBase<ScSubject>, IScSubjectRepository{public ScSubjectRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScSubjectRepository : IRepository<ScSubject>{}}