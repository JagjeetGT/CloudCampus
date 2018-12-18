using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScSalaryHeadRepository : RepositoryBase<ScSalaryHead>, IScSalaryHeadRepository
    {
        public ScSalaryHeadRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void UpdateSortOrder(string[] itemId)
        {
            var i = 1;
            foreach (var data in itemId.Select(s => Convert.ToInt32(s)).Select(id => this.GetById(x => x.Id == id)))
            {
                data.DisplayOrder = i;
                this.Update(data);
                i++;
            }
        }

        //public void UpdateSortOrder(string linkIds)
        //{
        //    for (int i = 0; i < linkIds.Length; i++)
        //    {
        //        var a = int.Parse(linkIds[i]);
        //        //    Link link = GetLinkById());
        //        //    _context.Links.Attach(link);
        //        //    link.OrderNumber = i + 1;
        //        //    _context.SaveChanges();
        //    }
        //}
    }
    public interface IScSalaryHeadRepository : IRepository<ScSalaryHead>
    {
        void UpdateSortOrder(string[] itemId);

    }
}
