using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace CustomSetup
{
    [RunInstaller(true)]
    public partial class ConfiguratorInstaller : System.Configuration.Install.Installer
    {
        public ConfiguratorInstaller()
        {
            InitializeComponent();
        }
    }
}
