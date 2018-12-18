using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScEmployeeInfoRepository : RepositoryBase<ScEmployeeInfo>, IScEmployeeInfoRepository{public ScEmployeeInfoRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScEmployeeInfoRepository : IRepository<ScEmployeeInfo>{}}