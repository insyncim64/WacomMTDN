using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WacomMTDN
{
    public class WacomMTDNManager
    {
        public delegate void WMTAttachHandler(WacomMTCapability deviceInfo);
        public delegate void WMTDetachHandler(int deviceID);
        public delegate int WMTFingerHandler(WacomMTFingerList fingerPacket);
        public delegate int WMTBlobHandler(WacomMTBlobAggregateList blobPacket);
        public delegate int WMTRawHandler(WacomMTRawDataList blobPacket);

        private  WMTFingerHandler _fingerEvent;
        private  WMTBlobHandler _blobEvent;
        private  WMTRawHandler _rawEvent;

        public Dictionary<int, WacomMTCapability> capabilityMap { get; private set; }
        public List<int> deviceList {get;private set;}

        private static WacomMTDNManager _instance;

        private WacomMTDNManager()
        {
        }

        public static WacomMTDNManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WacomMTDNManager();
            }
            return _instance;
        }

        /// <summary>
        /// An internal handler will be automaticall attached in the init.
        /// No further attachment should be made here.
        /// </summary>
        public event WMTAttachHandler AttachEvent;

        /// <summary>
        /// An internal handler will be automaticall attached in the init.
        /// No further attachment should be made here.
        /// </summary>
        public event WMTDetachHandler DetachEvent;

        /// <summary>
        /// An internal handler should be set to capture the callback
        /// </summary>
        public  event WMTFingerHandler FingerEvent
        {
            add
            {
                if (_fingerEvent==null && deviceList != null)
                {
                    int count = deviceList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        WacomMTFunc.WacomMTRegisterFingerReadCallback(deviceList[i], IntPtr.Zero, WacomMTProcessingMode.WMTProcessingModeNone, WMTFingerCallbackInternal, IntPtr.Zero);
                    }
                }
                _fingerEvent += value;
            }

            remove
            {
                _fingerEvent -= value;
            }
        }

        /// <summary>
        /// An internal handler should be set to capture the callback
        /// </summary>
        public  event WMTBlobHandler BlobEvent
        {
            add
            {
                if (_blobEvent == null && deviceList != null)
                {
                    int count = deviceList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        WacomMTFunc.WacomMTRegisterBlobReadCallback(deviceList[i], IntPtr.Zero, WacomMTProcessingMode.WMTProcessingModeNone, WMTBlobCallbackInternal, IntPtr.Zero);
                    }
                }
                _blobEvent += value;
            }

            remove
            {
                _blobEvent -= value;
            }
        }

        /// <summary>
        /// An internal handler should be set to capture the callback
        /// </summary>
        public  event WMTRawHandler RawEvent
        {
            add
            {
                if (_rawEvent == null && deviceList != null)
                {
                    int count = deviceList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        WacomMTFunc.WacomMTRegisterRawReadCallback(deviceList[i], WacomMTProcessingMode.WMTProcessingModeNone, WMTRawCallbackInternal, IntPtr.Zero);
                    }
                }
                _rawEvent += value;
            }

            remove
            {
                _rawEvent -= value;
            }
        }

        private void WMTAttachCallbackInternal(WacomMTCapability deviceInfo, System.IntPtr userData)
        {
            //Add new device into the local list
            AttachEvent(deviceInfo);
        }

        private void WMTDetachCallbackInternal(int deviceID, System.IntPtr userData)
        {
            //Remove device from the local list
            DetachEvent(deviceID);
        }
        private int WMTFingerCallbackInternal(ref WacomMTFingerCollection fingerPacket, System.IntPtr userData)
        {
            WacomMTFingerList list = new WacomMTFingerList(fingerPacket);
            _fingerEvent(list);
            return 0;
        }
        private int WMTBlobCallbackInternal(ref WacomMTBlobAggregate blobPacket, System.IntPtr userData)
        {
            WacomMTBlobAggregateList list = new WacomMTBlobAggregateList(blobPacket);
            _blobEvent(list);
            return 0;
        }
        private int WMTRawCallbackInternal(ref WacomMTRawData rawPacket, System.IntPtr userData)
        {
            WacomMTRawDataList list = new WacomMTRawDataList(rawPacket);
            _rawEvent(list);
            return 0;
        }

        public WacomMTError WacomMTRegisterFingerReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth)
        {
            return WacomMTFunc.WacomMTRegisterFingerReadHWND(deviceID, mode, hWnd, bufferDepth);
        }

        public WacomMTError WacomMTRegisterBlobReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth)
        {
            return WacomMTFunc.WacomMTRegisterBlobReadHWND(deviceID, mode, hWnd, bufferDepth);
        }

        public WacomMTError WacomMTRegisterRawReadHWND(int deviceID, WacomMTProcessingMode mode, System.IntPtr hWnd, int bufferDepth)
        {
            return WacomMTFunc.WacomMTRegisterRawReadHWND(deviceID, mode, hWnd, bufferDepth);
        }
        
        public  WacomMTError WacomMTInitialize(int libraryAPIVersion)
        {
            //register the handlers for internal events
            WacomMTError res = WacomMTFunc.WacomMTInitialize(libraryAPIVersion);
            if (res == WacomMTError.WMTErrorSuccess)
            {
                deviceList = WacomMTGetAttachedDeviceIDs();
                if (deviceList != null)
                {
                    int deviceCount = deviceList.Count;
                    if (deviceCount > 0)
                    {
                        capabilityMap = new Dictionary<int, WacomMTCapability>();
                        int loopCount = deviceCount;
                        for (int idx = 0; idx < loopCount; idx++)
                        {
                            int deviceID = deviceList[idx];
                            WacomMTCapability capa = WacomMTGetDeviceCapabilities(deviceID);
                            capabilityMap.Add(deviceID, capa);
                        }
                    }
                }
                res = WacomMTFunc.WacomMTRegisterAttachCallback(WMTAttachCallbackInternal, IntPtr.Zero);
                res = WacomMTFunc.WacomMTRegisterDetachCallback(WMTDetachCallbackInternal, IntPtr.Zero);
            }
            return res;
        }

        public  void WacomMTQuit()
        {
            //remove the handlers
            WacomMTFunc.WacomMTQuit();
        }

        /// <summary>
        /// Get the capability of a specific device
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public  WacomMTCapability WacomMTGetDeviceCapabilities(int deviceID)
        {
            WacomMTCapability capa = new WacomMTCapability();
            WacomMTFunc.WacomMTGetDeviceCapabilities(deviceID, ref capa);
            return capa;
        }

        /// <summary>
        /// Get a list of the current attached device
        /// </summary>
        /// <returns></returns>
        public  List<int> WacomMTGetAttachedDeviceIDs() 
        {
            int deviceCount = WacomMTFunc.WacomMTGetAttachedDeviceIDs(IntPtr.Zero, ((UIntPtr)(uint)0));
            if (deviceCount > 0)
            {
                int newCount = 0;
                IntPtr mainPtr = IntPtr.Zero;
                while (newCount != deviceCount)
                {
                    if (mainPtr != IntPtr.Zero)
                    {
                        WacomMTUtils.FreeUnmanagedBuf(mainPtr);
                    }
                    mainPtr = WacomMTUtils.AllocUnmanagedBuf(deviceCount * Marshal.SizeOf(typeof(int)));
                    newCount = WacomMTFunc.WacomMTGetAttachedDeviceIDs(mainPtr, (UIntPtr)((uint)(deviceCount * Marshal.SizeOf(typeof(int)))));
                }
                deviceList = WacomMTUtils.MarshalPtrToArray<int>(mainPtr, deviceCount);
                WacomMTUtils.FreeUnmanagedBuf(mainPtr);
                return deviceList;
            }
            else
            {
                return null;
            }  
        }
    }
}
