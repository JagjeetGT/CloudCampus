using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ExtraChargeRepository : RepositoryBase<ExtraCharge>, IExtraChargeRepository
    {
        public ExtraChargeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool IsDescriptionAvailable(string description)
        {

            var DescriptionName = this.GetMany(x => x.Description==description).Any();
            return !DescriptionName;
        }

        public bool IsDescriptionAvailableEdit(string description,int id)
        {
            var olddescritpion = this.GetById(id);
            var DescriptionName = this.GetMany(x => x.Description == description && x.Description!=olddescritpion.Description).Any();
            return !DescriptionName;
        }
    }

    public interface IExtraChargeRepository : IRepository<ExtraCharge>
    {
        bool IsDescriptionAvailable(string description);
        bool IsDescriptionAvailableEdit(string description, int id);
    } 
} 
 