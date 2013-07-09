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
        public delegate int WMTRawHandler(WacomMTRawDataList rawPacket);

        private  static WMTFingerHandler _fingerEvent;
        private  static WMTBlobHandler _blobEvent;
        private  static WMTRawHandler _rawEvent;

        private WacomMTHitRect fingerHitRect;
        private WacomMTProcessingMode fingerProcessingMode;
        private WacomMTHitRect blobHitRect;
        private WacomMTProcessingMode blobProcessingMode;
        private WacomMTProcessingMode rawProcessingMode;

        public Dictionary<int, WacomMTCapability> capabilityMap { get; private set; }
        public List<int> deviceList {get;private set;}

        private static WacomMTDNManager _instance;

        private WMT_ATTACH_CALLBACK localAttachCallback;
        private WMT_DETACH_CALLBACK localDetachCallback;
        private WMT_FINGER_CALLBACK localFingerCallback;
        private WMT_BLOB_CALLBACK localBlobCallback;
        private WMT_RAW_CALLBACK localRawCallback;

        private WacomMTDNManager()
        {
            localAttachCallback = new WMT_ATTACH_CALLBACK(WMTAttachCallbackInternal);
            localDetachCallback = new WMT_DETACH_CALLBACK(WMTDetachCallbackInternal);
            localFingerCallback = new WMT_FINGER_CALLBACK(WMTFingerCallbackInternal);
            localBlobCallback = new WMT_BLOB_CALLBACK(WMTBlobCallbackInternal);
            localRawCallback = new WMT_RAW_CALLBACK(WMTRawCallbackInternal);

            fingerHitRect = new WacomMTHitRect();
            fingerProcessingMode = WacomMTProcessingMode.WMTProcessingModeNone;

            blobHitRect = new WacomMTHitRect();
            blobProcessingMode = WacomMTProcessingMode.WMTProcessingModeNone;

            rawProcessingMode = WacomMTProcessingMode.WMTProcessingModeNone;
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

        public void configureFingerEvent(WacomMTHitRect hitRect, WacomMTProcessingMode mode)
        {
            fingerHitRect = hitRect;
            fingerProcessingMode = mode;
        }

        public void configureBlobEvent(WacomMTHitRect hitRect, WacomMTProcessingMode mode)
        {
            blobHitRect = hitRect;
            blobProcessingMode = mode;
        }

        public void configureRawEvent(WacomMTProcessingMode mode)
        {
            rawProcessingMode = mode;
        }
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
                    if (WacomMTUtils.IsHitRectEmpty(fingerHitRect))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            WacomMTFunc.WacomMTRegisterFingerReadCallback(deviceList[i], IntPtr.Zero, fingerProcessingMode, localFingerCallback, IntPtr.Zero);
                        }
                    }
                    else
                    {
                        IntPtr hitRectPtr = WacomMTUtils.AllocUnmanagedBuf(typeof(WacomMTHitRect));
                        Marshal.StructureToPtr(fingerHitRect, hitRectPtr, false);
                        for (int i = 0; i < count; i++)
                        {
                            WacomMTFunc.WacomMTRegisterFingerReadCallback(deviceList[i], hitRectPtr, fingerProcessingMode, localFingerCallback, IntPtr.Zero);
                        }
                        WacomMTUtils.FreeUnmanagedBuf(hitRectPtr);
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
                    if (WacomMTUtils.IsHitRectEmpty(blobHitRect))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            WacomMTFunc.WacomMTRegisterBlobReadCallback(deviceList[i], IntPtr.Zero, blobProcessingMode, localBlobCallback, IntPtr.Zero);
                        }
                    }
                    else
                    {
                        IntPtr hitRectPtr = WacomMTUtils.AllocUnmanagedBuf(typeof(WacomMTHitRect));
                        Marshal.StructureToPtr(blobHitRect, hitRectPtr, false);
                        for (int i = 0; i < count; i++)
                        {
                            WacomMTFunc.WacomMTRegisterBlobReadCallback(deviceList[i], hitRectPtr, blobProcessingMode, localBlobCallback, IntPtr.Zero);
                        }
                        WacomMTUtils.FreeUnmanagedBuf(hitRectPtr);
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
                        WacomMTFunc.WacomMTRegisterRawReadCallback(deviceList[i], rawProcessingMode, localRawCallback, IntPtr.Zero);
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
            if(AttachEvent != null)
                AttachEvent(deviceInfo);
        }
        private void WMTDetachCallbackInternal(int deviceID, System.IntPtr userData)
        {
            //Remove device from the local list
            if(DetachEvent != null)
                DetachEvent(deviceID);
        }
        private int WMTFingerCallbackInternal(ref WacomMTFingerCollection fingerPacket, System.IntPtr userData)
        {
            if (_fingerEvent != null)
            {
                WacomMTFingerList list = new WacomMTFingerList(fingerPacket);
                _fingerEvent(list);
            }
            return 0;
        }
        private int WMTBlobCallbackInternal(ref WacomMTBlobAggregate blobPacket, System.IntPtr userData)
        {
            if (_blobEvent != null)
            {
                WacomMTBlobAggregateList list = new WacomMTBlobAggregateList(blobPacket);
                _blobEvent(list);
            }
            return 0;
        }
        private int WMTRawCallbackInternal(ref WacomMTRawData rawPacket, System.IntPtr userData)
        {
            if (_rawEvent != null)
            {
                WacomMTRawDataList list = new WacomMTRawDataList(rawPacket);
                _rawEvent(list);
            }
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

                //res = WacomMTFunc.WacomMTRegisterAttachCallback(localAttachCallback, IntPtr.Zero);
                res = WacomMTFunc.WacomMTRegisterDetachCallback(localDetachCallback, IntPtr.Zero);
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
