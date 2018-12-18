using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStudentFeeRateMasterRepository : RepositoryBase<ScStudentFeeRateMaster>, IScStudentFeeRateMasterRepository{public ScStudentFeeRateMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStudentFeeRateMasterRepository : IRepository<ScStudentFeeRateMaster>{}}