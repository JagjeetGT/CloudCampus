using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{public class ProductOpeningRepository : RepositoryBase<ProductOpening>, IProductOpeningRepository{public ProductOpeningRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IProductOpeningRepository : IRepository<ProductOpening>{}}