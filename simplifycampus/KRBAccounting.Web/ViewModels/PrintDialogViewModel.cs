using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels
{
    public class PrintDialogViewModel
    {
        public SelectList PrintList { get; set; }
        public string PrintName { get; set; }
        public SelectList PaperSizeList { get; set; }
        public string PaperSize { get; set; }
        public SelectList OrientationList
        {
            get
            {

                var list = new List<SelectListItem>()
                               {
                                   
                              
                new SelectListItem()
                    {
                        Selected = true,
                        Text = "Portrait",
                        Value = "1"
                    },
                 new SelectListItem()
                    {
                        
                        Text = "Landscape",
                        Value = "2"
                    }
                 };
                return new SelectList(list, "Value", "Text");
            }

        }
        public string Orientation { get; set; }

           
    }
}