using System;
using System.Collections.Generic;
using System.Linq;
using KRBAccounting.Data.Infrastructure;
using System.Text;
using KRBAccounting.Domain.Entities;


namespace KRBAccounting.Data.Repositories
{
    public class ScLibraryLateFineRepository:RepositoryBase<ScLibraryLateFine>,IScLibraryLateFineRepository
    {
        public ScLibraryLateFineRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}
    }

    public interface IScLibraryLateFineRepository : IRepository<ScLibraryLateFine>
    {
    }
}
