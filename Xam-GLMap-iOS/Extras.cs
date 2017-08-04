using System;
using System.Runtime.InteropServices;
using Foundation;

namespace GLMap
{
    public partial class GLMapVectorObject
    {
        public void AddLine(GLMapPoint[] lineArray, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(lineArray))
            {

                AddLine((IntPtr)ap, pointCount);
            }
        }

        public void AddGeoLine(GLMapGeoPoint[] geoLineArray, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(geoLineArray))
            {
                AddGeoLine((IntPtr)ap, pointCount);
            }
        }

        public void AddPolygonOuterRing(GLMapPoint[] outerRing, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(outerRing))
            {
                AddPolygonOuterRing((IntPtr)ap, pointCount);
            }
        }

        public void AddPolygonInnerRing(GLMapPoint[] innerRing, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(innerRing))
            {

                AddPolygonInnerRing((IntPtr)ap, pointCount);
            }
        }

        public void AddGeoPolygonOuterRing(GLMapGeoPoint[] outerRing, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(outerRing))
            {


                AddGeoPolygonOuterRing((IntPtr)ap, pointCount);
            }
        }

        public void AddGeoPolygonInnerRing(GLMapGeoPoint[] innerRing, nint pointCount)
        {
            using (AutoPinner ap = new AutoPinner(innerRing))
            {
                AddGeoPolygonInnerRing((IntPtr)ap, pointCount);
            }
        }
    }

	// https://stackoverflow.com/questions/537573/how-to-get-intptr-from-byte-in-c-sharp
	class AutoPinner : IDisposable
	{
		GCHandle _pinnedArray;

		public AutoPinner(Object obj)
		{
			_pinnedArray = GCHandle.Alloc(obj, GCHandleType.Pinned);
		}

		public static implicit operator IntPtr(AutoPinner ap)
		{
			return ap._pinnedArray.AddrOfPinnedObject();
		}

		public void Dispose()
		{
			_pinnedArray.Free();
		}
	}
}
