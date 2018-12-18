using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SmsGroupDetailRepository : RepositoryBase<SmsGroupDetail>, ISmsGroupDetailRepository{public SmsGroupDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISmsGroupDetailRepository : IRepository<SmsGroupDetail>{}}