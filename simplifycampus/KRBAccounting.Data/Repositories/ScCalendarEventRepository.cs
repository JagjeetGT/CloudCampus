using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScCalendarEventRepository : RepositoryBase<ScCalendarEvent>, IScCalendarEventRepository
    {
        public ScCalendarEventRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
      
    }

    public interface IScCalendarEventRepository : IRepository<ScCalendarEvent>
    {
       
    }
}
