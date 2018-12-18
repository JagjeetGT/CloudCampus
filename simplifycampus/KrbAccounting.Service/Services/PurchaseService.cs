using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data;
using KRBAccounting.Service.Models.Purchase;

namespace KRBAccounting.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly DataContext db = new DataContext();
        public PurchaseService()
        {

        }

        public IEnumerable<ProductBatchSalesViewModel> GetProductBatchList(int productId)
        {
            var expDateAction = db.SystemControls.FirstOrDefault().ExpiredProduct;
            var data = (from pb in db.PurchaseProductBatches.Where(x => x.ProductId == productId).ToList()
                        join gd in db.Godowns on pb.Godown equals gd.Id into g
                        from gd in g.DefaultIfEmpty()
                        join u in db.Units on pb.Unit equals u.Id
                        join p in db.Products on pb.ProductId equals p.Id
                        select new ProductBatchSalesViewModel()
                                   {
                                       BatchSerialNo = pb.SerialNo,
                                       BuyRate = pb.BuyRate,
                                       ExpDate = pb.EXPDate,
                                       Godown = gd == null ? string.Empty : gd.Name,
                                       GodownId = gd == null ? 0 : gd.Id,
                                       StockQty = pb.StockQuantity,
                                       MfgDate = pb.MFGDate,
                                       SalesRate = pb.SalesRate,
                                       Unit = u.Description,
                                       UnitId=u.Id,
                                       Id = pb.Id,
                                       ExpiredProduct = p.ExpiredProduct == null || p.ExpiredProduct == 0 ? expDateAction : p.ExpiredProduct,
                                       IsExpired = pb.EXPDate != null && Convert.ToDateTime(pb.EXPDate).Date >= DateTime.Now.Date ? false : true
                                   }).ToList();

            return data;
        }
    }

    public interface IPurchaseService
    {
        IEnumerable<ProductBatchSalesViewModel> GetProductBatchList(int productId);
    }
}