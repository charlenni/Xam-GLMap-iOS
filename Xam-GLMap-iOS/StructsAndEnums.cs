using System;
using System.Runtime.InteropServices;
using Foundation;
using GLMap;
using ObjCRuntime;
using UIKit;

namespace GLMap
{
    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapImageGroupImageInfo
    {
        public int imageID;

        public int pos;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapResource
    {
        // public unsafe void *data;
        public IntPtr data;

        public uint dataSize;

        public float imageScale;
    }

    public enum GLMapInfoState : byte
    {
        Removed,
        NotDownloaded,
        Downloaded,
        NeedUpdate,
        NeedResume,
        InProgress
    }

    public enum GLMapPlacement : byte
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        Hidden
    }

    public enum GLMapVectorObjectType : byte
    {
        Unknown,
        Point,
        Line,
        Polygon
    }

    public enum GLMapError : ushort
    {
        Success = 0,
        Empty,
        Cancelled,
        InvalidTileData,
        InvalidObjectID,
        InvalidMapListData,
        NoMemory,
        FailedToOpenOutputFile,
        FailedToWrite,
        Http = 4096,
        Curl = 8192,
        Xz = 16384,
        ExpireWarning = 32768
    }

    public static partial class Functions
    {
        // extern GLMapPoint GLMapPointMakeFromGeoCoordinates (double lat, double lon);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern GLMapPoint GLMapPointMakeFromGeoCoordinates (double lat, double lon);

        // extern GLMapGeoPoint GLMapGeoPointFromMapPoint (GLMapPoint point);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern GLMapGeoPoint GLMapGeoPointFromMapPoint (GLMapPoint point);

        // extern GLMapPoint GLMapPointFromMapGeoPoint (GLMapGeoPoint point);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern GLMapPoint GLMapPointFromMapGeoPoint (GLMapGeoPoint point);

        // extern double GLMapMetersBetweenPoints (GLMapPoint a, GLMapPoint b);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern double GLMapMetersBetweenPoints (GLMapPoint a, GLMapPoint b);

    //    // extern void SendLogMessage (const char *, ...);
    //    [DllImport ("__Internal")]
    //    //[Verify (PlatformInvoke)]
    //    static extern unsafe void SendLogMessage (NSString text, IntPtr varArgs);

        // extern void GLMapMarkerSetLocation (GLMapMarkerData _Nonnull data, GLMapPoint point);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern unsafe void GLMapMarkerSetLocation (void* data, GLMapPoint point);

        // extern void GLMapMarkerSetStyle (GLMapMarkerData _Nonnull data, unsigned int style);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern unsafe void GLMapMarkerSetStyle (void* data, uint style);

        // extern void GLMapMarkerSetText (GLMapMarkerData _Nonnull data, NSString * _Nonnull text, int offset, GLMapVectorStyle * _Nonnull style);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern unsafe void GLMapMarkerSetText (IntPtr data, NSString text, int offset, GLMapVectorStyle style);

        // extern GLMapResource GLMapResourceWithData (NSData * _Nonnull data);
        [DllImport ("__Internal")]
        //[Verify (PlatformInvoke)]
        static extern GLMapResource GLMapResourceWithData (NSData data);
    }

    public enum GLUnitSystem : byte
    {
        International,
        Imperial,
        Nautical
    }

    public enum GLUnits : byte
    {
        Kilometers,
        Meters,
        Miles,
        Foots,
        NauticalMiles
    }

    public enum GLMapTileState : byte
    {
        NoData,
        Updating,
        HasData
    }

    public enum GLMapTransitionFunction : byte
    {
        Instant = 0,
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapTilePos
    {
        public int x;

        public int y;

        public int z;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapGeoPoint
    {
        public double lat;

        public double lon;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapPoint
    {
        public double x;

        public double y;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLTrackPoint
    {
        public GLMapPoint pt;

        public uint color;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct GLMapBBox
    {
        public GLMapPoint origin;

        public GLMapPoint size;
    }

    [Native]
    public enum GLMapViewGestures : long
    {
        None = 0,
        Pan = 1,
        Zoom = 1 << 1,
        Rotate = 1 << 2,
        ZoomIn = 1 << 3,
        ZoomOut = 1 << 4,
        Tap = 1 << 5,
        LongPress = 1 << 6,
        All = Pan | Zoom | Rotate | ZoomIn | ZoomOut | Tap | LongPress
    }
}
