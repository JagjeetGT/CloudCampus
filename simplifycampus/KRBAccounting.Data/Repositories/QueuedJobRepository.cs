using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class QueuedJobRepository : RepositoryBase<QueuedJob>, IQueuedJobRepository
    {
        public QueuedJobRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IQueuedJobRepository : IRepository<QueuedJob>
    {

    }
}
