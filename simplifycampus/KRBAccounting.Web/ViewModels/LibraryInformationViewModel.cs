using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels
{
    public class LibraryInformationViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScLibraryCard> LibraryCard { get; set; }
        public IEnumerable<SP_LibraryInformation> LibraryInformations { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SP_StudentInfoHeader { get; set; }
        public IEnumerable<SP_LibraryDueAmount> SP_LibraryDueAmount { get; set; }

        public string LibraryRegistrationNumber { get; set; }


    }
}