using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WacomMTDN
{
    /// <summary>
    /// Managed implementation of Wintab HWND typedef. 
    /// Holds native Window handle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct HWND
    {
        // \cond IGNORED_BY_DOXYGEN
        [MarshalAs(UnmanagedType.I4)]
        public IntPtr value;

        public HWND(IntPtr value)
        { this.value = value; }

        public static implicit operator IntPtr(HWND hwnd_I)
        { return hwnd_I.value; }

        public static implicit operator HWND(IntPtr ptr_I)
        { return new HWND(ptr_I); }

        public static bool operator ==(HWND hwnd1, HWND hwnd2)
        { return hwnd1.value == hwnd2.value; }

        public static bool operator !=(HWND hwnd1, HWND hwnd2)
        { return hwnd1.value != hwnd2.value; }

        public override bool Equals(object obj)
        { return (HWND)obj == this; }

        public override int GetHashCode()
        { return 0; }
        // \endcond IGNORED_BY_DOXYGEN
    }

    public enum WacomMTError
    {

        /// WMTErrorSuccess -> 0
        WMTErrorSuccess = 0,

        /// WMTErrorDriverNotFound -> 1
        WMTErrorDriverNotFound = 1,

        /// WMTErrorBadVersion -> 2
        WMTErrorBadVersion = 2,

        /// WMTErrorAPIOutdated -> 3
        WMTErrorAPIOutdated = 3,

        /// WMTErrorInvalidParam -> 4
        WMTErrorInvalidParam = 4,

        /// WMTErrorQuit -> 5
        WMTErrorQuit = 5,

        /// WMTErrorBufferTooSmall -> 6
        WMTErrorBufferTooSmall = 6,
    }

    public enum WacomMTDeviceType
    {

        /// WMTDeviceTypeOpaque -> 0
        WMTDeviceTypeOpaque = 0,

        /// WMTDeviceTypeIntegrated -> 1
        WMTDeviceTypeIntegrated = 1,
    }

    public enum _WacomMTCapabilityFlags
    {

        /// WMTCapabilityFlagsRawAvailable -> (1<<0)
        WMTCapabilityFlagsRawAvailable = (1) << (0),

        /// WMTCapabilityFlagsBlobAvailable -> (1<<1)
        WMTCapabilityFlagsBlobAvailable = (1) << (1),

        /// WMTCapabilityFlagsSensitivityAvailable -> (1<<2)
        WMTCapabilityFlagsSensitivityAvailable = (1) << (2),

        /// WMTCapabilityFlagsReserved -> (1<<31)
        WMTCapabilityFlagsReserved = (1) << (31),
    }

    public enum WacomMTFingerState
    {

        /// WMTFingerStateNone -> 0
        WMTFingerStateNone = 0,

        /// WMTFingerStateDown -> 1
        WMTFingerStateDown = 1,

        /// WMTFingerStateHold -> 2
        WMTFingerStateHold = 2,

        /// WMTFingerStateUp -> 3
        WMTFingerStateUp = 3,
    }

    public enum WacomMTBlobType
    {

        /// WMTBlobTypePrimary -> 0
        WMTBlobTypePrimary = 0,

        /// WMTBlobTypeVoid -> 1
        WMTBlobTypeVoid = 1,
    }

    public enum WacomMTProcessingMode
    {

        /// WMTProcessingModeNone -> 0
        WMTProcessingModeNone = 0,

        /// WMTProcessingModeObserver -> (1<<0)
        WMTProcessingModeObserver = (1) << (0),

        /// WMTProcessingModeChildofBrowser -> (1<<30)
        WMTProcessingModeChildofBrowser = (1) << (30),

        /// WMTProcessingModeReserved -> (1<<31)
        WMTProcessingModeReserved = (1) << (31),
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTCapability
    {

        /// int
        public int Version;

        /// int
        public int DeviceID;

        /// WacomMTDeviceType->_WacomMTDeviceType
        public WacomMTDeviceType Type;

        /// float
        public float LogicalOriginX;

        /// float
        public float LogicalOriginY;

        /// float
        public float LogicalWidth;

        /// float
        public float LogicalHeight;

        /// float
        public float PhysicalSizeX;

        /// float
        public float PhysicalSizeY;

        /// int
        public int ReportedSizeX;

        /// int
        public int ReportedSizeY;

        /// int
        public int ScanSizeX;

        /// int
        public int ScanSizeY;

        /// int
        public int FingerMax;

        /// int
        public int BlobMax;

        /// int
        public int BlobPointsMax;

        /// WacomMTCapabilityFlags->int
        public int CapabilityFlags;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTFinger
    {

        /// int
        public int FingerID;

        /// float
        public float X;

        /// float
        public float Y;

        /// float
        public float Width;

        /// float
        public float Height;

        /// unsigned short
        public ushort Sensitivity;

        /// float
        public float Orientation;

        /// boolean
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public bool Confidence;

        /// WacomMTFingerState->_WacomMTFingerState
        public WacomMTFingerState TouchState;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTFingerCollection
    {

        /// int
        public int Version;

        /// int
        public int DeviceID;

        /// int
        public int FrameNumber;

        /// int
        public int FingerCount;

        /// WacomMTFinger*
        public System.IntPtr Fingers;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTBlobPoint
    {

        /// float
        public float X;

        /// float
        public float Y;

        /// unsigned short
        public ushort Sensitivity;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTBlob
    {

        /// int
        public int BlobID;

        /// float
        public float X;

        /// float
        public float Y;

        /// boolean
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public bool Confidence;

        /// WacomMTBlobType->_WacomMTBlobType
        public WacomMTBlobType BlobType;

        /// int
        public int ParentID;

        /// int
        public int PointCount;

        /// WacomMTBlobPoint*
        public System.IntPtr BlobPoints;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTBlobAggregate
    {

        /// int
        public int Version;

        /// int
        public int DeviceID;

        /// int
        public int FrameNumber;

        /// int
        public int BlobCount;

        /// WacomMTBlob*
        public System.IntPtr BlobArray;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTRawData
    {

        /// int
        public int Version;

        /// int
        public int DeviceID;

        /// int
        public int FrameNumber;

        /// int
        public int ElementCount;

        /// unsigned short*
        public System.IntPtr Sensitivity;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WacomMTHitRect
    {

        /// float
        public float originX;

        /// float
        public float originY;

        /// float
        public float width;

        /// float
        public float height;

    }

    public class WacomMTFingerList
    {
        /// int
        public int Version{get;private set;}

        /// int
        public int DeviceID{get;private set;}

        /// int
        public int FrameNumber{get;private set;}

        /// WacomMTFinger*
        public List<WacomMTFinger> Fingers { get; private set; }

        public WacomMTFingerList(WacomMTFingerCollection collection)
        {
            Version = collection.Version;
            DeviceID = collection.DeviceID;
            FrameNumber = collection.FrameNumber;
            Fingers = WacomMTUtils.MarshalPtrToStructArray<WacomMTFinger>(collection.Fingers, collection.FingerCount);
        }
    }

    public class WacomMTBlobList
    {
        /// int
        public int BlobID { get; private set; }

        /// float
        public float X { get; private set; }

        /// float
        public float Y { get; private set; }

        /// boolean
        public bool Confidence { get; private set; }

        /// WacomMTBlobType->_WacomMTBlobType
        public WacomMTBlobType BlobType { get; private set; }

        /// int
        public int ParentID { get; private set; }

        /// WacomMTBlobPoint*
        public List<WacomMTBlobPoint> BlobPoints { get; private set; }

        public WacomMTBlobList(WacomMTBlob blob)
        {
            BlobID = blob.BlobID;
            X = blob.X;
            Y = blob.Y;
            Confidence = blob.Confidence;
            BlobType = blob.BlobType;
            ParentID = blob.ParentID;
            BlobPoints = WacomMTUtils.MarshalPtrToStructArray<WacomMTBlobPoint>(blob.BlobPoints, blob.PointCount);
        }
    }

    public class WacomMTBlobAggregateList
    {
        /// int
        public int Version { get; private set; }

        /// int
        public int DeviceID { get; private set; }

        /// int
        public int FrameNumber { get; private set; }

        /// WacomMTBlobList*
        public List<WacomMTBlobList> BlobList { get; private set; }

        public WacomMTBlobAggregateList(WacomMTBlobAggregate aggr)
        {
            Version = aggr.Version;
            DeviceID = aggr.DeviceID;
            FrameNumber = aggr.FrameNumber;
            BlobList = WacomMTUtils.MarshalPtrToBlobAggregateList(aggr.BlobArray, aggr.BlobCount);
        }
    }

    public class WacomMTRawDataList
    {
        /// int
        public int Version { get; private set; }

        /// int
        public int DeviceID { get; private set; }

        /// int
        public int FrameNumber { get; private set; }

        /// unsigned short*
        public List<ushort> SensitivityList { get; private set; }

        public WacomMTRawDataList(WacomMTRawData data)
        {
            Version = data.Version;
            DeviceID = data.DeviceID;
            FrameNumber = data.FrameNumber;
            SensitivityList = WacomMTUtils.MarshalPtrToStructArray<ushort>(data.Sensitivity, data.ElementCount);
        }
    }
}
