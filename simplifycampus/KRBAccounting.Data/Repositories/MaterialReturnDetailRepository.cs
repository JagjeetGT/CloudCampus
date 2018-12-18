using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialReturnDetailRepository : RepositoryBase<MaterialReturnDetail>, IMaterialReturnDetailRepository{public MaterialReturnDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IMaterialReturnDetailRepository : IRepository<MaterialReturnDetail>{}}