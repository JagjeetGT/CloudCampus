using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StudentExtraActivityDetailRepository : RepositoryBase<ScStudentExtraActivityDetails>, IStudentExtraActivityDetailRepository
    {
        public StudentExtraActivityDetailRepository(IDatabaseFactory dataBaseFactory) : base(dataBaseFactory) { }
    }
    public interface IStudentExtraActivityDetailRepository : IRepository<ScStudentExtraActivityDetails>
    {
        
    }
}
