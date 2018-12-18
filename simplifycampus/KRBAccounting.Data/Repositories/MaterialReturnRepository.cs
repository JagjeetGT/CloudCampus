using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class MaterialReturnRepository : RepositoryBase<MaterialReturn>, IMaterialReturnRepository{public MaterialReturnRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IMaterialReturnRepository : IRepository<MaterialReturn>{}}