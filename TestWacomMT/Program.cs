using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WacomMTDN;

namespace TestWacomMT
{
    class Program
    {
        static void Main(string[] args)
        {
            WacomMTError error = WacomMTDNManager.GetInstance().WacomMTInitialize(WacomMTDN.NativeConstants.WACOM_MULTI_TOUCH_API_VERSION);
            if (error == WacomMTError.WMTErrorSuccess)
            {
                WacomMTDNManager.GetInstance().FingerEvent += myWMTFingerHandler;
                Console.WriteLine("Success!");
            }
        }

        public static int myWMTFingerHandler(WacomMTFingerList fingerPacket)
        {
            Console.WriteLine("New values.\n");
            return 0;
        }
    }
}
