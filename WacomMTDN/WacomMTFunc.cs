using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WacomMTDN
{
    public partial class NativeConstants
    {

        /// WacomMultitouch_h -> 
        /// Error generating expression: Value cannot be null.
        ///Parameter name: node
        public const string WacomMultitouch_h = "";

        /// WMT_EXPORT -> 
        /// Error generating expression: Value cannot be null.
        ///Parameter name: node
        public const string WMT_EXPORT = "";

        /// WM_FINGERDATA -> 0x6205
        public const int WM_FINGERDATA = 25093;

        /// WM_BLOBDATA -> 0x6206
        public const int WM_BLOBDATA = 25094;

        /// WM_RAWDATA -> 0x6207
        public const int WM_RAWDATA = 25095;

        /// WACOM_MULTI_TOUCH_API_VERSION -> 3
        public const int WACOM_MULTI_TOUCH_API_VERSION = 3;
    }
    
    /// Return Type: void
    ///deviceInfo: WacomMTCapability->_WacomMTCapability
    ///userData: void*
    public delegate void WMT_ATTACH_CALLBACK(WacomMTCapability deviceInfo, System.IntPtr userData);

    /// Return Type: void
    ///deviceID: int
    ///userData: void*
    public delegate void WMT_DETACH_CALLBACK(int deviceID, System.IntPtr userData);

    /// Return Type: int
    ///fingerPacket: WacomMTFingerCollection*
    ///userData: void*
    public delegate int WMT_FINGER_CALLBACK(ref WacomMTFingerCollection fingerPacket, System.IntPtr userData);

    /// Return Type: int
    ///blobPacket: WacomMTBlobAggregate*
    ///userData: void*
    public delegate int WMT_BLOB_CALLBACK(ref WacomMTBlobAggregate blobPacket, System.IntPtr userData);

    /// Return Type: int
    ///blobPacket: WacomMTRawData*
    ///userData: void*
    public delegate int WMT_RAW_CALLBACK(ref WacomMTRawData blobPacket, System.IntPtr userData);

    public class WacomMTFunc
    {
        /// Return Type: WacomMTError->_WacomMTError
        ///libraryAPIVersion: int
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTInitialize")]
        public static extern WacomMTError WacomMTInitialize(int libraryAPIVersion);


        /// Return Type: void
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTQuit")]
        public static extern void WacomMTQuit();


        /// Return Type: int
        ///deviceArray: int*
        ///bufferSize: size_t->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTGetAttachedDeviceIDs")]
        public static extern int WacomMTGetAttachedDeviceIDs(IntPtr intPtr, UIntPtr bufferSize);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///capabilityBuffer: WacomMTCapability*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTGetDeviceCapabilities")]
        public static extern WacomMTError WacomMTGetDeviceCapabilities(int deviceID, ref WacomMTCapability capabilityBuffer);


        /// Return Type: WacomMTError->_WacomMTError
        ///attachCallback: WMT_ATTACH_CALLBACK
        ///userData: void*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterAttachCallback")]
        public static extern WacomMTError WacomMTRegisterAttachCallback(WMT_ATTACH_CALLBACK attachCallback, System.IntPtr userData);


        /// Return Type: WacomMTError->_WacomMTError
        ///detachCallback: WMT_DETACH_CALLBACK
        ///userData: void*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterDetachCallback")]
        public static extern WacomMTError WacomMTRegisterDetachCallback(WMT_DETACH_CALLBACK detachCallback, System.IntPtr userData);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///hitRect: WacomMTHitRect*
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///fingerCallback: WMT_FINGER_CALLBACK
        ///userData: void*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterFingerReadCallback")]
        public static extern WacomMTError WacomMTRegisterFingerReadCallback(int deviceID, System.IntPtr hitRect, WacomMTProcessingMode mode, WMT_FINGER_CALLBACK fingerCallback, System.IntPtr userData);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///hitRect: WacomMTHitRect*
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///blobCallback: WMT_BLOB_CALLBACK
        ///userData: void*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterBlobReadCallback")]
        public static extern WacomMTError WacomMTRegisterBlobReadCallback(int deviceID, System.IntPtr hitRect, WacomMTProcessingMode mode, WMT_BLOB_CALLBACK blobCallback, System.IntPtr userData);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///rawCallback: WMT_RAW_CALLBACK
        ///userData: void*
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterRawReadCallback")]
        public static extern WacomMTError WacomMTRegisterRawReadCallback(int deviceID, WacomMTProcessingMode mode, WMT_RAW_CALLBACK rawCallback, System.IntPtr userData);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///hWnd: HWND->HWND__*
        ///bufferDepth: int
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterFingerReadHWND")]
        public static extern WacomMTError WacomMTRegisterFingerReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///hWnd: HWND->HWND__*
        ///bufferDepth: int
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterBlobReadHWND")]
        public static extern WacomMTError WacomMTRegisterBlobReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth);


        /// Return Type: WacomMTError->_WacomMTError
        ///deviceID: int
        ///mode: WacomMTProcessingMode->_WacomMTProcessingMode
        ///hWnd: HWND->HWND__*
        ///bufferDepth: int
        [System.Runtime.InteropServices.DllImportAttribute("wacommt.dll", EntryPoint = "WacomMTRegisterRawReadHWND")]
        public static extern WacomMTError WacomMTRegisterRawReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth);
    }
}
