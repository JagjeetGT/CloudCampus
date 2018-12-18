using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBillTransactionRepository : RepositoryBase<ScBillTransaction>, IScBillTransactionRepository{public ScBillTransactionRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBillTransactionRepository : IRepository<ScBillTransaction>{}}