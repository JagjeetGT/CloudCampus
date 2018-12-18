using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;
namespace KRBAccounting.Data.Repositories
{
    public class OpeningStudentRepository : RepositoryBase<OpeningStudent>, IOpeningStudentRepository
    {
        public OpeningStudentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IOpeningStudentRepository : IRepository<OpeningStudent>
    {
    }
}
