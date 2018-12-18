using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SmsLogRepository : RepositoryBase<SmsLog>, ISmsLogRepository{public SmsLogRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISmsLogRepository : IRepository<SmsLog>{}}