using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SmsGroupRepository : RepositoryBase<SmsGroup>, ISmsGroupRepository{public SmsGroupRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISmsGroupRepository : IRepository<SmsGroup>{}}