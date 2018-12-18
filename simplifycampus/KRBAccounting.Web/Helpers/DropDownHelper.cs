using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.Helpers
{
    public class Sex
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MartialStatus
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class BloodGroup
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Relation
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public static class DropDownHelper
    {
        public static SelectList CreateSexDropDown()
        {
            List<SelectListItem> lstSelectListItem = new List<SelectListItem>();



            SelectListItem objSelectListItemOne = new SelectListItem();
            objSelectListItemOne.Text = "Male";
            objSelectListItemOne.Value = "Male";
            lstSelectListItem.Add(objSelectListItemOne);

            SelectListItem objSelectListItemTwo = new SelectListItem();
            objSelectListItemTwo.Text = "Female";
            objSelectListItemTwo.Value = "Female";
            lstSelectListItem.Add(objSelectListItemTwo);
            return new SelectList(lstSelectListItem, "Text", "Value");
        }

        public static SelectList CreateMartialStatusDropDown()
        {
            List<SelectListItem> lstSelectListItem = new List<SelectListItem>();

            //SelectListItem objSelectListItemZero = new SelectListItem();
            //objSelectListItemZero.Text = "--Select marital status--";
            //objSelectListItemZero.Value = "";
            //lstSelectListItem.Add(objSelectListItemZero);

            SelectListItem objSelectListItemZero = new SelectListItem();
            objSelectListItemZero.Text = "Unmarried";
            objSelectListItemZero.Value = "Unmarried";
            objSelectListItemZero.Selected = true;
            lstSelectListItem.Add(objSelectListItemZero);

            SelectListItem objSelectListItemOne = new SelectListItem();
            objSelectListItemOne.Text = "Married";
            objSelectListItemOne.Value = "Married";
            lstSelectListItem.Add(objSelectListItemOne);

            //SelectListItem objSelectListItemTwo = new SelectListItem();
            //objSelectListItemTwo.Text = "Unmarried";
            //objSelectListItemTwo.Value = "Unmarried";
            //objSelectListItemTwo.Selected = true;
            //lstSelectListItem.Add(objSelectListItemTwo);

            SelectListItem objSelectListItemThree = new SelectListItem();
            objSelectListItemThree.Text = "Others";
            objSelectListItemThree.Value = "Others";
            lstSelectListItem.Add(objSelectListItemThree);

            //SelectListItem objSelectListItemThree = new SelectListItem();
            //objSelectListItemThree.Text = "Divorced";
            //objSelectListItemThree.Value = "Divorced";
            //lstSelectListItem.Add(objSelectListItemThree);


            //SelectListItem objSelectListItemFour = new SelectListItem();
            //objSelectListItemFour.Text = "Widow";
            //objSelectListItemFour.Value = "Widow";
            //lstSelectListItem.Add(objSelectListItemFour);


            //SelectListItem objSelectListItemFive = new SelectListItem();
            //objSelectListItemFive.Text = "Widower ";
            //objSelectListItemFive.Value = "Widower ";
            //lstSelectListItem.Add(objSelectListItemFive);
            return new SelectList(lstSelectListItem, "Text", "Value");
        }

        public static List<SelectListItem> CreateBloodGroupDropDown()
        {
            return new List<SelectListItem>()

                                                         {
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "--Select blood group--",
                                                                     Value = "-"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "A+",
                                                                     Value = "A+"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "B+",
                                                                     Value = "B+"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "AB+",
                                                                     Value = "AB+"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "O+",
                                                                     Value = "O+"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "A-",
                                                                     Value = "A-"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "B-",
                                                                     Value = "B-"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "AB-",
                                                                     Value = "AB-"

                                                                 },
                                                             new SelectListItem()
                                                                 {
                                                                     Text = "O-",
                                                                     Value = "O-"

                                                                 }
                                                         };
            //SelectListItem objSelectListItemZero = new SelectListItem();
            //objSelectListItemZero.Text = "--Select blood group--";
            //objSelectListItemZero.Value = "-";
            //lstSelectListItem.Add(objSelectListItemZero);

            //SelectListItem objSelectListItemOne = new SelectListItem();
            //objSelectListItemOne.Text = "A+";
            //objSelectListItemOne.Value = "A+";
            //lstSelectListItem.Add(objSelectListItemOne);

            //SelectListItem objSelectListItemTwo = new SelectListItem();
            //objSelectListItemTwo.Text = "B+";
            //objSelectListItemTwo.Value = "B+";
            //lstSelectListItem.Add(objSelectListItemTwo);

            //SelectListItem objSelectListItemThree = new SelectListItem();
            //objSelectListItemThree.Text = "AB+";
            //objSelectListItemThree.Value = "AB+";
            //lstSelectListItem.Add(objSelectListItemThree);


            //SelectListItem objSelectListItemFour = new SelectListItem();
            //objSelectListItemFour.Text = "O+";
            //objSelectListItemFour.Value = "O+";
            //lstSelectListItem.Add(objSelectListItemFour);

            //SelectListItem objSelectListItemFive = new SelectListItem();
            //objSelectListItemFive.Text = "A-";
            //objSelectListItemFive.Value = "A-";
            //lstSelectListItem.Add(objSelectListItemFive);

            //SelectListItem objSelectListItemSix = new SelectListItem();
            //objSelectListItemSix.Text = "B-";
            //objSelectListItemSix.Value = "B-";
            //lstSelectListItem.Add(objSelectListItemSix);

            //SelectListItem objSelectListItemSeven = new SelectListItem();
            //objSelectListItemSeven.Text = "AB-";
            //objSelectListItemSeven.Value = "AB-";
            //lstSelectListItem.Add(objSelectListItemSeven);


            //SelectListItem objSelectListItemEight = new SelectListItem();
            //objSelectListItemEight.Text = "O-";
            //objSelectListItemEight.Value = "O-";
            //lstSelectListItem.Add(objSelectListItemEight);


            //return new SelectList(lstSelectListItem, "Value", "Text");

        }

        public static SelectList CreateRelationDropDown()
        {
            List<SelectListItem> lstSelectListItem = new List<SelectListItem>();

            SelectListItem objSelectListItemZero = new SelectListItem();
            objSelectListItemZero.Text = "--Relation--";
            objSelectListItemZero.Value = "-";
            lstSelectListItem.Add(objSelectListItemZero);

            SelectListItem objSelectListItemOne = new SelectListItem();
            objSelectListItemOne.Text = "Brothers/Sisters";
            objSelectListItemOne.Value = "Brothers/Sisters";
            lstSelectListItem.Add(objSelectListItemOne);

            SelectListItem objSelectListItemTwo = new SelectListItem();
            objSelectListItemTwo.Text = "Uncle/Aunt";
            objSelectListItemTwo.Value = "Uncle/Aunt";
            lstSelectListItem.Add(objSelectListItemTwo);

            SelectListItem objSelectListItemThree = new SelectListItem();
            objSelectListItemThree.Text = "Others";
            objSelectListItemThree.Value = "Others";
            lstSelectListItem.Add(objSelectListItemThree);

            return new SelectList(lstSelectListItem, "Text", "Value");

        }

        public static SelectList CreateMonthMiti()
        {
            List<SelectListItem> MonthListMiti = new List<SelectListItem>()
                                                     {
                                                         new SelectListItem() {Text = "Baishakh", Value = "01"},
                                                         new SelectListItem() {Text = "Jestha", Value = "02"},
                                                         new SelectListItem() {Text = "Ashadh", Value = "03"},
                                                         new SelectListItem() {Text = "Shrawan", Value = "04"},
                                                         new SelectListItem() {Text = "Bhadra", Value = "05"},
                                                         new SelectListItem() {Text = "Ashwin", Value = "06"},
                                                         new SelectListItem() {Text = "Kartik", Value = "07"},
                                                         new SelectListItem() {Text = "Mangsir", Value = "08"},
                                                         new SelectListItem() {Text = "Poush", Value = "09"},
                                                         new SelectListItem() {Text = "Magh", Value = "10"},
                                                         new SelectListItem() {Text = "Falgun", Value = "11"},
                                                         new SelectListItem() {Text = "Chaitra", Value = "12"},

                                                     };
            return new SelectList(MonthListMiti, "Value", "Text");
        }

        public static SelectList CreateMonthDate()
        {
            List<SelectListItem> MonthListDate = new List<SelectListItem>()
                                                     {
                                                         new SelectListItem() {Text = "January", Value = "01"},
                                                         new SelectListItem() {Text = "February", Value = "02"},
                                                         new SelectListItem() {Text = "March", Value = "03"},
                                                         new SelectListItem() {Text = "April", Value = "04"},
                                                         new SelectListItem() {Text = "May", Value = "05"},
                                                         new SelectListItem() {Text = "June", Value = "06"},
                                                         new SelectListItem() {Text = "July", Value = "07"},
                                                         new SelectListItem() {Text = "August", Value = "08"},
                                                         new SelectListItem() {Text = "September", Value = "09"},
                                                         new SelectListItem() {Text = "October", Value = "10"},
                                                         new SelectListItem() {Text = "November", Value = "11"},
                                                         new SelectListItem() {Text = "December", Value = "12"},

                                                     };
            return new SelectList(MonthListDate, "Value", "Text");
        }

        public static List<SelectListItem> OperatorList()
        {
            List<SelectListItem> list = new List<SelectListItem>
                                            {
                                                new SelectListItem
                                                    {
                                                        Text = "+",
                                                        Value = "+",
                                                        Selected = true
                                                    },
            new SelectListItem()
                {
                    Text = "-",
                                                        Value = "-"
                }
                                            };
            return list.ToList();
        }

        public static SelectList CreateGenderDropDown()
        {
            List<SelectListItem> lstSelectListItem = new List<SelectListItem>();


            SelectListItem objSelectListItemOne = new SelectListItem();
            objSelectListItemOne.Text = "Male";
            objSelectListItemOne.Value = "Male";
            lstSelectListItem.Add(objSelectListItemOne);

            SelectListItem objSelectListItemTwo = new SelectListItem();
            objSelectListItemTwo.Text = "Female";
            objSelectListItemTwo.Value = "Female";
            lstSelectListItem.Add(objSelectListItemTwo);
            SelectListItem objSelectListItemThree = new SelectListItem();
            objSelectListItemThree.Text = "Both";
            objSelectListItemThree.Value = "Both";
            lstSelectListItem.Add(objSelectListItemThree);
            return new SelectList(lstSelectListItem, "Text", "Value");
        }
    }
}