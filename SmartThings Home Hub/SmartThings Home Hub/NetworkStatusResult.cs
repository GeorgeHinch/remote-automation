using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Hub
{
    public class NetworkStatusResult
    {
        String ssid;

        NetworkResultSignalStrength sigStrength;

        public string Ssid
        {
            get
            {
                return ssid;
            }

            set
            {
                ssid = value;
            }
        }

        internal NetworkResultSignalStrength SigStrength
        {
            get
            {
                return sigStrength;
            }

            set
            {
                sigStrength = value;
            }
        }
    }
}
