using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialIssue_DetailsRepository : RepositoryBase<ScMaterialIssueDetails>, IMaterialIssue_DetailsRepository{public MaterialIssue_DetailsRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IMaterialIssue_DetailsRepository : IRepository<ScMaterialIssueDetails>{}}