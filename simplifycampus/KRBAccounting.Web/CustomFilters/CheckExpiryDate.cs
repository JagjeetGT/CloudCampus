using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using KRBAccounting.Web.Helpers;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckExpiryDate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cipherDate = ConfigurationManager.AppSettings["eSetting"];
            string mac = MacAddress.GetMACAddress();

            string passPhrase = "eSetting";        // can be any string
            string saltValue = mac;        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = ConfigurationManager.AppSettings["initVector"]; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128
            /*string decrypted = EncryptionService.Decrypt(cipherDate,
                                                    passPhrase,
                                                    saltValue,
                                                    hashAlgorithm,
                                                    passwordIterations,
                                                    initVector,
                                                    keySize);
            DateTime expiryDate = DateTime.Now;
            try
            {
                expiryDate = DateTime.ParseExact(decrypted, "d",
                                                       CultureInfo.InvariantCulture);
                var serverDate = DateTime.Now;
                if (expiryDate < serverDate)
                {
                    filterContext.Result = new RedirectResult("~/License-Expired");

                }
            }
            catch(Exception ex)
            {
                filterContext.Result = new RedirectResult("~/License-Expired");
            }
            
            */
            
        }
    }
}