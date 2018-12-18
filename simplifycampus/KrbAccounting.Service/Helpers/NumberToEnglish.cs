namespace System.Web.Mvc
{
    public class NumberToEnglish
    {
        //changeCurrencyToWords
        //changeNumericToWords
        //public static string changeNumericToWords(string value1)
        //{
        //    var val = Convert.ToDouble(value1);
        //    val = Math.Round(val, 2);
        //    var arrValue = val.ToString().Split('.');
        //    Int64 rup = Convert.ToInt64(arrValue[0]);

        //    Int64 paisa = 0;
        //    if (arrValue.Length > 1)
        //    {
        //        paisa = Convert.ToInt64(arrValue[1]);
        //    }

        //    string result = "";
        //    Int64 res;
        //    if ((rup / 100000000000) > 0)
        //    {
        //        res = rup / 100000000000;
        //        rup = rup % 100000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Kharab";
        //    }
        //    if ((rup / 1000000000) > 0)
        //    {
        //        res = rup / 1000000000;
        //        rup = rup % 1000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Arab";
        //    }
        //    if ((rup / 10000000) > 0)
        //    {
        //        res = rup / 10000000;
        //        rup = rup % 10000000;
        //        result = result + ' ' + RupeesToWords(res) + " Crore";
        //    }
        //    if ((rup / 100000) > 0)
        //    {
        //        res = rup / 100000;
        //        rup = rup % 100000;
        //        result = result + ' ' + RupeesToWords(res) + " Lakh";
        //    }
        //    if ((rup / 1000) > 0)
        //    {
        //        res = rup / 1000;
        //        rup = rup % 1000;
        //        result = result + ' ' + RupeesToWords(res) + " Thousand";
        //    }
        //    if ((rup / 100) > 0)
        //    {
        //        res = rup / 100;
        //        rup = rup % 100;
        //        result = result + ' ' + RupeesToWords(res) + " Hundred";
        //    }
        //    if ((rup % 10) > 0)
        //    {
        //        res = rup % 100;
        //        result = result + " " + RupeesToWords(res);
        //    }


        //    if (paisa > 0)
        //    {
        //        if ((paisa % 10) > 0)
        //        {
        //            res = paisa % 100;
        //            result = result + " Rupees and " + RupeesToWords(res);
        //            result = result + ' ' + " Paisa only";
        //        }
        //    }
        //    else
        //    {
        //        result = result + ' ' + " Rupees only";
        //    }
        //    return result;
        //}
        //public static string changeNumericToWords(double value)
        //{
        //    value = Math.Round(value, 2);
        //    var arrValue = value.ToString().Split('.');
        //    Int64 rup = Convert.ToInt64(arrValue[0]);

        //    Int64 paisa = 0;
        //    if (arrValue.Length > 1)
        //    {
        //        paisa = Convert.ToInt64(arrValue[1]);
        //    }

        //    string result = "";
        //    Int64 res;
        //    if ((rup / 100000000000) > 0)
        //    {
        //        res = rup / 100000000000;
        //        rup = rup % 100000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Kharab";
        //    }
        //    if ((rup / 1000000000) > 0)
        //    {
        //        res = rup / 1000000000;
        //        rup = rup % 1000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Arab";
        //    }
        //    if ((rup / 10000000) > 0)
        //    {
        //        res = rup / 10000000;
        //        rup = rup % 10000000;
        //        result = result + ' ' + RupeesToWords(res) + " Crore";
        //    }
        //    if ((rup / 100000) > 0)
        //    {
        //        res = rup / 100000;
        //        rup = rup % 100000;
        //        result = result + ' ' + RupeesToWords(res) + " Lakh";
        //    }
        //    if ((rup / 1000) > 0)
        //    {
        //        res = rup / 1000;
        //        rup = rup % 1000;
        //        result = result + ' ' + RupeesToWords(res) + " Thousand";
        //    }
        //    if ((rup / 100) > 0)
        //    {
        //        res = rup / 100;
        //        rup = rup % 100;
        //        result = result + ' ' + RupeesToWords(res) + " Hundred";
        //    }
        //    if ((rup % 10) > 0)
        //    {
        //        res = rup % 100;
        //        result = result + " " + RupeesToWords(res);
        //    }


        //    if (paisa > 0)
        //    {
        //        if ((paisa % 10) > 0)
        //        {
        //            res = paisa % 100;
        //            result = result + " Rupees and " + RupeesToWords(res);
        //            result = result + ' ' + " Paisa only";
        //        }
        //    }
        //    else
        //    {
        //        result = result + ' ' + " Rupees only";
        //    }
        //    return result;
        //}
        //public static string changeCurrencyToWords(string value1)
        //{
        //    var val = Convert.ToDouble(value1);
        //    val = Math.Round(val, 2);
        //    var arrValue = val.ToString().Split('.');
        //    Int64 rup = Convert.ToInt64(arrValue[0]);

        //    Int64 paisa = 0;
        //    if (arrValue.Length > 1)
        //    {
        //        paisa = Convert.ToInt64(arrValue[1]);
        //    }

        //    string result = "";
        //    Int64 res;
        //    if ((rup / 100000000000) > 0)
        //    {
        //        res = rup / 100000000000;
        //        rup = rup % 100000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Kharab";
        //    }
        //    if ((rup / 1000000000) > 0)
        //    {
        //        res = rup / 1000000000;
        //        rup = rup % 1000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Arab";
        //    }
        //    if ((rup / 10000000) > 0)
        //    {
        //        res = rup / 10000000;
        //        rup = rup % 10000000;
        //        result = result + ' ' + RupeesToWords(res) + " Crore";
        //    }
        //    if ((rup / 100000) > 0)
        //    {
        //        res = rup / 100000;
        //        rup = rup % 100000;
        //        result = result + ' ' + RupeesToWords(res) + " Lakh";
        //    }
        //    if ((rup / 1000) > 0)
        //    {
        //        res = rup / 1000;
        //        rup = rup % 1000;
        //        result = result + ' ' + RupeesToWords(res) + " Thousand";
        //    }
        //    if ((rup / 100) > 0)
        //    {
        //        res = rup / 100;
        //        rup = rup % 100;
        //        result = result + ' ' + RupeesToWords(res) + " Hundred";
        //    }
        //    if ((rup % 10) > 0)
        //    {
        //        res = rup % 100;
        //        result = result + " " + RupeesToWords(res);
        //    }


        //    if (paisa > 0)
        //    {
        //        if ((paisa % 10) > 0)
        //        {
        //            res = paisa % 100;
        //            result = result + " Rupees and " + RupeesToWords(res);
        //            result = result + ' ' + " Paisa only";
        //        }
        //    }
        //    else
        //    {
        //        result = result + ' ' + " Rupees only";
        //    }
        //    return result;
        //}
        //public static string changeCurrencyToWords(double value)
        //{
        //    value = Math.Round(value, 2);
        //    var arrValue = value.ToString().Split('.');
        //    Int64 rup = Convert.ToInt64(arrValue[0]);

        //    Int64 paisa = 0;
        //    if (arrValue.Length > 1)
        //    {
        //        paisa = Convert.ToInt64(arrValue[1]);
        //    }

        //    string result = "";
        //    Int64 res;
        //    if ((rup / 100000000000) > 0)
        //    {
        //        res = rup / 100000000000;
        //        rup = rup % 100000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Kharab";
        //    }
        //    if ((rup / 1000000000) > 0)
        //    {
        //        res = rup / 1000000000;
        //        rup = rup % 1000000000;
        //        result = result + ' ' + RupeesToWords(res) + " Arab";
        //    }
        //    if ((rup / 10000000) > 0)
        //    {
        //        res = rup / 10000000;
        //        rup = rup % 10000000;
        //        result = result + ' ' + RupeesToWords(res) + " Crore";
        //    }
        //    if ((rup / 100000) > 0)
        //    {
        //        res = rup / 100000;
        //        rup = rup % 100000;
        //        result = result + ' ' + RupeesToWords(res) + " Lakh";
        //    }
        //    if ((rup / 1000) > 0)
        //    {
        //        res = rup / 1000;
        //        rup = rup % 1000;
        //        result = result + ' ' + RupeesToWords(res) + " Thousand";
        //    }
        //    if ((rup / 100) > 0)
        //    {
        //        res = rup / 100;
        //        rup = rup % 100;
        //        result = result + ' ' + RupeesToWords(res) + " Hundred";
        //    }
        //    if ((rup % 10) > 0)
        //    {
        //        res = rup % 100;
        //        result = result + " " + RupeesToWords(res);
        //    }


        //    if (paisa > 0)
        //    {
        //        if ((paisa % 10) > 0)
        //        {
        //            res = paisa % 100;
        //            result = result + " Rupees and " + RupeesToWords(res);
        //            result = result + ' ' + " Paisa only";
        //        }
        //    }
        //    else
        //    {
        //        result = result + ' ' + " Rupees only";
        //    }
        //    return result;
        //}
        //public static string RupeesToWords(Int64 rup)
        //{
        //    string result = "";
        //    if ((rup >= 1) && (rup <= 10))
        //    {
        //        if ((rup % 10) == 1) result = "One";
        //        if ((rup % 10) == 2) result = "Two";
        //        if ((rup % 10) == 3) result = "Three";
        //        if ((rup % 10) == 4) result = "Four";
        //        if ((rup % 10) == 5) result = "Five";
        //        if ((rup % 10) == 6) result = "Six";
        //        if ((rup % 10) == 7) result = "Seven";
        //        if ((rup % 10) == 8) result = "Eight";
        //        if ((rup % 10) == 9) result = "Nine";
        //        if ((rup % 10) == 0) result = "Ten";
        //    }
        //    if (rup > 9 && rup < 20)
        //    {
        //        if (rup == 11) result = "Eleven";
        //        if (rup == 12) result = "Twelve";
        //        if (rup == 13) result = "Thirteen";
        //        if (rup == 14) result = "Forteen";
        //        if (rup == 15) result = "Fifteen";
        //        if (rup == 16) result = "Sixteen";
        //        if (rup == 17) result = "Seventeen";
        //        if (rup == 18) result = "Eighteen";
        //        if (rup == 19) result = "Nineteen";
        //    }
        //    if (rup > 20 && (rup / 10) == 2 && (rup % 10) == 0) result = "Twenty";
        //    if (rup > 20 && (rup / 10) == 3 && (rup % 10) == 0) result = "Thirty";
        //    if (rup > 20 && (rup / 10) == 4 && (rup % 10) == 0) result = "Forty";
        //    if (rup > 20 && (rup / 10) == 5 && (rup % 10) == 0) result = "Fifty";
        //    if (rup > 20 && (rup / 10) == 6 && (rup % 10) == 0) result = "Sixty";
        //    if (rup > 20 && (rup / 10) == 7 && (rup % 10) == 0) result = "Seventy";
        //    if (rup > 20 && (rup / 10) == 8 && (rup % 10) == 0) result = "Eighty";
        //    if (rup > 20 && (rup / 10) == 9 && (rup % 10) == 0) result = "Ninty";

        //    if (rup > 20 && (rup / 10) == 2 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Twenty One";
        //        if ((rup % 10) == 2) result = "Twenty Two";
        //        if ((rup % 10) == 3) result = "Twenty Three";
        //        if ((rup % 10) == 4) result = "Twenty Four";
        //        if ((rup % 10) == 5) result = "Twenty Five";
        //        if ((rup % 10) == 6) result = "Twenty Six";
        //        if ((rup % 10) == 7) result = "Twenty Seven";
        //        if ((rup % 10) == 8) result = "Twenty Eight";
        //        if ((rup % 10) == 9) result = "Twenty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 3 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Thirty One";
        //        if ((rup % 10) == 2) result = "Thirty Two";
        //        if ((rup % 10) == 3) result = "Thirty Three";
        //        if ((rup % 10) == 4) result = "Thirty Four";
        //        if ((rup % 10) == 5) result = "Thirty Five";
        //        if ((rup % 10) == 6) result = "Thirty Six";
        //        if ((rup % 10) == 7) result = "Thirty Seven";
        //        if ((rup % 10) == 8) result = "Thirty Eight";
        //        if ((rup % 10) == 9) result = "Thirty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 4 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Forty One";
        //        if ((rup % 10) == 2) result = "Forty Two";
        //        if ((rup % 10) == 3) result = "Forty Three";
        //        if ((rup % 10) == 4) result = "Forty Four";
        //        if ((rup % 10) == 5) result = "Forty Five";
        //        if ((rup % 10) == 6) result = "Forty Six";
        //        if ((rup % 10) == 7) result = "Forty Seven";
        //        if ((rup % 10) == 8) result = "Forty Eight";
        //        if ((rup % 10) == 9) result = "Forty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 5 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Fifty One";
        //        if ((rup % 10) == 2) result = "Fifty Two";
        //        if ((rup % 10) == 3) result = "Fifty Three";
        //        if ((rup % 10) == 4) result = "Fifty Four";
        //        if ((rup % 10) == 5) result = "Fifty Five";
        //        if ((rup % 10) == 6) result = "Fifty Six";
        //        if ((rup % 10) == 7) result = "Fifty Seven";
        //        if ((rup % 10) == 8) result = "Fifty Eight";
        //        if ((rup % 10) == 9) result = "Fifty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 6 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Sixty One";
        //        if ((rup % 10) == 2) result = "Sixty Two";
        //        if ((rup % 10) == 3) result = "Sixty Three";
        //        if ((rup % 10) == 4) result = "Sixty Four";
        //        if ((rup % 10) == 5) result = "Sixty Five";
        //        if ((rup % 10) == 6) result = "Sixty Six";
        //        if ((rup % 10) == 7) result = "Sixty Seven";
        //        if ((rup % 10) == 8) result = "Sixty Eight";
        //        if ((rup % 10) == 9) result = "Sixty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 7 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Seventy One";
        //        if ((rup % 10) == 2) result = "Seventy Two";
        //        if ((rup % 10) == 3) result = "Seventy Three";
        //        if ((rup % 10) == 4) result = "Seventy Four";
        //        if ((rup % 10) == 5) result = "Seventy Five";
        //        if ((rup % 10) == 6) result = "Seventy Six";
        //        if ((rup % 10) == 7) result = "Seventy Seven";
        //        if ((rup % 10) == 8) result = "Seventy Eight";
        //        if ((rup % 10) == 9) result = "Seventy Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 8 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Eighty One";
        //        if ((rup % 10) == 2) result = "Eighty Two";
        //        if ((rup % 10) == 3) result = "Eighty Three";
        //        if ((rup % 10) == 4) result = "Eighty Four";
        //        if ((rup % 10) == 5) result = "Eighty Five";
        //        if ((rup % 10) == 6) result = "Eighty Six";
        //        if ((rup % 10) == 7) result = "Eighty Seven";
        //        if ((rup % 10) == 8) result = "Eighty Eight";
        //        if ((rup % 10) == 9) result = "Eighty Nine";
        //    }
        //    if (rup > 20 && (rup / 10) == 9 && (rup % 10) != 0)
        //    {
        //        if ((rup % 10) == 1) result = "Ninty One";
        //        if ((rup % 10) == 2) result = "Ninty Two";
        //        if ((rup % 10) == 3) result = "Ninty Three";
        //        if ((rup % 10) == 4) result = "Ninty Four";
        //        if ((rup % 10) == 5) result = "Ninty Five";
        //        if ((rup % 10) == 6) result = "Ninty Six";
        //        if ((rup % 10) == 7) result = "Ninty Seven";
        //        if ((rup % 10) == 8) result = "Ninty Eight";
        //        if ((rup % 10) == 9) result = "Ninty Nine";
        //    }
        //    return result;
        //}


        public static string changeCurrencyToWords(double value)
        {
            value = Math.Round(value, 2);
            var arrValue = value.ToString().Split('.');
            Int64 rup = Convert.ToInt64(arrValue[0]);

            Int64 paisa = 0;
            if (arrValue.Length > 1)
            {
                paisa = Convert.ToInt64(arrValue[1]);
            }

            string result = "";
            Int64 res;
            if ((rup / 100000000000) > 0)
            {
                res = rup / 100000000000;
                rup = rup % 100000000000;
                result = result + ' ' + RupeesToWords(res) + " Kharab";
            }
            if ((rup / 1000000000) > 0)
            {
                res = rup / 1000000000;
                rup = rup % 1000000000;
                result = result + ' ' + RupeesToWords(res) + " Arab";
            }
            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }
            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lakh";
            }
            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }
            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }
            if ((rup / 10) > 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }


            if (paisa > 0)
            {
                if ((paisa % 10) > 0)
                {
                    res = paisa % 100;
                    result = result + " Rupees and " + RupeesToWords(res);
                    result = result + ' ' + " Paisa only";
                }
            }
            else
            {
                result = result + ' ' + " Rupees only";
            }
            return result;
        }
        public static string changeNumericToWords(double value)
        {
            value = Math.Round(value, 2);
            var arrValue = value.ToString().Split('.');
            Int64 rup = Convert.ToInt64(arrValue[0]);

            Int64 paisa = 0;
            if (arrValue.Length > 1)
            {
                paisa = Convert.ToInt64(arrValue[1]);
            }

            string result = "";
            Int64 res;
            if ((rup / 100000000000) > 0)
            {
                res = rup / 100000000000;
                rup = rup % 100000000000;
                result = result + ' ' + RupeesToWords(res) + " Kharab";
            }
            if ((rup / 1000000000) > 0)
            {
                res = rup / 1000000000;
                rup = rup % 1000000000;
                result = result + ' ' + RupeesToWords(res) + " Arab";
            }
            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }
            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lakh";
            }
            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }
            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }
            if ((rup / 10) > 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }


            if (paisa > 0)
            {
                if ((paisa % 10) > 0)
                {
                    res = paisa % 100;
                    result = result + " Rupees and " + RupeesToWords(res);
                    result = result + ' ' + " Paisa only";
                }
            }
            else
            {
                result = result + ' ' + " Rupees only";
            }
            return result;
        }
        public static string changeNumericToWords(string value)
        {
           // value = Convert.ToDouble(value);
            var arrValue = value.ToString().Split('.');
            Int64 rup = Convert.ToInt64(arrValue[0]);

            Int64 paisa = 0;
            if (arrValue.Length > 1)
            {
                paisa = Convert.ToInt64(arrValue[1]);
            }

            string result = "";
            Int64 res;
            if ((rup / 100000000000) > 0)
            {
                res = rup / 100000000000;
                rup = rup % 100000000000;
                result = result + ' ' + RupeesToWords(res) + " Kharab";
            }
            if ((rup / 1000000000) > 0)
            {
                res = rup / 1000000000;
                rup = rup % 1000000000;
                result = result + ' ' + RupeesToWords(res) + " Arab";
            }
            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }
            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lakh";
            }
            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }
            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }
            if ((rup / 10) > 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }


            if (paisa > 0)
            {
                if ((paisa % 10) > 0)
                {
                    res = paisa % 100;
                    result = result + " Rupees and " + RupeesToWords(res);
                    result = result + ' ' + " Paisa only";
                }
            }
            else
            {
                result = result + ' ' + " Rupees only";
            }
            return result;
        }
        public static string changeCurrencyToWords(string value)
        {
           // value = Math.Round(value, 2);
            var arrValue = value.ToString().Split('.');
            Int64 rup = Convert.ToInt64(arrValue[0]);

            Int64 paisa = 0;
            if (arrValue.Length > 1)
            {
                paisa = Convert.ToInt64(arrValue[1]);
            }

            string result = "";
            Int64 res;
            if ((rup / 100000000000) > 0)
            {
                res = rup / 100000000000;
                rup = rup % 100000000000;
                result = result + ' ' + RupeesToWords(res) + " Kharab";
            }
            if ((rup / 1000000000) > 0)
            {
                res = rup / 1000000000;
                rup = rup % 1000000000;
                result = result + ' ' + RupeesToWords(res) + " Arab";
            }
            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }
            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lakh";
            }
            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }
            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }
            if ((rup / 10) > 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }


            if (paisa > 0)
            {
                if ((paisa % 10) > 0)
                {
                    res = paisa % 100;
                    result = result + " Rupees and " + RupeesToWords(res);
                    result = result + ' ' + " Paisa only";
                }
            }
            else
            {
                result = result + ' ' + " Rupees only";
            }
            return result;
        }
        public static string Rupees(double value)
        {
            value = Math.Round(value, 2);
            var arrValue = value.ToString().Split('.');
            Int64 rup = Convert.ToInt64(arrValue[0]);

            Int64 paisa = 0;
            if (arrValue.Length > 1)
            {
                paisa = Convert.ToInt64(arrValue[1]);
            }

            string result = "";
            Int64 res;
            if ((rup / 100000000000) > 0)
            {
                res = rup / 100000000000;
                rup = rup % 100000000000;
                result = result + ' ' + RupeesToWords(res) + " Kharab";
            }
            if ((rup / 1000000000) > 0)
            {
                res = rup / 1000000000;
                rup = rup % 1000000000;
                result = result + ' ' + RupeesToWords(res) + " Arab";
            }
            if ((rup / 10000000) > 0)
            {
                res = rup / 10000000;
                rup = rup % 10000000;
                result = result + ' ' + RupeesToWords(res) + " Crore";
            }
            if ((rup / 100000) > 0)
            {
                res = rup / 100000;
                rup = rup % 100000;
                result = result + ' ' + RupeesToWords(res) + " Lakh";
            }
            if ((rup / 1000) > 0)
            {
                res = rup / 1000;
                rup = rup % 1000;
                result = result + ' ' + RupeesToWords(res) + " Thousand";
            }
            if ((rup / 100) > 0)
            {
                res = rup / 100;
                rup = rup % 100;
                result = result + ' ' + RupeesToWords(res) + " Hundred";
            }
            if ((rup % 10) > 0)
            {
                res = rup % 100;
                result = result + " " + RupeesToWords(res);
            }


            if (paisa > 0)
            {
                if ((paisa % 10) > 0)
                {
                    res = paisa % 100;
                    result = result + " Rupees and " + RupeesToWords(res);
                    result = result + ' ' + " Paisa only";
                }
            }
            else
            {
                result = result + ' ' + " Rupees only";
            }
            return result;
        }
        public static string RupeesToWords(Int64 rup)
        {
            string result = "";
            if ((rup >= 1) && (rup <= 10))
            {
                if ((rup % 10) == 1) result = "One";
                if ((rup % 10) == 2) result = "Two";
                if ((rup % 10) == 3) result = "Three";
                if ((rup % 10) == 4) result = "Four";
                if ((rup % 10) == 5) result = "Five";
                if ((rup % 10) == 6) result = "Six";
                if ((rup % 10) == 7) result = "Seven";
                if ((rup % 10) == 8) result = "Eight";
                if ((rup % 10) == 9) result = "Nine";
                if ((rup % 10) == 0) result = "Ten";
            }
            if (rup > 9 && rup < 20)
            {
                if (rup == 11) result = "Eleven";
                if (rup == 12) result = "Twelve";
                if (rup == 13) result = "Thirteen";
                if (rup == 14) result = "Forteen";
                if (rup == 15) result = "Fifteen";
                if (rup == 16) result = "Sixteen";
                if (rup == 17) result = "Seventeen";
                if (rup == 18) result = "Eighteen";
                if (rup == 19) result = "Nineteen";
            }
            if (rup >= 20 && (rup / 10) == 2 && (rup % 10) == 0) result = "Twenty";
            if (rup > 20 && (rup / 10) == 3 && (rup % 10) == 0) result = "Thirty";
            if (rup > 20 && (rup / 10) == 4 && (rup % 10) == 0) result = "Forty";
            if (rup > 20 && (rup / 10) == 5 && (rup % 10) == 0) result = "Fifty";
            if (rup > 20 && (rup / 10) == 6 && (rup % 10) == 0) result = "Sixty";
            if (rup > 20 && (rup / 10) == 7 && (rup % 10) == 0) result = "Seventy";
            if (rup > 20 && (rup / 10) == 8 && (rup % 10) == 0) result = "Eighty";
            if (rup > 20 && (rup / 10) == 9 && (rup % 10) == 0) result = "Ninty";

            if (rup > 20 && (rup / 10) == 2 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Twenty One";
                if ((rup % 10) == 2) result = "Twenty Two";
                if ((rup % 10) == 3) result = "Twenty Three";
                if ((rup % 10) == 4) result = "Twenty Four";
                if ((rup % 10) == 5) result = "Twenty Five";
                if ((rup % 10) == 6) result = "Twenty Six";
                if ((rup % 10) == 7) result = "Twenty Seven";
                if ((rup % 10) == 8) result = "Twenty Eight";
                if ((rup % 10) == 9) result = "Twenty Nine";
            }
            if (rup > 20 && (rup / 10) == 3 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Thirty One";
                if ((rup % 10) == 2) result = "Thirty Two";
                if ((rup % 10) == 3) result = "Thirty Three";
                if ((rup % 10) == 4) result = "Thirty Four";
                if ((rup % 10) == 5) result = "Thirty Five";
                if ((rup % 10) == 6) result = "Thirty Six";
                if ((rup % 10) == 7) result = "Thirty Seven";
                if ((rup % 10) == 8) result = "Thirty Eight";
                if ((rup % 10) == 9) result = "Thirty Nine";
            }
            if (rup > 20 && (rup / 10) == 4 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Forty One";
                if ((rup % 10) == 2) result = "Forty Two";
                if ((rup % 10) == 3) result = "Forty Three";
                if ((rup % 10) == 4) result = "Forty Four";
                if ((rup % 10) == 5) result = "Forty Five";
                if ((rup % 10) == 6) result = "Forty Six";
                if ((rup % 10) == 7) result = "Forty Seven";
                if ((rup % 10) == 8) result = "Forty Eight";
                if ((rup % 10) == 9) result = "Forty Nine";
            }
            if (rup > 20 && (rup / 10) == 5 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Fifty One";
                if ((rup % 10) == 2) result = "Fifty Two";
                if ((rup % 10) == 3) result = "Fifty Three";
                if ((rup % 10) == 4) result = "Fifty Four";
                if ((rup % 10) == 5) result = "Fifty Five";
                if ((rup % 10) == 6) result = "Fifty Six";
                if ((rup % 10) == 7) result = "Fifty Seven";
                if ((rup % 10) == 8) result = "Fifty Eight";
                if ((rup % 10) == 9) result = "Fifty Nine";
            }
            if (rup > 20 && (rup / 10) == 6 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Sixty One";
                if ((rup % 10) == 2) result = "Sixty Two";
                if ((rup % 10) == 3) result = "Sixty Three";
                if ((rup % 10) == 4) result = "Sixty Four";
                if ((rup % 10) == 5) result = "Sixty Five";
                if ((rup % 10) == 6) result = "Sixty Six";
                if ((rup % 10) == 7) result = "Sixty Seven";
                if ((rup % 10) == 8) result = "Sixty Eight";
                if ((rup % 10) == 9) result = "Sixty Nine";
            }
            if (rup > 20 && (rup / 10) == 7 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Seventy One";
                if ((rup % 10) == 2) result = "Seventy Two";
                if ((rup % 10) == 3) result = "Seventy Three";
                if ((rup % 10) == 4) result = "Seventy Four";
                if ((rup % 10) == 5) result = "Seventy Five";
                if ((rup % 10) == 6) result = "Seventy Six";
                if ((rup % 10) == 7) result = "Seventy Seven";
                if ((rup % 10) == 8) result = "Seventy Eight";
                if ((rup % 10) == 9) result = "Seventy Nine";
            }
            if (rup > 20 && (rup / 10) == 8 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Eighty One";
                if ((rup % 10) == 2) result = "Eighty Two";
                if ((rup % 10) == 3) result = "Eighty Three";
                if ((rup % 10) == 4) result = "Eighty Four";
                if ((rup % 10) == 5) result = "Eighty Five";
                if ((rup % 10) == 6) result = "Eighty Six";
                if ((rup % 10) == 7) result = "Eighty Seven";
                if ((rup % 10) == 8) result = "Eighty Eight";
                if ((rup % 10) == 9) result = "Eighty Nine";
            }
            if (rup > 20 && (rup / 10) == 9 && (rup % 10) != 0)
            {
                if ((rup % 10) == 1) result = "Ninty One";
                if ((rup % 10) == 2) result = "Ninty Two";
                if ((rup % 10) == 3) result = "Ninty Three";
                if ((rup % 10) == 4) result = "Ninty Four";
                if ((rup % 10) == 5) result = "Ninty Five";
                if ((rup % 10) == 6) result = "Ninty Six";
                if ((rup % 10) == 7) result = "Ninty Seven";
                if ((rup % 10) == 8) result = "Ninty Eight";
                if ((rup % 10) == 9) result = "Ninty Nine";
            }
            return result;
        }


    }
}

