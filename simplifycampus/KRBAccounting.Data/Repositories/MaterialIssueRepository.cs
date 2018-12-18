using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialIssueRepository : RepositoryBase<MaterialIssue>, IMaterialIssueRepository{public MaterialIssueRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IMaterialIssueRepository : IRepository<MaterialIssue>{}}