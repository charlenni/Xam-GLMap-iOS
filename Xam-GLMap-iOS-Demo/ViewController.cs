using System;
using System.IO;
using GLMap;
using UIKit;

namespace XamGLMapiOSDemo
{
    public partial class ViewController : UIViewController
    {
        GLMapView mapView;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            GLMapManager.SharedManager().TileDownloadingAllowed = true;

            mapView = new GLMapView();

            mapView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            mapView.ShowUserLocation = true;
            mapView.BackgroundColor = UIColor.Red;

			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var cacheDir = Path.Combine(documents, "RasterCache");

			Directory.CreateDirectory(cacheDir);

			var cache = Path.Combine(cacheDir, "os.cache");

            mapView.RasterSources = new GLMapRasterTileSource[] { new OSMTileSource(cache) };

			this.View.AddSubview(mapView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLayoutSubviews()
        {
            mapView.Frame = new CoreGraphics.CGRect(10, 20, this.View.Bounds.Size.Width-20, this.View.Bounds.Size.Height-30); // size of the video frame
        }
    }
}
