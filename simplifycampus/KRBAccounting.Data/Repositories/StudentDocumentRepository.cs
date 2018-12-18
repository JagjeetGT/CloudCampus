using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{

    public class StudentDocumentRepository : RepositoryBase<StudentDocument>, IStudentDocumentRepository
    {
        public StudentDocumentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IStudentDocumentRepository : IRepository<StudentDocument>
    {

    }
}
