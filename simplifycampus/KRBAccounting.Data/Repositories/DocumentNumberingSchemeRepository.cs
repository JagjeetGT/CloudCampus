using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class DocumentNumberingSchemeRepository : RepositoryBase<DocumentNumberingScheme>, IDocumentNumeringSchemeRepository
    {
        public DocumentNumberingSchemeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IDocumentNumeringSchemeRepository : IRepository<DocumentNumberingScheme>
    {
        
    }
}
