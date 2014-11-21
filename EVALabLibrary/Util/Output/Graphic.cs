#define XNA

using System;
using System.Collections.Generic;
using System.Text;

#if XNA
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;

namespace EVALab.Util.Output
{
    public class Graphic
    {

        public static string[] GetAdapterList()
        {
            string[] adapters = new string[GraphicsAdapter.Adapters.Count];
            int i = 0;
            foreach (GraphicsAdapter adapter in GraphicsAdapter.Adapters)
            {
                adapters[i++] = adapter.Description;
            }      

            return adapters;
        }

        public static int GetRefreshRate(int idxAdapter)
        {
            return 1000; //GraphicsAdapter.Adapters[idxAdapter].CurrentDisplayMode
        }


    }
}

#else

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
#endif