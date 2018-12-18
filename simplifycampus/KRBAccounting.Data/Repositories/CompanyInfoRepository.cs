using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class CompanyInfoRepository : RepositoryBase<CompanyInfo>, ICompanyInfoRepository
    {
        public CompanyInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICompanyInfoRepository : IRepository<CompanyInfo>
    {
        
    }
}
