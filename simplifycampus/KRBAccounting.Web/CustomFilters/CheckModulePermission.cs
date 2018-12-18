using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using System.Linq;
using System.Web;
using KRBAccounting.Web.Helpers;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckModulePermission
    {
        public static bool CheckModule(string Module)
        {
            var cipherText = ConfigurationManager.AppSettings["eSetting"];
            string passPhrase = "b6ww5XfanLwaUW4k";        // can be any string
            string saltValue = "JvRbrKwmwDUrzNvV";        // can be any string
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "p#NfthB!xg&$EN%U"; // must be 16 bytes; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128
            return true;
            string decrypted = EncryptionService.Decrypt(cipherText,
                                                    passPhrase,
                                                    saltValue,
                                                    hashAlgorithm,
                                                    passwordIterations,
                                                    initVector,
                                                    keySize);
            if (Module == "Accounting")
                return true;
            try
            {
                var arrDecrypted = decrypted.Split(';');
                string mac = MacAddress.GetMACAddress();
                if (mac != arrDecrypted[0])
                    return false;
                if (!string.IsNullOrEmpty(Module))
                {
                    var arrModules = arrDecrypted[4].Split(',');
                    if (arrModules.Contains(Module))
                        return true;
                }
                //if (mac != arrDecrypted[0])
                //    return false;
                //if (arrDecrypted[3] != "0")
                //{
                //    var expiryDate = Convert.ToDateTime(arrDecrypted[2]).Date;
                //    var currentDate = DateTime.Now.Date;
                //    if (expiryDate < currentDate)
                //        return false;
                //    return true;
                //}
                //var expiryDate = Convert.ToDateTime(arrDecrypted[2]);
                //if (EntityFunctions.TruncateTime(expiryDate) > EntityFunctions.TruncateTime(DateTime.Now))
                //{
                //    return true;
                //}
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}