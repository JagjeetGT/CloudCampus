using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web.Configuration;
using System.Web.Mvc;

namespace KRBAccounting.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public static string Validated(ICollection<ModelState> modelState)
        {
            foreach (var state in modelState.Where(state => state.Errors.Any()))
            {
                return state.Errors[0].ErrorMessage;
            }
            return string.Empty;
        }

        public ActionResult LicenseExpired()
        {
            //var gateway = "";
            //NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            //foreach (NetworkInterface adapter in adapters)
            //{
            //    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            //    GatewayIPAddressInformationCollection addresses =
            //        adapterProperties.GatewayAddresses;
            //    if (addresses.Count > 0)
            //    {
            //        foreach (GatewayIPAddressInformation address in addresses)
            //        {
            //            gateway = address.Address.ToString();
            //        }
            //    }
            //}
            //var test = gateway;
            return View();
        }

        [HttpPost]
        public ActionResult CheckLicense(string License)
        {
            var success = KRBAccounting.Web.Helpers.EncryptionService.CheckLicense(License);
            var msg = string.Empty;
            if (success)
            {
                msg = "Thank You for purchasing our product.";
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Request.ApplicationPath);
                config.AppSettings.Settings.Remove("eSetting");
                config.AppSettings.Settings.Add("eSetting", License);
                config.Save();
            }
            else
            {
                msg = "The license you provided is not valid. Please enter correct License";
            }

            return Json(new { success = success, msg = msg });
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult Error(string err)
        {
            return View(err);
        }

    }
}
