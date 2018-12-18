using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SmsTemplatesRepository : RepositoryBase<SmsTemplates>, ISmsTemplatesRepository{public SmsTemplatesRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISmsTemplatesRepository : IRepository<SmsTemplates>{}}