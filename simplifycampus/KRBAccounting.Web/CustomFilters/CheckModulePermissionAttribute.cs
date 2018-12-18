using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Web.Helpers;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckModulePermissionAttribute : ActionFilterAttribute
    {
        public string Module = string.Empty;
        public CheckModulePermissionAttribute(string ModuleName)
        {
            Module = ModuleName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cipherText = ConfigurationManager.AppSettings["eSetting"];
            string mac = MacAddress.GetMACAddress();

            //string passPhrase = Module;        // can be any string
            //string saltValue = hdSerial;        // can be any string
            //string hashAlgorithm = "SHA1";             // can be "MD5"
            //int passwordIterations = 2;                  // can be any number
            //string initVector = ConfigurationManager.AppSettings["initVector"]; // must be 16 bytes
            //int keySize = 256;                // can be 192 or 128
            //string decrypted = EncryptionService.Decrypt(cipherText,
            //                                        passPhrase,
            //                                        saltValue,
            //                                        hashAlgorithm,
            //                                        passwordIterations,
            //                                        initVector,
            //                                        keySize);

            string passPhrase = "b6ww5XfanLwaUW4k";        // can be any string
            string saltValue = "JvRbrKwmwDUrzNvV";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "p#NfthB!xg&$EN%U"; // must be 16 bytes; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128

           /* string decrypted = EncryptionService.Decrypt(cipherText,
                                                    passPhrase,
                                                    saltValue,
                                                    hashAlgorithm,
                                                    passwordIterations,
                                                    initVector,
                                                    keySize);*/

            //return decrypted;
            //0=Mac Address
            //1=Key Generation Date
            //2=Key Expiry Date
            //3=Total months
            //4=Modules
            /*try
            {
                var arrDecrypted = decrypted.Split(';');
                if (mac != arrDecrypted[0])
                    filterContext.Result = new RedirectResult("~/License-Expired");
                else
                {
                    if (arrDecrypted[3] != "0")
                    {
                        var expiryDate = Convert.ToDateTime(arrDecrypted[2]).Date;
                        var currentDate = DateTime.Now.Date;
                        if (expiryDate < currentDate)
                            filterContext.Result = new RedirectResult("~/License-Expired");
                    }
                    if (!string.IsNullOrEmpty(Module))
                    {
                        var arrModules = arrDecrypted[4].Split(',');
                        if (!arrModules.Contains(Module))
                            filterContext.Result = new RedirectResult("~/License-Expired");
                    }
                }
                
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/License-Expired");
            }*/
            //if (decrypted != "True")
            //{
            //    filterContext.Result = new RedirectResult("~/404");
            //}

        }
    }
}