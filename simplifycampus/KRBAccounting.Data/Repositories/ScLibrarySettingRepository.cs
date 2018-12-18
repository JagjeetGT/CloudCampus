using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScLibrarySettingRepository : RepositoryBase<ScLibrarySetting>, IScLibrarySettingRepository{public ScLibrarySettingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScLibrarySettingRepository : IRepository<ScLibrarySetting>{}}