using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace EVALab.Util.License
{
    [GuidAttribute("0017106c-e5f2-4401-8e7a-df3a2715fbee")]
    [LicenseProvider(typeof(RegistryLicenseProvider))]
    public class FakeApp
    {
        public static long Convert(DateTime time)
        {
            return Convert(time.Ticks);
        }

        public static long Convert(long time)
        {
            string key = "17111973";
            int kint = key.GetHashCode();

            return time ^ kint;
        }


    }
}
