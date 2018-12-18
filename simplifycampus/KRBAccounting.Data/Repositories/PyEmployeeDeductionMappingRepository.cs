using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class PyEmployeeDeductionMappingRepository : RepositoryBase<PyEmployeeDeductionMapping>, IPyEmployeeDeductionMappingRepository{
    public PyEmployeeDeductionMappingRepository(IDatabaseFactory databaseFactory)
        : base(databaseFactory){}}
    public interface IPyEmployeeDeductionMappingRepository : IRepository<PyEmployeeDeductionMapping>{}}