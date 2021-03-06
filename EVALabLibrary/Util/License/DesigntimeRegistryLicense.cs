﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EVALab.Util.License
{
    /// <summary>
    /// Summary description for DesigntimeRegistryLicense.
    /// </summary>
    public class DesigntimeRegistryLicense : System.ComponentModel.License
    {
        private Type type;

        public DesigntimeRegistryLicense(Type type)
        {
            if (type == null) throw new NullReferenceException("The licensed type reference may not be null.");
            this.type = type;
        }

        public override string LicenseKey
        {
            get
            {
                // Simply return the application's GUID
                return type.GUID.ToString();
            }
        }

        public override void Dispose()
        {
        }
    }
}
