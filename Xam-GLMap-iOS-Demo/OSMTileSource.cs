using System;
using System.IO;
using Foundation;
using GLMap;
using UIKit;

namespace XamGLMapiOSDemo
{
    public class OSMTileSource : GLMapRasterTileSource
	{
		string[] mirrors;

        public OSMTileSource(string cacheFile) : base(cacheFile)
        {
            mirrors = new string[3];
            mirrors[0] = @"https://a.tile.openstreetmap.org/{0}/{1}/{2}.png";
            mirrors[1] = @"https://b.tile.openstreetmap.org/{0}/{1}/{2}.png";
            mirrors[2] = @"https://c.tile.openstreetmap.org/{0}/{1}/{2}.png";

            //Set as valid zooms all levels from 0 to 19
            this.ValidZoomMask = (1 << 20) - 1;

            //For retina devices we can make tile size a bit smaller.
            if (UIScreen.MainScreen.Scale >= 2)
            {
                this.TileSize = 192;
            }

            AttributionText = "© OpenStreetMap contributors";
		}

        public override NSUrl UrlForTilePos(GLMapTilePos pos)
		{
			string mirror = mirrors[new Random().Next(2)];
			string rv = string.Format(mirror, pos.z, pos.x, pos.y);
			
			return new NSUrl(rv);
		}
	}
}
