using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialIssueDetailRepository : RepositoryBase<MaterialIssueDetail>, IMaterialIssueDetailRepository{public MaterialIssueDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IMaterialIssueDetailRepository : IRepository<MaterialIssueDetail>{}}