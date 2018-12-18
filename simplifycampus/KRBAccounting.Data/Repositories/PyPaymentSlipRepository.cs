using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class PyPaymentSlipRepository : RepositoryBase<PyPaymentSlip>, IPyPaymentSlipRepository{public PyPaymentSlipRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IPyPaymentSlipRepository : IRepository<PyPaymentSlip>{}}