using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStaffSubjectMappingRepository : RepositoryBase<ScStaffSubjectMapping>, IScStaffSubjectMappingRepository{public ScStaffSubjectMappingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStaffSubjectMappingRepository : IRepository<ScStaffSubjectMapping>{}}