using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStudentFeeTermRepository : RepositoryBase<ScStudentFeeTerm>, IScStudentFeeTermRepository{public ScStudentFeeTermRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStudentFeeTermRepository : IRepository<ScStudentFeeTerm>{}}