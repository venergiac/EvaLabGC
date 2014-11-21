using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace EVALab.Util.Output
{
    public class Graphic
    {

        public static string[] GetAdapterList()
        {
            string[] adapters = new string[Manager.Adapters.Count];
            int i = 0;
            foreach (AdapterInformation AdaptInfo in Manager.Adapters)
            {
                adapters[i++] = AdaptInfo.Information.Description;
            }
            return adapters;
        }

        public static int GetRefreshRate(int idxAdapter)
        {
            return Manager.Adapters[idxAdapter].CurrentDisplayMode.RefreshRate;
        }


    }
}
