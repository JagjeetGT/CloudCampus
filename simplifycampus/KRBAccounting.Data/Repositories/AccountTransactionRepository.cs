using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class AccountTransactionRepository : RepositoryBase<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public void PostToAccountTransaction(string vNo, DateTime vDate, int glCode, int? slCode, decimal drAmount,
                                      decimal localDrAmt, decimal crAmount, decimal localCrAmt, string source,
                                      int createdById, int cbCode, int fyId, int referenceId, string narration,
                                      string remarks, DateTime? dueDate,string crCode,decimal? crRate,int sno
            //,int sourceId
            )
        {

            var _context = new KRBAccounting.Data.DataContext();

            //if (source=="JV" || source =="OB")
            //{
            //      //This is single entry always dont post to sourceID
            //    _context.AccountTransactions.SqlQuery("InsertAccountTransaction " + vNo + "," + vDate + "," + crCode + "," + crRate + "," + glCode + "," + drAmount + "," + crAmount + "," + localDrAmt + "," + localCrAmt + "," + narration + "," + remarks + "," + source + "," + sno + "," + cbCode + "," + createdById + "," + referenceId + "," + fyId + "," + dueDate + "," + slCode);   
            //}
            //else
            //{
                _context.AccountTransactions.SqlQuery("InsertAccountTransaction " + vNo + "," + vDate + "," + crCode + "," + crRate + "," + glCode + "," + drAmount + "," + crAmount + "," + localDrAmt + "," + localCrAmt + "," + narration + "," + remarks + "," + source + "," + sno + "," + cbCode + "," + createdById + "," + referenceId + "," + fyId + "," + dueDate + "," + slCode);   
                //  Post to it source id
            //    _context.AccountTransactions.SqlQuery("InsertAccountTransaction " + vNo + "," + vDate + "," + crCode + "," + crRate + "," + glCode + "," + crAmount + "," + drAmount + "," + localCrAmt + "," + localDrAmt + "," + narration + "," + remarks + "," + source + "," + sno + "," + cbCode + "," + createdById + "," + referenceId + "," + fyId + "," + dueDate + "," + slCode);   
                
            //}
            //string acSource = 
            //`if JV / OB ho bhane single insert to that ledger only here we have to made sinle transaction
            //post to ledgerId
            // else if for other ac. source we have to made double entry . eg. if we are posting to account from sales (SB),
            //  then we have to make any entry for that customer and sales account defined in system controls

        }

    }

    public interface IAccountTransactionRepository : IRepository<AccountTransaction>
    {
        void PostToAccountTransaction(string vNo, DateTime vDate, int glCode, int? slCode, decimal drAmount,
                                      decimal localDrAmt, decimal crAmount, decimal localCrAmt, string source,
                                      int createdById, int cbCode, int fyId, int referenceId, string narration,
                                      string remarks, DateTime? dueDate, string crCode, decimal? crRate, int sno
            //, 
            //int sourceId
            );
    }
}
