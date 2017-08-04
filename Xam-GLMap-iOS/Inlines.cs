using System;
using UIKit;

namespace GLMap
{
	using GLMapColor = UInt32;
	
    public static partial class Functions
    {
		// Creates new `GLMapGeoPoint`.
		// 
		// @param lat Latitude
		// @param lon Longitude
		// @return New geo point
		public static GLMapGeoPoint GLMapGeoPointMake(double lat, double lon)
		{
			return new GLMapGeoPoint() { lat = lat, lon = lon };
		}

        // Creates new `GLMapPoint`
        //
        // @param x X coordinate
        // @param y Y coordinate
        // @return New map point
 		public static GLMapPoint GLMapPointMake(double x, double y)
		{
			return new GLMapPoint() { x = x, y = y };
		}

		// Checks equality of two map points
		// 
		// @param a First map point
		// @param b Second map point
		// @return `true` if map points is equal
		public static bool GLMapPointEqual(GLMapPoint a, GLMapPoint b)
		{
			return a.x == b.x && a.y == b.y;
		}

        // Creates new color from int chanel values 0 - 255
		//
		// @param r Red channel value
		// @param g Green channel value
		// @param b Blue channel value
		// @param a Alpha channel value
		//
		// @return Returns new color object
		public static GLMapColor GLMapColorMake(int r, int g, int b, int a)
		{
            return (((uint)a & 255) << 24) | (((uint)b & 255) << 16) | (((uint)g & 255) << 8) | ((uint)r & 255);
		}

		// Creates new color from float chanel values 0.0 - 1.0
		//
		// @param r Red channel value
		// @param g Green channel value
		// @param b Blue channel value
		// @param a Alpha channel value
		// 
		// @return Returns new color object
		public static GLMapColor GLMapColorMakeF(double r, double g, double b, double a)
		{
            return GLMapColorMake((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));
		}

        // Creates `UIColor` object from our `GLMapColor`
        // 
        // @param color Initial color
        // @return New `UIColor` object
		public static UIColor UIColorFromGLMapColor(GLMapColor color)
        {
            return new UIColor((color & 255) / 255f, (color >> 8 & 255) / 255f, (color >> 16 & 255) / 255f, (color >> 24 & 255) / 255f);
        }

        // @return Returns empty bounding box
		public static GLMapBBox GLMapBBoxEmpty()
		{
			return new GLMapBBox() { origin = GLMapPointMake(0, 0), size = GLMapPointMake(-1, -1) };
		}

		// Adds point into existing bounding box.
		// 
		// @param bbox Bounding box
		// @param point Point to add into bounding box
		public static GLMapBBox GLMapBBoxAddPoint(GLMapBBox bbox, GLMapPoint point)
		{
			if (bbox.size.x < 0 && bbox.size.y < 0)
			{
				bbox.size = GLMapPointMake(0, 0);
				bbox.origin = point;
			}
			else
			{
				if (point.x < bbox.origin.x)
				{
					bbox.size.x += bbox.origin.x - point.x;
					bbox.origin.x = point.x;
				}
				if (point.x > bbox.origin.x + bbox.size.x)
				{
					bbox.size.x = point.x - bbox.origin.x;
				}

				if (point.y < bbox.origin.y)
				{
					bbox.size.y += bbox.origin.y - point.y;
					bbox.origin.y = point.y;
				}
				if (point.y > bbox.origin.y + bbox.size.y)
				{
					bbox.size.y = point.y - bbox.origin.y;
				}
			}
			return bbox;
		}

		// Check if the bbox contains the point
		// 
		// @param bbox Bounding box
		// @param point Point to check
		// @return true if point is in bbox
		public static bool GLMapBBoxContains(GLMapBBox bbox, GLMapPoint point)
		{
			if (point.y < bbox.origin.y)
				return false;
			if (point.y > bbox.origin.y + bbox.size.y)
				return false;

			if (point.x >= bbox.origin.x && point.x <= bbox.origin.x + bbox.size.x)
				return true;

			if (point.x >= bbox.origin.x - Constants.GLMapPointMax && point.x <= bbox.origin.x + bbox.size.x - Constants.GLMapPointMax)
				return true;

			if (point.x >= bbox.origin.x + Constants.GLMapPointMax && point.x <= bbox.origin.x + bbox.size.x + Constants.GLMapPointMax)
				return true;

			return false;
		}

		// Creates new bounding box
		// 
		// @param origin Origin point
		// @param width Width
		// @param height Height
		// @return New bounding box
		public static GLMapBBox GLMapBBoxMake(GLMapPoint origin, double width, double height)
		{
			return new GLMapBBox() { origin = origin, size = GLMapPointMake(width, height)};
		}

		// Checks equality of two bounding boxes
		// 
		// @param a First bounding box
		// @param b Second bounding box
		// @return `true` if bounding boxes is equal
		public static bool GLMapBBoxEqual(GLMapBBox a, GLMapBBox b)
		{
			return GLMapPointEqual(a.origin, b.origin) && GLMapPointEqual(a.size, b.size);
		}

		// Returns center of bbox
		// 
		// @return center of bbox
		public static GLMapPoint GLMapBBoxCenter(GLMapBBox a)
		{
			return GLMapPointMake(a.origin.x + a.size.x / 2, a.origin.y + a.size.y / 2);
		}

        public static GLMapTilePos GLMapTilePosMake(int x, int y, int z)
        {
            return new GLMapTilePos() { x = x, y = y, z = z };
        }

		// Checks if it's error or success code
		public static bool GLMapIsSuccess(GLMapError v)
		{
			return v == GLMapError.Success;
		}

		// @return Returns empty resource data.
		public static GLMapResource GLMapResourceEmpty()
		{
			return new GLMapResource() { data = (IntPtr)0, dataSize = 0, imageScale = 0f };
		}

	}
}