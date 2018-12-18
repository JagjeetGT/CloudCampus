using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScMaterialReturnDetailsRepository : RepositoryBase<ScMaterialReturnDetails>, IScMaterialReturnDetailsRepository{public ScMaterialReturnDetailsRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScMaterialReturnDetailsRepository : IRepository<ScMaterialReturnDetails>{}}