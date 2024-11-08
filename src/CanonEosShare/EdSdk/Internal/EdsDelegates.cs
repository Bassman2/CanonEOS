namespace CanonEos.EdSdk.Internal;

internal delegate EdsError EdsCameraAddedHandler(nint inContext);

internal delegate EdsError EdsProgressCallback(int inPercent, nint inContext, ref bool outCancel);

internal delegate EdsError EdsPropertyEventHandler(EdsPropertyEventID inEvent, EdsPropertyID inPropertyID, int inParameter, nint inContext);

internal delegate EdsError EdsObjectEventHandler(EdsObjectEventID inEvent, nint inRef, nint inContext);

internal delegate EdsError EdsStateEventHandler(EdsStateEventID inEvent, int inParameter, nint inContext);

