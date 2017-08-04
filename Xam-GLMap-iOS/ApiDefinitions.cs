using System;
using CoreGraphics;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace GLMap
{
    // Delegate for blocks
    delegate void NSDispatchHandlerT();

	// @interface GLMapDownloadTask : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapDownloadTask
	{
		// @property (readonly) NSError * _Nullable error;
		[NullAllowed, Export("error")]
		NSError Error { get; }

		// @property (readonly) BOOL isCancelled;
		[Export("isCancelled")]
		bool IsCancelled { get; }

		// @property (readonly) GLMapInfo * _Nonnull map;
		[Export("map")]
		GLMapInfo Map { get; }

		// -(void)cancel;
		[Export("cancel")]
		void Cancel();
	}

	// @interface GLMapImage : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapImage
	{
		// @property (assign) BOOL rotatesWithMap;
		[Export("rotatesWithMap")]
		bool RotatesWithMap { get; set; }

		// @property (assign) BOOL hidden;
		[Export("hidden")]
		bool Hidden { get; set; }

		// @property (assign) int position;
		[Export("position")]
		int Position { get; set; }

		// @property (assign) int offset;
		[Export("offset")]
		int Offset { get; set; }

		// @property (assign) float angle;
		[Export("angle")]
		float Angle { get; set; }

		// @property (readonly) int size;
		[Export("size")]
		int Size { get; }

		// -(void)setImage:(id)image completion:(dispatch_block_t)completion;
		[Export("setImage:completion:")]
		void SetImage(NSObject image, NSDispatchHandlerT completion);

		// -(void)setText:(NSString *)text withStyle:(GLMapVectorStyle *)style completion:(dispatch_block_t)completion;
		[Export("setText:withStyle:completion:")]
		void SetText(string text, GLMapVectorStyle style, NSDispatchHandlerT completion);
	}

	// typedef GLMapImageGroupImageInfo (^GLMapImageGroupFillBlock)(size_t);
	// delegate GLMapImageGroupImageInfo GLMapImageGroupFillBlock(nuint arg0);
    // TODO: Change return type
	delegate IntPtr GLMapImageGroupFillBlock(nuint arg0);

	// typedef void (^GLMapImageGroupIsUpdatingBlock)(BOOL);
	delegate void GLMapImageGroupIsUpdatingBlock(bool arg0);

	// @interface GLMapImageGroup : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapImageGroup
	{
		// @property (assign) _Bool hidden;
		[Export("hidden")]
		bool Hidden { get; set; }

		// -(NSArray<NSNumber *> * _Nonnull)setImages:(NSArray * _Nonnull)images completion:(dispatch_block_t _Nullable)completion;
		[Export("setImages:completion:")]
		//[Verify(StronglyTypedNSArray)]
		NSNumber[] SetImages(NSObject[] images, [NullAllowed] NSDispatchHandlerT completion);

		// -(void)setImageOffset:(id)offset forImageWithID:(int)imageID;
		[Export("setImageOffset:forImageWithID:")]
		void SetImageOffset(NSObject offset, int imageID);

		// -(void)setNeedsUpdate;
		[Export("setNeedsUpdate")]
		void SetNeedsUpdate();

		// -(void)setObjectCount:(size_t)count;
		[Export("setObjectCount:")]
		void SetObjectCount(nuint count);

		// -(void)setObjectFillBlock:(GLMapImageGroupFillBlock _Nonnull)fillBlock;
		[Export("setObjectFillBlock:")]
		void SetObjectFillBlock(GLMapImageGroupFillBlock fillBlock);

		// -(void)setIsUpdatingBlock:(GLMapImageGroupIsUpdatingBlock _Nonnull)isUpdatingBlock;
		[Export("setIsUpdatingBlock:")]
		void SetIsUpdatingBlock(GLMapImageGroupIsUpdatingBlock isUpdatingBlock);
	}

	// @interface GLMapInfo : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapInfo
	{
		// @property (readonly) GLMapInfoState state;
		[Export("state")]
		GLMapInfoState State { get; }

		// @property (readonly) NSDictionary * _Nullable names;
		[NullAllowed, Export("names")]
		NSDictionary Names { get; }

		// @property (readonly) int64_t mapID;
		[Export("mapID")]
		long MapID { get; }

		// @property (readonly) NSTimeInterval serverTimestamp;
		[Export("serverTimestamp")]
		double ServerTimestamp { get; }

		// @property (readonly) GLMapGeoPoint location;
		[Export("location")]
		GLMapGeoPoint Location { get; }

		// @property (readonly) NSArray<GLMapInfo *> * _Nonnull subMaps;
		[Export("subMaps")]
		GLMapInfo[] SubMaps { get; }

		// @property (readonly) NSString * _Nullable path;
		[NullAllowed, Export("path")]
		string Path { get; }

		// @property (readonly) int64_t sizeOnDisk;
		[Export("sizeOnDisk")]
		long SizeOnDisk { get; }

		// @property (readonly) int64_t sizeOnServer;
		[Export("sizeOnServer")]
		long SizeOnServer { get; }

		// @property (readonly) NSTimeInterval timestamp;
		[Export("timestamp")]
		double Timestamp { get; }

		// @property (readonly) float downloadProgress;
		[Export("downloadProgress")]
		float DownloadProgress { get; }

		// +(NSString * _Nonnull)storagePath;
		// +(void)setStoragePath:(NSString * _Nonnull)storagePath;
		[Static]
		[Export("storagePath")]
		string StoragePath { get; set; }

		// -(NSString * _Nullable)nameInLanguage:(NSString * _Nonnull)language;
		[Export("nameInLanguage:")]
		[return: NullAllowed]
		string NameInLanguage(string language);

		// -(NSString * _Nonnull)name;
		[Export("name")]
		string Name { get; }

		// -(NSString * _Nullable)localizedName:(GLMapLocaleSettings * _Nonnull)settings;
		[Export("localizedName:")]
		[return: NullAllowed]
		string LocalizedName(GLMapLocaleSettings settings);

		// -(double)distanceFrom:(GLMapGeoPoint)location;
		[Export("distanceFrom:")]
		double DistanceFrom(GLMapGeoPoint location);

		// -(double)distanceFromBorder:(GLMapPoint)location;
		[Export("distanceFromBorder:")]
		double DistanceFromBorder(GLMapPoint location);
	}

	// @interface GLMapLocaleSettings : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapLocaleSettings
	{
		// +(GLMapLocaleSettings * _Nonnull)systemSettings;
		[Static]
		[Export("systemSettings")]
		GLMapLocaleSettings SystemSettings { get; }

		// +(NSSet<NSString *> * _Nonnull)supportedLocales;
		[Static]
		[Export("supportedLocales")]
		NSSet<NSString> SupportedLocales { get; }

		// +(BOOL)isLocaleSupported:(NSString * _Nonnull)locale;
		[Static]
		[Export("isLocaleSupported:")]
		bool IsLocaleSupported(string locale);

		// -(instancetype _Nonnull)initWithLocalesOrder:(NSArray<NSString *> * _Nullable)localesOrder;
		[Export("initWithLocalesOrder:")]
		IntPtr Constructor([NullAllowed] string[] localesOrder);
	}

	// typedef void (^GLMapDownloadCompletionBlock)(GLMapDownloadTask * _Nonnull);
	delegate void GLMapDownloadCompletionBlock(GLMapDownloadTask arg0);

	// typedef void (^GLMapListUpdateBlock)(NSArray<GLMapInfo *> * _Nullable, BOOL, NSError * _Nullable);
	delegate void GLMapListUpdateBlock([NullAllowed] GLMapInfo[] arg0, bool arg1, [NullAllowed] NSError arg2);

	// @interface GLMapManager : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapManager
	{
		// @property (assign) BOOL tileDownloadingAllowed;
		[Export("tileDownloadingAllowed")]
		bool TileDownloadingAllowed { get; set; }

		// @property (assign) int64_t tileRefreshTimeInterval;
		[Export("tileRefreshTimeInterval")]
		long TileRefreshTimeInterval { get; set; }

		// @property (readonly) uint64_t downloadedMapsCount;
		[Export("downloadedMapsCount")]
		ulong DownloadedMapsCount { get; }

		// @property (strong) NSString * _Nullable apiKey;
		[NullAllowed, Export("apiKey", ArgumentSemantic.Strong)]
		string ApiKey { get; set; }

		// +(instancetype _Nonnull)sharedManager;
		[Static]
		[Export("sharedManager")]
		GLMapManager SharedManager();

		// -(NSArray<GLMapInfo *> * _Nullable)cachedMapList;
		[NullAllowed, Export("cachedMapList")]
		GLMapInfo[] CachedMapList { get; }

		// -(NSDictionary<NSNumber *,GLMapInfo *> * _Nullable)cachedMaps;
		[NullAllowed, Export("cachedMaps")]
		//NSDictionary<NSNumber, GLMapInfo> CachedMaps { get; }
		NSDictionary<NSNumber, NSObject> CachedMaps { get; }

		// -(void)updateMapListWithCompletionBlock:(GLMapListUpdateBlock _Nullable)block;
		[Export("updateMapListWithCompletionBlock:")]
		void UpdateMapListWithCompletionBlock([NullAllowed] GLMapListUpdateBlock block);

		// -(GLMapDownloadTask * _Nullable)downloadMap:(GLMapInfo * _Nonnull)map withCompletionBlock:(GLMapDownloadCompletionBlock _Nullable)block;
		[Export("downloadMap:withCompletionBlock:")]
		[return: NullAllowed]
		GLMapDownloadTask DownloadMap(GLMapInfo map, [NullAllowed] GLMapDownloadCompletionBlock block);

		// -(GLMapDownloadTask * _Nullable)downloadTaskForMap:(GLMapInfo * _Nonnull)map;
		[Export("downloadTaskForMap:")]
		[return: NullAllowed]
		GLMapDownloadTask DownloadTaskForMap(GLMapInfo map);

		// -(NSArray<GLMapDownloadTask *> * _Nullable)allDownloadTasks;
		[NullAllowed, Export("allDownloadTasks")]
		GLMapDownloadTask[] AllDownloadTasks { get; }

		// -(void)addMap:(NSString * _Nonnull)path;
		[Export("addMap:")]
		void AddMap(string path);

		// -(void)removeMap:(NSString * _Nonnull)path;
		[Export("removeMap:")]
		void RemoveMap(string path);

		// -(GLMapInfo * _Nullable)downloadedMapAtPoint:(GLMapPoint)point;
		[Export("downloadedMapAtPoint:")]
		[return: NullAllowed]
		GLMapInfo DownloadedMapAtPoint(GLMapPoint point);

		// -(GLMapInfo * _Nullable)mapAtPoint:(GLMapPoint)point;
		[Export("mapAtPoint:")]
		[return: NullAllowed]
		GLMapInfo MapAtPoint(GLMapPoint point);

		// -(void)deleteMap:(GLMapInfo * _Nonnull)map;
		[Export("deleteMap:")]
		void DeleteMap(GLMapInfo map);

		// -(void)clearCaches;
		[Export("clearCaches")]
		void ClearCaches();

		// -(void)saveDownloadsState;
		[Export("saveDownloadsState")]
		void SaveDownloadsState();
	}

	// @interface GLMapMarkerLayer : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapMarkerLayer
	{
		// -(instancetype _Nonnull)initWithMarkers:(NSArray<id> * _Nonnull)markers andStyles:(GLMapMarkerStyleCollection * _Nonnull)styleCollection;
		[Export("initWithMarkers:andStyles:")]
		IntPtr Constructor(NSObject[] markers, GLMapMarkerStyleCollection styleCollection);

		// -(instancetype _Nonnull)initWithVectorObjects:(GLMapVectorObjectArray * _Nonnull)vectorObjects andStyles:(GLMapMarkerStyleCollection * _Nonnull)styleCollection;
		[Export("initWithVectorObjects:andStyles:")]
		IntPtr Constructor(GLMapVectorObjectArray vectorObjects, GLMapMarkerStyleCollection styleCollection);

		// -(instancetype _Nonnull)initWithVectorObjects:(GLMapVectorObjectArray * _Nonnull)objects cascadeStyle:(GLMapVectorCascadeStyle * _Nonnull)cascadeStyle styleCollection:(GLMapMarkerStyleCollection * _Nonnull)styleCollection;
		[Export("initWithVectorObjects:cascadeStyle:styleCollection:")]
		IntPtr Constructor(GLMapVectorObjectArray objects, GLMapVectorCascadeStyle cascadeStyle, GLMapMarkerStyleCollection styleCollection);

		// -(void)add:(NSArray<id> * _Nullable)markersToAdd remove:(NSArray<id> * _Nullable)markersToRemove reload:(NSArray<id> * _Nullable)markersToReload animated:(BOOL)animated completion:(dispatch_block_t _Nullable)completion;
		[Export("add:remove:reload:animated:completion:")]
		void Add([NullAllowed] NSObject[] markersToAdd, [NullAllowed] NSObject[] markersToRemove, [NullAllowed] NSObject[] markersToReload, bool animated, [NullAllowed] NSDispatchHandlerT completion);

		// -(void)changeStyle:(GLMapMarkerStyleCollection * _Nonnull)style completion:(dispatch_block_t _Nullable)completion;
		[Export("changeStyle:completion:")]
		void ChangeStyle(GLMapMarkerStyleCollection style, [NullAllowed] NSDispatchHandlerT completion);

		// -(NSArray<id> * _Nullable)objectsAtMapView:(GLMapView * _Nonnull)mapView nearPoint:(GLMapPoint)point distance:(double)distanceInPoints;
		[Export("objectsAtMapView:nearPoint:distance:")]
		[return: NullAllowed]
		NSObject[] ObjectsAtMapView(GLMapView mapView, GLMapPoint point, double distanceInPoints);

		// @property (assign) BOOL clusteringEnabled;
		[Export("clusteringEnabled")]
		bool ClusteringEnabled { get; set; }

		// @property (assign) int drawOrder;
		[Export("drawOrder")]
		int DrawOrder { get; set; }

		// @property (assign) double animationDuration;
		[Export("animationDuration")]
		double AnimationDuration { get; set; }
	}

	// @interface GLMapVectorStyle : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapVectorStyle
	{
		// +(instancetype _Nullable)createStyle:(NSString * _Nonnull)style;
		[Static]
		[Export("createStyle:")]
		[return: NullAllowed]
		GLMapVectorStyle CreateStyle(string style);

		// +(instancetype _Nullable)createStyle:(NSString * _Nonnull)style error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("createStyle:error:")]
		[return: NullAllowed]
		GLMapVectorStyle CreateStyle(string style, [NullAllowed] out NSError error);
	}

	// typedef void (^GLMapMarkerDataFillBlock)(NSObject * _Nonnull, GLMapMarkerData _Nonnull);
	// unsafe delegate void GLMapMarkerDataFillBlock(NSObject arg0, void* arg1);
	unsafe delegate void GLMapMarkerDataFillBlock(NSObject arg0, IntPtr arg1);

	// typedef void (^GLMapMarkerUnionFillBlock)(uint32_t, GLMapMarkerData _Nonnull);
	// unsafe delegate void GLMapMarkerUnionFillBlock(uint arg0, void* arg1);
	unsafe delegate void GLMapMarkerUnionFillBlock(uint arg0, IntPtr arg1);

	// @interface GLMapMarkerStyleCollection : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapMarkerStyleCollection
	{
		// -(void)setMarkerDataFillBlock:(GLMapMarkerDataFillBlock _Nullable)block;
		[Export("setMarkerDataFillBlock:")]
		void SetMarkerDataFillBlock([NullAllowed] GLMapMarkerDataFillBlock block);

		// -(void)setMarkerUnionFillBlock:(GLMapMarkerUnionFillBlock _Nullable)block;
		[Export("setMarkerUnionFillBlock:")]
		void SetMarkerUnionFillBlock([NullAllowed] GLMapMarkerUnionFillBlock block);

		// -(uint32_t)addMarkerImage:(id)image;
		[Export("addMarkerImage:")]
		uint AddMarkerImage(NSObject image);

		// -(uint32_t)addMarkerImages:(NSArray * _Nonnull)images;
		[Export("addMarkerImages:")]
		//[Verify(StronglyTypedNSArray)]
		uint AddMarkerImages(NSObject[] images);

		// -(uint32_t)addMarkerImages:(NSArray * _Nonnull)images offsets:(NSArray<NSValue *> * _Nullable)offsets;
		[Export("addMarkerImages:offsets:")]
		//[Verify(StronglyTypedNSArray)]
		uint AddMarkerImages(NSObject[] images, [NullAllowed] NSValue[] offsets);

		// -(void)setStyleName:(NSString * _Nonnull)name forStyleIndex:(uint32_t)styleIndex;
		[Export("setStyleName:forStyleIndex:")]
		void SetStyleName(string name, uint styleIndex);
	}

	// @interface GLMapRasterTileSource : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapRasterTileSource
	{
		// -(instancetype _Nullable)initWithCachePath:(NSString * _Nullable)cachePath;
		[Export("initWithCachePath:")]
		IntPtr Constructor([NullAllowed] string cachePath);

		// -(NSURL * _Nullable)urlForTilePos:(GLMapTilePos)pos;
		[Export("urlForTilePos:")]
		[return: NullAllowed]
		NSUrl UrlForTilePos(GLMapTilePos pos);

		// -(id)imageForTilePos:(GLMapTilePos)pos expired:(BOOL * _Nonnull)expired;
		[Export("imageForTilePos:expired:")]
		unsafe NSObject ImageForTilePos(GLMapTilePos pos, ref bool expired);

		// -(void)saveTileData:(NSData * _Nonnull)data forTilePos:(GLMapTilePos)pos;
		[Export("saveTileData:forTilePos:")]
		void SaveTileData(NSData data, GLMapTilePos pos);

		// @property (assign) int64_t tileRefreshTimeInterval;
		[Export("tileRefreshTimeInterval")]
		long TileRefreshTimeInterval { get; set; }

		// @property (assign) uint32_t validZoomMask;
		[Export("validZoomMask")]
		uint ValidZoomMask { get; set; }

		// @property (assign) uint32_t tileSize;
		[Export("tileSize")]
		uint TileSize { get; set; }

		// @property (strong) NSString * _Nullable attributionText;
		[NullAllowed, Export("attributionText", ArgumentSemantic.Strong)]
		string AttributionText { get; set; }

		// @property (readonly) uint64_t cacheSize;
		[Export("cacheSize")]
		ulong CacheSize { get; }

		// -(void)dropCache;
		[Export("dropCache")]
		void DropCache();
	}

	// @interface GLMapTrack : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapTrack
	{
		// -(void)setWidth:(id)width;
		[Export("setWidth:")]
		void SetWidth(NSObject width);

		// -(void)setTrackData:(GLMapTrackData * _Nullable)data;
		[Export("setTrackData:")]
		void SetTrackData([NullAllowed] GLMapTrackData data);

		// -(uint32_t)drawOrder;
		[Export("drawOrder")]
		//[Verify(MethodToProperty)]
		uint DrawOrder { get; }

		// -(BOOL)findNearestPoint:(GLMapPoint * _Nonnull)nearestPoint toPoint:(GLMapPoint)point maxDistance:(id)distance;
		[Export("findNearestPoint:toPoint:maxDistance:")]
		unsafe bool FindNearestPoint(ref GLMapPoint nearestPoint, GLMapPoint point, NSObject distance);
	}

	// typedef void (^GLMapTrackPointsCallback)(NSUInteger, GLTrackPoint * _Nonnull);
	unsafe delegate void GLMapTrackPointsCallback(nuint arg0, ref GLTrackPoint arg1);

	// @interface GLMapTrackData : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapTrackData
	{
		// -(instancetype _Nullable)initWithPoints:(const GLTrackPoint * _Nonnull)points count:(NSUInteger)count;
		[Export("initWithPoints:count:")]
		// unsafe IntPtr Constructor(GLTrackPoint[] points, nuint count);
		unsafe IntPtr Constructor(IntPtr points, nuint count);

		// -(instancetype _Nullable)initWithPointsCallback:(GLMapTrackPointsCallback _Nonnull)pointsCallback count:(NSUInteger)count;
		[Export("initWithPointsCallback:count:")]
		IntPtr Constructor(GLMapTrackPointsCallback pointsCallback, nuint count);

		// -(instancetype _Nullable)initWithData:(GLMapTrackData * _Nonnull)data mergeSegments:(BOOL)mergeSegments;
		[Export("initWithData:mergeSegments:")]
		IntPtr Constructor(GLMapTrackData data, bool mergeSegments);

		// -(instancetype _Nullable)initWithData:(GLMapTrackData * _Nonnull)data andNewPoint:(GLTrackPoint * _Nonnull)pt startNewSegment:(BOOL)startNewSegment;
		[Export("initWithData:andNewPoint:startNewSegment:")]
		unsafe IntPtr Constructor(GLMapTrackData data, GLTrackPoint pt, bool startNewSegment);

		// -(instancetype _Nullable)trackDataWithMergedSegments;
		[Export("trackDataWithMergedSegments")]
		[return: NullAllowed]
		GLMapTrackData TrackDataWithMergedSegments();

		// -(GLMapBBox)bbox;
		[Export("bbox")]
		GLMapBBox Bbox { get; }

		// -(BOOL)isEmpty;
		[Export("isEmpty")]
		bool IsEmpty { get; }

		// -(size_t)segmentCount;
		[Export("segmentCount")]
		nuint SegmentCount { get; }
	}

	// @interface GLMapVectorCascadeStyle : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapVectorCascadeStyle
	{
		// +(instancetype _Nullable)createStyle:(NSString * _Nonnull)style;
		[Static]
		[Export("createStyle:")]
		[return: NullAllowed]
		GLMapVectorCascadeStyle CreateStyle(string style);

		// +(instancetype _Nullable)createStyle:(NSString * _Nonnull)style error:(NSError * _Nullable * _Nullable)error;
		[Static]
		[Export("createStyle:error:")]
		[return: NullAllowed]
		GLMapVectorCascadeStyle CreateStyle(string style, [NullAllowed] out NSError error);
	}

	// @interface GLMapVectorImageFactory : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapVectorImageFactory
	{
		// @property (assign) BOOL disableCaching;
		[Export("disableCaching")]
		bool DisableCaching { get; set; }

		// +(GLMapVectorImageFactory * _Nonnull)sharedFactory;
		[Static]
		[Export("sharedFactory")]
		GLMapVectorImageFactory SharedFactory { get; }

		// -(void)clearCaches;
		[Export("clearCaches")]
		void ClearCaches();

		// -(id)imageFromSvgpb:(NSString * _Nonnull)imagePath;
		[Export("imageFromSvgpb:")]
		NSObject ImageFromSvgpb(string imagePath);

		// -(id)imageFromSvgpb:(NSString * _Nonnull)imagePath withScale:(double)scale;
		[Export("imageFromSvgpb:withScale:")]
		NSObject ImageFromSvgpb(string imagePath, double scale);

		// -(id)imageFromSvgpb:(NSString * _Nonnull)imagePath withScale:(double)scale andTintColor:(uint32_t)tintColor;
		[Export("imageFromSvgpb:withScale:andTintColor:")]
		NSObject ImageFromSvgpb(string imagePath, double scale, uint tintColor);

		// -(id)imageFromSvgpb:(NSString * _Nonnull)imagePath withSize:(id)size andTintColor:(uint32_t)tintColor;
		[Export("imageFromSvgpb:withSize:andTintColor:")]
		NSObject ImageFromSvgpb(string imagePath, NSObject size, uint tintColor);

		// -(id)imageFromSvgpb:(NSString * _Nonnull)imagePath withSize:(id)size contentMode:(id)contentMode andTintColor:(uint32_t)tintColor;
		[Export("imageFromSvgpb:withSize:contentMode:andTintColor:")]
		NSObject ImageFromSvgpb(string imagePath, NSObject size, NSObject contentMode, uint tintColor);
	}

	// typedef BOOL (^GLMapVectorObjectBlock)(GLMapVectorObject * _Nonnull);
	delegate bool GLMapVectorObjectBlock(GLMapVectorObject arg0);

	// @interface GLMapVectorObject : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapVectorObject
	{
		// @property (readonly) GLMapPoint point;
		[Export("point")]
		GLMapPoint Point { get; }

		// @property (readonly) NSArray<NSData *> * _Nullable multilineData;
		[NullAllowed, Export("multilineData")]
		NSData[] MultilineData { get; }

		// @property (readonly) GLMapBBox bbox;
		[Export("bbox")]
		GLMapBBox Bbox { get; }

		// @property (readonly) GLMapVectorObjectType type;
		[Export("type")]
		GLMapVectorObjectType ObjectType { get; }

		// @property (readonly) NSDictionary * _Nonnull properties;
		[Export("properties")]
		NSDictionary Properties { get; }

		// +(GLMapVectorObjectArray * _Nullable)createVectorObjectsFromGeoJSONData:(NSData * _Nonnull)geoJSONData;
		[Static]
		[Export("createVectorObjectsFromGeoJSONData:")]
		[return: NullAllowed]
		GLMapVectorObjectArray CreateVectorObjectsFromGeoJSONData(NSData geoJSONData);

		// +(GLMapVectorObjectArray * _Nullable)createVectorObjectsFromGeoJSON:(NSString * _Nonnull)geoJSON;
		[Static]
		[Export("createVectorObjectsFromGeoJSON:")]
		[return: NullAllowed]
		GLMapVectorObjectArray CreateVectorObjectsFromGeoJSON(string geoJSON);

		// +(GLMapVectorObjectArray * _Nullable)createVectorObjectsFromFile:(NSString * _Nonnull)filePath;
		[Static]
		[Export("createVectorObjectsFromFile:")]
		[return: NullAllowed]
		GLMapVectorObjectArray CreateVectorObjectsFromFile(string filePath);

		// +(BOOL)enumVectorObjectsFromFile:(NSString * _Nonnull)filePath objectBlock:(GLMapVectorObjectBlock _Nonnull)objectBlock;
		[Static]
		[Export("enumVectorObjectsFromFile:objectBlock:")]
		bool EnumVectorObjectsFromFile(string filePath, GLMapVectorObjectBlock objectBlock);

		// -(void)loadPoint:(GLMapPoint)point;
		[Export("loadPoint:")]
		void LoadPoint(GLMapPoint point);

		// -(void)loadMultiLine:(NSArray<NSData *> * _Nonnull)multiLineData;
		[Export("loadMultiLine:")]
		void LoadMultiLine(NSData[] multiLineData);

		// -(void)loadGeoMultiLine:(NSArray<NSData *> * _Nonnull)multiLineData;
		[Export("loadGeoMultiLine:")]
		void LoadGeoMultiLine(NSData[] multiLineData);

		// -(void)addLine:(const GLMapPoint * _Nonnull)lineArray pointCount:(NSInteger)pointCount;
		[Export("addLine:pointCount:"), Internal]
		// unsafe void AddLine(GLMapPoint[] lineArray, nint pointCount);
		void AddLine(IntPtr lineArray, nint pointCount);

		// -(void)addGeoLine:(const GLMapGeoPoint * _Nonnull)geoLineArray pointCount:(NSInteger)pointCount;
		[Export("addGeoLine:pointCount:"),Internal]
		// unsafe void AddGeoLine(GLMapGeoPoint[] geoLineArray, nint pointCount);
        void AddGeoLine(IntPtr geoLineArray, nint pointCount);

		// -(void)loadPolygon:(NSArray<NSArray<NSData *> *> * _Nonnull)polygonData;
		[Export("loadPolygon:")]
		void LoadPolygon(NSArray<NSData>[] polygonData);

		// -(void)loadGeoPolygon:(NSArray<NSArray<NSData *> *> * _Nonnull)polygonData;
		[Export("loadGeoPolygon:")]
		void LoadGeoPolygon(NSArray<NSData>[] polygonData);

		// -(void)addPolygonOuterRing:(const GLMapPoint * _Nonnull)outerRing pointCount:(NSInteger)pointCount;
		[Export("addPolygonOuterRing:pointCount:"), Internal]
		// unsafe void AddPolygonOuterRing(GLMapPoint[] outerRing, nint pointCount);
		void AddPolygonOuterRing(IntPtr outerRing, nint pointCount);

		// -(void)addPolygonInnerRing:(const GLMapPoint * _Nonnull)innerRing pointCount:(NSInteger)pointCount;
		[Export("addPolygonInnerRing:pointCount:"), Internal]
		// unsafe void AddPolygonInnerRing(GLMapPoint[] innerRing, nint pointCount);
		void AddPolygonInnerRing(IntPtr innerRing, nint pointCount);

		// -(void)addGeoPolygonOuterRing:(const GLMapGeoPoint * _Nonnull)outerRing pointCount:(NSInteger)pointCount;
		[Export("addGeoPolygonOuterRing:pointCount:")]
		// unsafe void AddGeoPolygonOuterRing(GLMapGeoPoint[] outerRing, nint pointCount);
		void AddGeoPolygonOuterRing(IntPtr outerRing, nint pointCount);

		// -(void)addGeoPolygonInnerRing:(const GLMapGeoPoint * _Nonnull)innerRing pointCount:(NSInteger)pointCount;
		[Export("addGeoPolygonInnerRing:pointCount:")]
		// unsafe void AddGeoPolygonInnerRing(GLMapGeoPoint[] innerRing, nint pointCount);
		void AddGeoPolygonInnerRing(IntPtr innerRing, nint pointCount);

		// -(void)setGeoLineStart:(GLMapPoint)start end:(GLMapPoint)end quality:(double)quality;
		[Export("setGeoLineStart:end:quality:")]
		void SetGeoLineStart(GLMapPoint start, GLMapPoint end, double quality);

		// -(void)setValue:(NSString * _Nullable)value forKey:(NSString * _Nonnull)key;
		[Export("setValue:forKey:")]
		void SetValue([NullAllowed] string value, string key);

		// -(NSString * _Nullable)valueForKey:(NSString * _Nonnull)key;
		[Export("valueForKey:")]
		[return: NullAllowed]
		string ValueForKey(string key);

		// -(NSAttributedString * _Nullable)attributedName:(NSDictionary * _Nonnull)normalAttributes highlight:(NSDictionary * _Nonnull)highlightAttributes localeSettings:(GLMapLocaleSettings * _Nonnull)localeSettings;
		[Export("attributedName:highlight:localeSettings:")]
		[return: NullAllowed]
		NSAttributedString AttributedName(NSDictionary normalAttributes, NSDictionary highlightAttributes, GLMapLocaleSettings localeSettings);

		// -(NSString * _Nullable)nameWithLocaleSettings:(GLMapLocaleSettings * _Nonnull)localeSettings;
		[Export("nameWithLocaleSettings:")]
		[return: NullAllowed]
		string NameWithLocaleSettings(GLMapLocaleSettings localeSettings);

		// -(GLSearchCategory * _Nullable)searchCategory:(GLSearchCategories * _Nonnull)categories;
		[Export("searchCategory:")]
		[return: NullAllowed]
		GLSearchCategory SearchCategory(GLSearchCategories categories);

		// -(void)useTransform;
		[Export("useTransform")]
		void UseTransform();

		// -(void)setOffset:(GLMapPoint)offset andScale:(id)scale atMap:(GLMapView * _Nonnull)mapView;
		[Export("setOffset:andScale:atMap:")]
		void SetOffset(GLMapPoint offset, NSObject scale, GLMapView mapView);

		// -(BOOL)findNearestPoint:(GLMapPoint * _Nonnull)nearestPoint toPoint:(GLMapPoint)point maxDistance:(id)distance;
		[Export("findNearestPoint:toPoint:maxDistance:")]
		unsafe bool FindNearestPoint(ref GLMapPoint nearestPoint, GLMapPoint point, NSObject distance);
	}

	// @interface GLMapVectorObjectArray : NSObject
	[BaseType(typeof(NSObject))]
	interface GLMapVectorObjectArray
	{
		// -(GLMapVectorObject * _Nonnull)objectAtIndex:(NSUInteger)index;
		[Export("objectAtIndex:")]
		GLMapVectorObject ObjectAtIndex(nuint index);

		// -(GLMapVectorObject * _Nonnull)objectAtIndexedSubscript:(NSUInteger)index;
		[Export("objectAtIndexedSubscript:")]
		GLMapVectorObject ObjectAtIndexedSubscript(nuint index);

		// @property (readonly) NSUInteger count;
		[Export("count")]
		nuint Count { get; }

		// @property (readonly) GLMapBBox bbox;
		[Export("bbox")]
		GLMapBBox Bbox { get; }
	}

	// typedef void (^GLMapTapGestureBlock)(CGPoint);
	delegate void GLMapTapGestureBlock(CGPoint arg0);

	// typedef void (^GLMapBBoxChangedBlock)(GLMapBBox);
	delegate void GLMapBBoxChangedBlock(GLMapBBox arg0);

	// typedef BOOL (^GLMapAnimationBlock)(double);
	delegate bool GLMapAnimationBlock(double arg0);

	// typedef void (^GLMapVisibleTilesChangedBlock)(NSSet * _Nonnull);
	delegate void GLMapVisibleTilesChangedBlock(NSSet arg0);

	// typedef UIImage * _Nullable (^GLMapTileErrorBlock)(GLMapTilePos, NSError * _Nonnull);
	delegate UIImage GLMapTileErrorBlock(GLMapTilePos arg0, NSError arg1);

	// typedef void (^GLMapCaptureFrameBlock)(UIImage * _Nullable);
	delegate void GLMapCaptureFrameBlock([NullAllowed] UIImage arg0);

	// typedef NSString * _Nullable (^GLMapScaleRulerTextFormatterBlock)(int, GLUnits);
	delegate string GLMapScaleRulerTextFormatterBlock(int arg0, GLUnits arg1);

	// typedef GLMapResource (^GLMapResourceBlock)(NSString * _Nonnull);
	// delegate GLMapResource GLMapResourceBlock(string arg0);
	// TODO: Change return type
	delegate IntPtr GLMapResourceBlock(string arg0);

	// @interface GLMapView : UIView <CLLocationManagerDelegate>
	[BaseType(typeof(UIView))]
	interface GLMapView : ICLLocationManagerDelegate
	{
		// -(UIImage * _Nullable)captureFrame;
		[NullAllowed, Export("captureFrame")]
		UIImage CaptureFrame { get; }

		// -(void)captureFrameWhenFinish:(GLMapCaptureFrameBlock _Nonnull)block;
		[Export("captureFrameWhenFinish:")]
		void CaptureFrameWhenFinish(GLMapCaptureFrameBlock block);

		// @property BOOL offscreen;
		[Export("offscreen")]
		bool Offscreen { get; set; }

		// @property NSInteger frameInterval;
		[Export("frameInterval")]
		nint FrameInterval { get; set; }

		// @property (readonly) GLMapTileState centerTileState;
		[Export("centerTileState")]
		GLMapTileState CenterTileState { get; }

		// @property (assign, nonatomic) GLMapViewGestures gestureMask;
		[Export("gestureMask", ArgumentSemantic.Assign)]
		GLMapViewGestures GestureMask { get; set; }

		// @property (copy) GLMapTapGestureBlock _Nullable tapGestureBlock;
		[NullAllowed, Export("tapGestureBlock", ArgumentSemantic.Copy)]
		GLMapTapGestureBlock TapGestureBlock { get; set; }

		// @property (copy) GLMapTapGestureBlock _Nullable longPressGestureBlock;
		[NullAllowed, Export("longPressGestureBlock", ArgumentSemantic.Copy)]
		GLMapTapGestureBlock LongPressGestureBlock { get; set; }

		// @property (copy) GLMapBBoxChangedBlock _Nullable bboxChangedBlock;
		[NullAllowed, Export("bboxChangedBlock", ArgumentSemantic.Copy)]
		GLMapBBoxChangedBlock BboxChangedBlock { get; set; }

		// @property (copy) GLMapBBoxChangedBlock _Nullable mapDidMoveBlock;
		[NullAllowed, Export("mapDidMoveBlock", ArgumentSemantic.Copy)]
		GLMapBBoxChangedBlock MapDidMoveBlock { get; set; }

		// @property (copy) GLMapVisibleTilesChangedBlock _Nullable visibleTilesChangedBlock;
		[NullAllowed, Export("visibleTilesChangedBlock", ArgumentSemantic.Copy)]
		GLMapVisibleTilesChangedBlock VisibleTilesChangedBlock { get; set; }

		// @property (copy) GLMapTileErrorBlock _Nullable tileErrorBlock;
		[NullAllowed, Export("tileErrorBlock", ArgumentSemantic.Copy)]
		GLMapTileErrorBlock TileErrorBlock { get; set; }

		// @property (copy) dispatch_block_t _Nonnull centerTileStateChangedBlock;
		[Export("centerTileStateChangedBlock", ArgumentSemantic.Copy)]
		NSDispatchHandlerT CenterTileStateChangedBlock { get; set; }

		// @property (readonly) GLMapBBox bbox;
		[Export("bbox")]
		GLMapBBox Bbox { get; }

		// -(void)setAttributionPosition:(GLMapPlacement)position;
		[Export("setAttributionPosition:")]
		void SetAttributionPosition(GLMapPlacement position);

		// -(void)setScaleRulerBottomText:(NSString * _Nullable)text;
		[Export("setScaleRulerBottomText:")]
		void SetScaleRulerBottomText([NullAllowed] string text);

		// -(void)setScaleRulerUnits:(GLUnitSystem)unitSystem placement:(GLMapPlacement)placement paddings:(CGPoint)paddings maxWidth:(float)maxWidth;
		[Export("setScaleRulerUnits:placement:paddings:maxWidth:")]
		void SetScaleRulerUnits(GLUnitSystem unitSystem, GLMapPlacement placement, CGPoint paddings, float maxWidth);

		// -(void)setScaleRulerTextFormatterBlock:(GLMapScaleRulerTextFormatterBlock _Nonnull)block;
		[Export("setScaleRulerTextFormatterBlock:")]
		void SetScaleRulerTextFormatterBlock(GLMapScaleRulerTextFormatterBlock block);

		// @property (readonly) CLLocation * _Nullable lastLocation;
		[NullAllowed, Export("lastLocation")]
		CLLocation LastLocation { get; }

		// @property (assign, nonatomic) BOOL showUserLocation;
		[Export("showUserLocation")]
		bool ShowUserLocation { get; set; }

		// -(void)setUserLocationImage:(UIImage * _Nullable)locationImage movementImage:(UIImage * _Nullable)movementImage;
		[Export("setUserLocationImage:movementImage:")]
		void SetUserLocationImage([NullAllowed] UIImage locationImage, [NullAllowed] UIImage movementImage);

		// -(BOOL)loadStyleFromBundle:(NSBundle * _Nonnull)styleBundle;
		[Export("loadStyleFromBundle:")]
		bool LoadStyleFromBundle(NSBundle styleBundle);

		// -(BOOL)loadStyleWithBlock:(GLMapResourceBlock _Nonnull)resourceBlock;
		[Export("loadStyleWithBlock:")]
		bool LoadStyleWithBlock(GLMapResourceBlock resourceBlock);

		// -(void)setFontScale:(float)fontScale;
		[Export("setFontScale:")]
		void SetFontScale(float fontScale);

		// -(NSArray<GLMapRasterTileSource *> * _Nullable)rasterSources;
		// -(void)setRasterSources:(NSArray<GLMapRasterTileSource *> * _Nullable)sources;
		[NullAllowed, Export("rasterSources")]
		GLMapRasterTileSource[] RasterSources { get; set; }

		// -(NSSet<NSString *> * _Nullable)getStyleOptions;
		[NullAllowed, Export("getStyleOptions")]
		NSSet<NSString> StyleOptions { get; }

		// -(void)setEnabledStyleOptions:(NSSet<NSString *> * _Nonnull)opts;
		[Export("setEnabledStyleOptions:")]
		void SetEnabledStyleOptions(NSSet<NSString> opts);

		// -(void)reloadTiles;
		[Export("reloadTiles")]
		void ReloadTiles();

		// -(void)removeAllTiles;
		[Export("removeAllTiles")]
		void RemoveAllTiles();

		// -(void)startAnimationBlock:(GLMapAnimationBlock _Nonnull)animationBlock;
		[Export("startAnimationBlock:")]
		void StartAnimationBlock(GLMapAnimationBlock animationBlock);

		// @property (assign) CGPoint mapOrigin;
		[Export("mapOrigin", ArgumentSemantic.Assign)]
		CGPoint MapOrigin { get; set; }

		// -(GLMapPoint)mapCenter;
		// -(void)setMapCenter:(GLMapPoint)center;
		[Export("mapCenter")]
		GLMapPoint MapCenter { get; set; }

		// -(void)setMapCenter:(GLMapPoint)center zoom:(double)zoom;
		[Export("setMapCenter:zoom:")]
		void SetMapCenter(GLMapPoint center, double zoom);

		// -(void)setMapCenter:(GLMapPoint)center transition:(GLMapTransitionFunction)transition;
		[Export("setMapCenter:transition:")]
		void SetMapCenter(GLMapPoint center, GLMapTransitionFunction transition);

		// -(void)setMapCenter:(GLMapPoint)center zoom:(double)zoom transition:(GLMapTransitionFunction)transition;
		[Export("setMapCenter:zoom:transition:")]
		void SetMapCenter(GLMapPoint center, double zoom, GLMapTransitionFunction transition);

		// -(void)flyTo:(GLMapGeoPoint)geoPoint;
		[Export("flyTo:")]
		void FlyTo(GLMapGeoPoint geoPoint);

		// -(void)flyTo:(GLMapGeoPoint)geoPoint zoomLevel:(CGFloat)zoomLevel;
		[Export("flyTo:zoomLevel:")]
		void FlyTo(GLMapGeoPoint geoPoint, nfloat zoomLevel);

		// -(void)flyTo:(GLMapGeoPoint)geoPoint zoomLevel:(CGFloat)zoomLevel velocity:(CGFloat)velocity;
		[Export("flyTo:zoomLevel:velocity:")]
		void FlyToWithVelocity(GLMapGeoPoint geoPoint, nfloat zoomLevel, nfloat velocity);

		// -(void)flyTo:(GLMapGeoPoint)geoPoint zoomLevel:(CGFloat)zoomLevel duration:(CGFloat)duration;
		[Export("flyTo:zoomLevel:duration:")]
		void FlyToWithDuration(GLMapGeoPoint geoPoint, nfloat zoomLevel, nfloat duration);

		// -(BOOL)flyToInProgress;
		[Export("flyToInProgress")]
		bool FlyToInProgress();

		// -(void)moveTo:(GLMapGeoPoint)geoPoint;
		[Export("moveTo:")]
		void MoveTo(GLMapGeoPoint geoPoint);

		// -(void)moveTo:(GLMapGeoPoint)geoPoint zoomLevel:(CGFloat)zoomLevel;
		[Export("moveTo:zoomLevel:")]
		void MoveTo(GLMapGeoPoint geoPoint, nfloat zoomLevel);

		// -(void)moveTo:(GLMapGeoPoint)geoPoint transition:(GLMapTransitionFunction)transition;
		[Export("moveTo:transition:")]
		void MoveTo(GLMapGeoPoint geoPoint, GLMapTransitionFunction transition);

		// -(void)moveTo:(GLMapGeoPoint)geoPoint zoomLevel:(CGFloat)zoomLevel transition:(GLMapTransitionFunction)transition;
		[Export("moveTo:zoomLevel:transition:")]
		void MoveTo(GLMapGeoPoint geoPoint, nfloat zoomLevel, GLMapTransitionFunction transition);

		// -(void)changeZoomLevel:(int)diff center:(GLMapPoint)center;
		[Export("changeZoomLevel:center:")]
		void ChangeZoomLevel(int diff, GLMapPoint center);

		// -(void)startDecelerate:(GLMapPoint)velocity;
		[Export("startDecelerate:")]
		void StartDecelerate(GLMapPoint velocity);

		// -(double)mapZoomForBBox:(GLMapBBox)bbox viewSize:(CGSize)size;
		[Export("mapZoomForBBox:viewSize:")]
		double MapZoomForBBox(GLMapBBox bbox, CGSize size);

		// -(double)mapZoomForBBox:(GLMapBBox)bbox;
		[Export("mapZoomForBBox:")]
		double MapZoomForBBox(GLMapBBox bbox);

		// -(double)mapZoom;
		[Export("mapZoom")]
		double MapZoom();

		// @property (assign) double maxZoom;
		[Export("maxZoom")]
		double MaxZoom { get; set; }

		// -(void)setMapZoom:(double)mapZoom;
		[Export("setMapZoom:")]
		void SetMapZoom(double mapZoom);

		// -(void)setMapZoom:(double)mapZoom center:(GLMapPoint)center;
		[Export("setMapZoom:center:")]
		void SetMapZoom(double mapZoom, GLMapPoint center);

		// -(void)setMapZoom:(double)mapZoom transition:(GLMapTransitionFunction)transition;
		[Export("setMapZoom:transition:")]
		void SetMapZoom(double mapZoom, GLMapTransitionFunction transition);

		// -(void)setMapZoom:(double)mapZoom center:(GLMapPoint)center transition:(GLMapTransitionFunction)transition;
		[Export("setMapZoom:center:transition:")]
		void SetMapZoom(double mapZoom, GLMapPoint center, GLMapTransitionFunction transition);

		// -(double)mapAngle;
		[Export("mapAngle")]
		double MapAngle();

		// -(void)setMapAngle:(double)angle;
		[Export("setMapAngle:")]
		void SetMapAngle(double angle);

		// -(void)setMapAngle:(double)angle center:(GLMapPoint)center;
		[Export("setMapAngle:center:")]
		void SetMapAngle(double angle, GLMapPoint center);

		// -(void)setMapAngle:(double)angle transition:(GLMapTransitionFunction)transition;
		[Export("setMapAngle:transition:")]
		void SetMapAngle(double angle, GLMapTransitionFunction transition);

		// -(void)setMapAngle:(double)angle center:(GLMapPoint)center transition:(GLMapTransitionFunction)transition;
		[Export("setMapAngle:center:transition:")]
		void SetMapAngle(double angle, GLMapPoint center, GLMapTransitionFunction transition);

		// -(GLMapPoint)makeMapPointFromDisplayPoint:(CGPoint)displayPoint;
		[Export("makeMapPointFromDisplayPoint:")]
		GLMapPoint MakeMapPointFromDisplayPoint(CGPoint displayPoint);

		// -(GLMapPoint)makeMapPointFromDisplayDelta:(CGPoint)displayDelta;
		[Export("makeMapPointFromDisplayDelta:")]
		GLMapPoint MakeMapPointFromDisplayDelta(CGPoint displayDelta);

		// -(GLMapPoint)makeMapPointFromDisplayDelta:(CGPoint)displayDelta andMapZoom:(double)mapZoom;
		[Export("makeMapPointFromDisplayDelta:andMapZoom:")]
		GLMapPoint MakeMapPointFromDisplayDelta(CGPoint displayDelta, double mapZoom);

		// -(CGPoint)makeDisplayPointFromGeoPoint:(GLMapGeoPoint)geoPoint;
		[Export("makeDisplayPointFromGeoPoint:")]
		CGPoint MakeDisplayPointFromGeoPoint(GLMapGeoPoint geoPoint);

		// -(GLMapGeoPoint)makeGeoPointFromDisplayPoint:(CGPoint)displayPoint;
		[Export("makeGeoPointFromDisplayPoint:")]
		GLMapGeoPoint MakeGeoPointFromDisplayPoint(CGPoint displayPoint);

		// -(CGPoint)makeDisplayPointFromMapPoint:(GLMapPoint)mapPoint;
		[Export("makeDisplayPointFromMapPoint:")]
		CGPoint MakeDisplayPointFromMapPoint(GLMapPoint mapPoint);

		// +(GLMapPoint)makeMapPointFromGeoPoint:(GLMapGeoPoint)geoPoint;
		[Static]
		[Export("makeMapPointFromGeoPoint:")]
		GLMapPoint MakeMapPointFromGeoPoint(GLMapGeoPoint geoPoint);

		// +(GLMapGeoPoint)makeGeoPointFromMapPoint:(GLMapPoint)mapPoint;
		[Static]
		[Export("makeGeoPointFromMapPoint:")]
		GLMapGeoPoint MakeGeoPointFromMapPoint(GLMapPoint mapPoint);

		// -(double)makePixelsFromMeters:(double)meters;
		[Export("makePixelsFromMeters:")]
		double MakePixelsFromMeters(double meters);

		// -(double)makeMetersFromPixels:(double)pixels;
		[Export("makeMetersFromPixels:")]
		double MakeMetersFromPixels(double pixels);

		// -(double)makeInternalFromMeters:(double)meters;
		[Export("makeInternalFromMeters:")]
		double MakeInternalFromMeters(double meters);

		// -(double)makeMetersFromInternal:(double)internal;
		[Export("makeMetersFromInternal:")]
		double MakeMetersFromInternal(double @internal);

		// -(double)makeInternalFromPixels:(double)pixels;
		[Export("makeInternalFromPixels:")]
		double MakeInternalFromPixels(double pixels);

		// -(double)makePixelsFromInternal:(double)internal;
		[Export("makePixelsFromInternal:")]
		double MakePixelsFromInternal(double @internal);

		// -(GLMapImage * _Nullable)displayImage:(UIImage * _Nonnull)image;
		[Export("displayImage:")]
		[return: NullAllowed]
		GLMapImage DisplayImage(UIImage image);

		// -(GLMapImage * _Nullable)displayImage:(UIImage * _Nonnull)image drawOrder:(int)drawOrder;
		[Export("displayImage:drawOrder:")]
		[return: NullAllowed]
		GLMapImage DisplayImage(UIImage image, int drawOrder);

		// -(GLMapImage * _Nullable)displayText:(NSString * _Nonnull)text withStyle:(GLMapVectorStyle * _Nonnull)style drawOrder:(int)drawOrder completion:(dispatch_block_t _Nonnull)completion;
		[Export("displayText:withStyle:drawOrder:completion:")]
		[return: NullAllowed]
		GLMapImage DisplayText(string text, GLMapVectorStyle style, int drawOrder, NSDispatchHandlerT completion);

		// -(void)removeImage:(GLMapImage * _Nonnull)image;
		[Export("removeImage:")]
		void RemoveImage(GLMapImage image);

		// -(GLMapImageGroup * _Nullable)createImageGroup;
		[Export("createImageGroup")]
		[return: NullAllowed]
		GLMapImageGroup CreateImageGroup();

		// -(GLMapImageGroup * _Nullable)createImageGroupWithDrawOrder:(int)drawOrder;
		[Export("createImageGroupWithDrawOrder:")]
		[return: NullAllowed]
		GLMapImageGroup CreateImageGroupWithDrawOrder(int drawOrder);

		// -(void)removeImageGroup:(GLMapImageGroup * _Nonnull)imageGroup;
		[Export("removeImageGroup:")]
		void RemoveImageGroup(GLMapImageGroup imageGroup);

		// -(void)addStyle:(GLMapVectorCascadeStyle * _Nonnull)style;
		[Export("addStyle:")]
		void AddStyle(GLMapVectorCascadeStyle style);

		// -(void)removeStyle:(GLMapVectorCascadeStyle * _Nonnull)style;
		[Export("removeStyle:")]
		void RemoveStyle(GLMapVectorCascadeStyle style);

		// -(GLMapVectorObject * _Nullable)mapObjectNearPoint:(GLMapPoint)point maxDistance:(double)maxDistance categories:(GLSearchCategories * _Nonnull)categories;
		[Export("mapObjectNearPoint:maxDistance:categories:")]
		[return: NullAllowed]
		GLMapVectorObject MapObjectNearPoint(GLMapPoint point, double maxDistance, GLSearchCategories categories);

		// -(void)addVectorObject:(GLMapVectorObject * _Nonnull)object withStyle:(GLMapVectorCascadeStyle * _Nullable)style onReadyToDraw:(dispatch_block_t _Nullable)onReadyToDraw;
		[Export("addVectorObject:withStyle:onReadyToDraw:")]
		void AddVectorObject(GLMapVectorObject @object, [NullAllowed] GLMapVectorCascadeStyle style, [NullAllowed] NSDispatchHandlerT onReadyToDraw);

		// -(void)addVectorObjects:(NSArray<GLMapVectorObject *> * _Nonnull)objects withStyle:(GLMapVectorCascadeStyle * _Nullable)style;
		[Export("addVectorObjects:withStyle:")]
		void AddVectorObjects(GLMapVectorObject[] objects, [NullAllowed] GLMapVectorCascadeStyle style);

		// -(void)addVectorObjectArray:(GLMapVectorObjectArray * _Nonnull)array withStyle:(GLMapVectorCascadeStyle * _Nullable)style;
		[Export("addVectorObjectArray:withStyle:")]
		void AddVectorObjectArray(GLMapVectorObjectArray array, [NullAllowed] GLMapVectorCascadeStyle style);

		// -(void)removeVectorObject:(GLMapVectorObject * _Nonnull)object;
		[Export("removeVectorObject:")]
		void RemoveVectorObject(GLMapVectorObject @object);

		// -(void)removeVectorObjects:(NSArray<GLMapVectorObject *> * _Nonnull)objects;
		[Export("removeVectorObjects:")]
		void RemoveVectorObjects(GLMapVectorObject[] objects);

		// -(GLMapMarkerLayer * _Nonnull)displayMarkers:(NSArray<id> * _Nonnull)markers withStyles:(GLMapMarkerStyleCollection * _Nonnull)styles;
		[Export("displayMarkers:withStyles:")]
		GLMapMarkerLayer DisplayMarkers(NSObject[] markers, GLMapMarkerStyleCollection styles);

		// -(void)displayMarkerLayer:(GLMapMarkerLayer * _Nonnull)layer completion:(dispatch_block_t _Nullable)completion;
		[Export("displayMarkerLayer:completion:")]
		void DisplayMarkerLayer(GLMapMarkerLayer layer, [NullAllowed] NSDispatchHandlerT completion);

		// -(void)removeMarkerLayer:(GLMapMarkerLayer * _Nonnull)layer;
		[Export("removeMarkerLayer:")]
		void RemoveMarkerLayer(GLMapMarkerLayer layer);

		// -(GLMapTrack * _Nonnull)displayTrackData:(GLMapTrackData * _Nonnull)trackData;
		[Export("displayTrackData:")]
		GLMapTrack DisplayTrackData(GLMapTrackData trackData);

		// -(GLMapTrack * _Nullable)displayTrackData:(GLMapTrackData * _Nonnull)trackData drawOrder:(int)drawOrder;
		[Export("displayTrackData:drawOrder:")]
		[return: NullAllowed]
		GLMapTrack DisplayTrackData(GLMapTrackData trackData, int drawOrder);

		// -(void)removeTrack:(GLMapTrack * _Nonnull)track;
		[Export("removeTrack:")]
		void RemoveTrack(GLMapTrack track);

		// -(void)removeAllTracks;
		[Export("removeAllTracks")]
		void RemoveAllTracks();

		// @property (strong) GLMapLocaleSettings * _Nonnull localeSettings;
		[Export("localeSettings", ArgumentSemantic.Strong)]
		GLMapLocaleSettings LocaleSettings { get; set; }
	}

	// @interface GLSearchCategories : NSObject
	[BaseType(typeof(NSObject))]
	interface GLSearchCategories
	{
		// -(instancetype _Nullable)initWithPath:(NSString * _Nonnull)path;
		[Export("initWithPath:")]
		IntPtr Constructor(string path);

		// -(NSArray<GLSearchCategory *> * _Nonnull)topCategories;
		[Export("topCategories")]
		GLSearchCategory[] TopCategories { get; }

		// -(NSArray<GLSearchCategory *> * _Nonnull)categoriesStartedWith:(NSArray<NSString *> * _Nonnull)words localeSettings:(GLMapLocaleSettings * _Nonnull)localeSettings;
		[Export("categoriesStartedWith:localeSettings:")]
		GLSearchCategory[] CategoriesStartedWith(string[] words, GLMapLocaleSettings localeSettings);

		// -(GLSearchCategory * _Nullable)findByName:(NSString * _Nonnull)name;
		[Export("findByName:")]
		[return: NullAllowed]
		GLSearchCategory FindByName(string name);

		// -(GLSearchCategory * _Nullable)findByIconName:(NSString * _Nonnull)iconName;
		[Export("findByIconName:")]
		[return: NullAllowed]
		GLSearchCategory FindByIconName(string iconName);
	}

	// @interface GLSearchCategory : NSObject
	[BaseType(typeof(NSObject))]
	interface GLSearchCategory
	{
		// -(NSString * _Nullable)localizedName:(GLMapLocaleSettings * _Nonnull)settings;
		[Export("localizedName:")]
		[return: NullAllowed]
		string LocalizedName(GLMapLocaleSettings settings);

		// -(NSAttributedString * _Nullable)attributedName:(NSDictionary * _Nonnull)normal highlight:(NSDictionary * _Nonnull)highlight localeSettings:(GLMapLocaleSettings * _Nonnull)localeSettings;
		[Export("attributedName:highlight:localeSettings:")]
		[return: NullAllowed]
		NSAttributedString AttributedName(NSDictionary normal, NSDictionary highlight, GLMapLocaleSettings localeSettings);

		// @property (readonly) NSString * _Nonnull iconName;
		[Export("iconName")]
		string IconName { get; }

		// @property (readonly) NSArray<GLSearchCategory *> * _Nonnull childs;
		[Export("childs")]
		GLSearchCategory[] Childs { get; }
	}

	// typedef void (^GLMapSearchCompletionBlock)(NSArray<GLMapVectorObject *> * _Nonnull);
	delegate void GLMapSearchCompletionBlock(GLMapVectorObject[] arg0);

	// typedef void (^GLMapSearchInfoBlock)(id _Nonnull, GLMapPoint * _Nonnull, GLSearchCategory * _Nullable * _Nonnull);
    unsafe delegate void GLMapSearchInfoBlock(NSObject arg0, GLMapPoint arg1, GLSearchCategory arg2);

	// @interface GLSearchOffline : NSObject
	[BaseType(typeof(NSObject))]
	interface GLSearchOffline
	{
		// -(void)addNameFilter:(NSString * _Nonnull)word;
		[Export("addNameFilter:")]
		void AddNameFilter(string word);

		// -(void)addNamesFilter:(NSArray<NSString *> * _Nonnull)words;
		[Export("addNamesFilter:")]
		void AddNamesFilter(string[] words);

		// -(void)setTiles:(NSSet<NSNumber *> * _Nonnull)tiles;
		[Export("setTiles:")]
		void SetTiles(NSSet<NSNumber> tiles);

		// -(void)setCategories:(GLSearchCategories * _Nonnull)categories;
		[Export("setCategories:")]
		void SetCategories(GLSearchCategories categories);

		// -(void)setLocaleSettings:(GLMapLocaleSettings * _Nonnull)localeSettings;
		[Export("setLocaleSettings:")]
		void SetLocaleSettings(GLMapLocaleSettings localeSettings);

		// -(void)addCategoryFilter:(GLSearchCategory * _Nonnull)category;
		[Export("addCategoryFilter:")]
		void AddCategoryFilter(GLSearchCategory category);

		// -(void)addTagFilter:(NSString * _Nonnull)tag value:(NSString * _Nonnull)value;
		[Export("addTagFilter:value:")]
		void AddTagFilter(string tag, string value);

		// -(void)addTagFilter:(NSString * _Nonnull)tag values:(NSArray<NSString *> * _Nonnull)values;
		[Export("addTagFilter:values:")]
		void AddTagFilter(string tag, string[] values);

		// -(void)setLimit:(uint32_t)limit;
		[Export("setLimit:")]
		void SetLimit(uint limit);

		// -(void)setBBox:(GLMapBBox)bbox;
		[Export("setBBox:")]
		void SetBBox(GLMapBBox bbox);

		// -(void)setCenter:(GLMapPoint)center;
		[Export("setCenter:")]
		void SetCenter(GLMapPoint center);

		// -(void)addResults:(NSArray<id> * _Nonnull)results infoBlock:(GLMapSearchInfoBlock _Nonnull)infoBlock;
		[Export("addResults:infoBlock:")]
		void AddResults(NSObject[] results, GLMapSearchInfoBlock infoBlock);

		// -(void)startWithCompletionBlock:(GLMapSearchCompletionBlock _Nonnull)completionBlock;
		[Export("startWithCompletionBlock:")]
		void StartWithCompletionBlock(GLMapSearchCompletionBlock completionBlock);

		// -(void)cancel;
		[Export("cancel")]
		void Cancel();
	}
}
