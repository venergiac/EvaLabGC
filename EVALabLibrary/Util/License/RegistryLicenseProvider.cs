using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Microsoft.Win32;

namespace EVALab.Util.License
{
    /// <summary>
    /// Summary description for RegistryLicenseProvider.
    /// </summary>
    public class RegistryLicenseProvider : LicenseProvider
    {
        public RegistryLicenseProvider()
        {
        }

        public override System.ComponentModel.License GetLicense(
            LicenseContext context,
            Type type,
            object instance,
            bool allowExceptions)
        {
            // We'll test for the usage mode...run time v. design time. Note
            // we only check if run time...
            if (context.UsageMode == LicenseUsageMode.Runtime)
            {
                // The Registry key we'll check
                RegistryKey licenseKey = Registry.CurrentUser.OpenSubKey("Software\\EvaLab\\Key");

                if (licenseKey != null)
                {
                    // Passed the first test, which is the existence of the
                    // Registry key itself. Now obtain the stored value
                    // associated with our app's GUID.
                    string strLic = (string)licenseKey.GetValue(type.GUID.ToString()); // reflected!
                    if (strLic != null)
                    {

                        long k1 = Int64.Parse(strLic);
                        long k2 = EVALab.Util.License.FakeApp.Convert(k1);
                        DateTime licenseDate = new DateTime(k2);
                        // Passed the second test, which is some value
                        // exists that's associated with our app's GUID.
                        if (licenseDate.CompareTo(DateTime.Now)>=0)
                        {
                            // Got it...valid license...
                            return new RuntimeRegistryLicense(type);
                        }
                        else
                        {
                            Registry.CurrentUser.DeleteSubKey("Software\\EvaLab\\Key");
                            throw new LicenseException(type, instance, "Your license is expired");
                        }
                    } // if
                } // if

                // If we got this far, we failed the license test. We then
                // check to see if exceptions are allowed, and if so, throw
                // a new license exception...
                if (allowExceptions == true)
                {
                    throw new LicenseException(type, instance, "Your license is invalid");
                } // if

                // Exceptions are not desired, so we'll simply return null.
                return null;
            } // if
            else
            {
                return new DesigntimeRegistryLicense(type);
            } // else
        }
    }
}
