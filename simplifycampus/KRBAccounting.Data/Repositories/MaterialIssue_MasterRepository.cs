using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialIssue_MasterRepository : RepositoryBase<ScMaterialIssueMaster>, IMaterialIssue_MasterRepository
{
public MaterialIssue_MasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory)
{
}
}
public interface IMaterialIssue_MasterRepository : IRepository<ScMaterialIssueMaster>
{
}
}
