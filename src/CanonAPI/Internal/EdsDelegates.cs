namespace CanonAPI.Internal;

internal delegate EdsError EdsCameraAddedHandler(IntPtr inContext);

internal delegate EdsError EdsProgressCallback(int inPercent, IntPtr inContext, ref bool outCancel);

internal delegate EdsError EdsPropertyEventHandler(EdsPropertyEventID inEvent, EdsPropertyID inPropertyID, int inParameter, IntPtr inContext);

internal delegate EdsError EdsObjectEventHandler(EdsObjectEventID inEvent, IntPtr inRef, IntPtr inContext);

internal delegate EdsError EdsStateEventHandler(EdsStateEventID inEvent, int inParameter, IntPtr inContext);

