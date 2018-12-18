using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SMSSettingsRepository : RepositoryBase<SMSSettings>, ISMSSettingsRepository{public SMSSettingsRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISMSSettingsRepository : IRepository<SMSSettings>{}}