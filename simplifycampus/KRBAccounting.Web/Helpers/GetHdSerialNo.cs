using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Web;

namespace KRBAccounting.Web.Helpers
{
    public class HardDrive
    {
        private string model = null;
        private string type = null;
        private string serialNo = null;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
    }
    public static class GetHdSerialNo
    {
        public static string GetSerialNo()
        {
            var serialNo = string.Empty;
            ArrayList hdCollection = new ArrayList();

            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();

                hdCollection.Add(hd);
            }

            searcher = new
                ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                if (i == 0)
                {

                    HardDrive hd = (HardDrive)hdCollection[i];

                    // get the hardware serial no.
                    if (wmi_HD["SerialNumber"] == null)
                        hd.SerialNo = "None";
                    else
                    {

                        hd.SerialNo = wmi_HD["SerialNumber"].ToString();
                        serialNo = hd.SerialNo;
                    }
                }

                ++i;
            }

            //// Display available hard drives
            //foreach(HardDrive hd in hdCollection)
            //{
            //    Console.WriteLine("Model\t\t: " + hd.Model);
            //    Console.WriteLine("Type\t\t: " + hd.Type);
            //    Console.WriteLine("Serial No.\t: " + hd.SerialNo);
            //    Console.WriteLine();
            //}

            //// Pause application
            //Console.WriteLine("Press [Enter] to exit...");
            //Console.ReadLine();
            return serialNo;
        }
    }
}