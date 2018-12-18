using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyTaxDeductionPatternRepository : RepositoryBase<PyTaxDeductionPattern>, IPyTaxDeductionPatternRepository{public PyTaxDeductionPatternRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyTaxDeductionPatternRepository : IRepository<PyTaxDeductionPattern>{}}