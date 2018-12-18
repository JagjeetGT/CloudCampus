using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStudentFeeRateDetailRepository : RepositoryBase<ScStudentFeeRateDetail>, IScStudentFeeRateDetailRepository{public ScStudentFeeRateDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStudentFeeRateDetailRepository : IRepository<ScStudentFeeRateDetail>{}}