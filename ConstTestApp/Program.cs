using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using WintabDN;

namespace ConsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestOnly.puts("nutz");
            //TestOnly._flushall();

            Test_IsWintabAvailable();
            Test_GetDeviceInfo();
            Test_GetDefaultDigitizingContext();
            Test_GetDefaultSystemContext();
            Test_GetDefaultDeviceIndex();
            Test_GetDeviceAxis();
            Test_GetDeviceOrientation();
            Test_GetDeviceRotation();
            Test_GetNumberOfDevices();
            Test_IsStylusActive();
            Test_GetStylusName();
            Test_GetExtensionMask();


        }


        ///////////////////////////////////////////////////////////////////////
        private static void Test_IsWintabAvailable()
        {
            if (CWintabInfo.IsWintabAvailable())
            {
                Console.WriteLine("Wintab was found!");
            }
            else
            {
                Console.WriteLine("Wintab was not found!\nCheck to see if tablet driver service is running.");
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDeviceInfo()
        {
            Console.WriteLine("DeviceInfo: " + CWintabInfo.GetDeviceInfo());
        }


        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDefaultDigitizingContext()
        {
            CWintabContext context = CWintabInfo.GetDefaultDigitizingContext();

            Console.WriteLine("Default Digitizing Context:");
            Console.WriteLine("\tSysOrgX, SysOrgY, SysExtX, SysExtY\t[" +
                context.SysOrgX + "," + context.SysOrgY + "," +
                context.SysExtX + "," + context.SysExtY + "]");

            Console.WriteLine("\tInOrgX, InOrgY, InExtX, InExtY\t\t[" +
                context.InOrgX + "," + context.InOrgY + "," +
                context.InExtX + "," + context.InExtY + "]");

            Console.WriteLine("\tOutOrgX, OutOrgY, OutExtX, OutExt\t[" +
                context.OutOrgX + "," + context.OutOrgY + "," +
                context.OutExtX + "," + context.OutExtY + "]");
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDefaultSystemContext()
        {
            CWintabContext context = CWintabInfo.GetDefaultSystemContext();

            Console.WriteLine("Default System Context:");
            Console.WriteLine("\tSysOrgX, SysOrgY, SysExtX, SysExtY\t[" +
                context.SysOrgX + "," + context.SysOrgY + "," +
                context.SysExtX + "," + context.SysExtY + "]");

            Console.WriteLine("\tInOrgX, InOrgY, InExtX, InExtY\t\t[" +
                context.InOrgX + "," + context.InOrgY + "," +
                context.InExtX + "," + context.InExtY + "]");

            Console.WriteLine("\tOutOrgX, OutOrgY, OutExtX, OutExt\t[" +
                context.OutOrgX + "," + context.OutOrgY + "," +
                context.OutExtX + "," + context.OutExtY + "]");
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDefaultDeviceIndex()
        {
            Int32 devIndex = CWintabInfo.GetDefaultDeviceIndex();

            Console.WriteLine("Default device index is: " + devIndex + (devIndex == -1 ? " (virtual device)" : ""));
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDeviceAxis()
        {
            WintabAxis axis;

            // HEYBOB - TODO check returned values against win32 wintab program 

            // Get virtual device axis for X, Y and Z.
            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_X);
            Console.WriteLine("Device axis X for virtual device:");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution);

            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_Y);
            Console.WriteLine("Device axis Y for virtual device:");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution);

            axis = CWintabInfo.GetDeviceAxis(-1, EAxisDimension.AXIS_Z);
            Console.WriteLine("Device axis Z for virtual device:");
            Console.WriteLine("\taxMin, axMax, axUnits, axResolution: " + axis.axMin + "," + axis.axMax + "," + axis.axUnits + "," + axis.axResolution);
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDeviceOrientation()
        {
            bool tiltSupported = false;
            WintabAxisArray axisArray = CWintabInfo.GetDeviceOrientation(out tiltSupported);
            Console.WriteLine("Device orientation:");
            Console.WriteLine("\ttilt supported for current tablet: " + (tiltSupported ? "YES" : "NO"));

            if (tiltSupported)
            {
                for (int idx = 0; idx < axisArray.array.Length; idx++)
                {
                Console.WriteLine("\t[" + idx + "] axMin, axMax, axResolution, axUnits: " +
                    axisArray.array[idx].axMin + "," +
                    axisArray.array[idx].axMax + "," +
                    axisArray.array[idx].axResolution + "," +
                    axisArray.array[idx].axUnits);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetDeviceRotation()
        {
            bool rotationSupported = false;
            WintabAxisArray axisArray = CWintabInfo.GetDeviceRotation(out rotationSupported);
            Console.WriteLine("Device rotation:");
            Console.WriteLine("\trotation supported for current tablet: " + (rotationSupported ? "YES" : "NO"));

            if (rotationSupported)
            {
                for (int idx = 0; idx < axisArray.array.Length; idx++)
                {
                Console.WriteLine("\t[" + idx + "] axMin, axMax, axResolution, axUnits: " +
                    axisArray.array[idx].axMin + "," +
                    axisArray.array[idx].axMax + "," +
                    axisArray.array[idx].axResolution + "," +
                    axisArray.array[idx].axUnits);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetNumberOfDevices()
        {
            UInt32 numDevices = CWintabInfo.GetNumberOfDevices();
            Console.WriteLine("Number of tablets connected: " + numDevices);
        }


        ///////////////////////////////////////////////////////////////////////
        private static void Test_IsStylusActive()
        {
            bool isStylusActive = CWintabInfo.IsStylusActive();
            Console.WriteLine("Is stylus active: " + (isStylusActive ? "YES" : "NO"));
        }


        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetStylusName()
        {
            Console.WriteLine("Stylus name (puck):   " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_PUCK));
            Console.WriteLine("Stylus name (pen):    " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_PRESSURE_STYLUS));
            Console.WriteLine("Stylus name (eraser): " + CWintabInfo.GetStylusName(EWTICursorNameIndex.CSR_NAME_ERASER));
       }

        ///////////////////////////////////////////////////////////////////////
        private static void Test_GetExtensionMask()
        {
            Console.WriteLine("Extension touchring mask:   0x" + CWintabInfo.GetExtensionMask(EWTXExtensionTag.WTX_TOUCHRING).ToString("x"));
            Console.WriteLine("Extension touchstring mask: 0x" + CWintabInfo.GetExtensionMask(EWTXExtensionTag.WTX_TOUCHSTRIP).ToString("x"));
            Console.WriteLine("Extension express key mask: 0x" + CWintabInfo.GetExtensionMask(EWTXExtensionTag.WTX_EXPKEYS2).ToString("x"));
        }

    } // end class Program
}
