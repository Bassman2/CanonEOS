namespace CanonEos.EdSdk.Internal;

internal enum EdsError : int
{
    OK                                  /**/ = 0x00000000,

    /////////////////////////////////////////////////////////////////////////// 
    // Miscellaneous errors

    Unimplemented                       /**/ = 0x00000001,
    InternalError                       /**/ = 0x00000002,
    MemAllocFailed                      /**/ = 0x00000003,
    MemFreeFailed                       /**/ = 0x00000004,
    OperationCancelled                  /**/ = 0x00000005,
    IncompatibleVersion                 /**/ = 0x00000006,
    NotSupported                        /**/ = 0x00000007,
    UnexpectedException                 /**/ = 0x00000008,
    ProtectionViolation                 /**/ = 0x00000009,
    MissingSubcomponent                 /**/ = 0x0000000A,
    SelectionUnavailable                /**/ = 0x0000000B,

    /////////////////////////////////////////////////////////////////////////// 
    // File errors

    FileIOError                         /**/ = 0x00000020,
    FileTooManyOpen                     /**/ = 0x00000021,
    FileNotFound                        /**/ = 0x00000022,
    FileOpenError                       /**/ = 0x00000023,
    FileCloseError                      /**/ = 0x00000024,
    FileSeekError                       /**/ = 0x00000025,
    FileTellError                       /**/ = 0x00000026,
    FileReadError                       /**/ = 0x00000027,
    FileWriteError                      /**/ = 0x00000028,
    FilePermissionError                 /**/ = 0x00000029,
    FileDiskFullError                   /**/ = 0x0000002A,
    FileAlreadyExists                   /**/ = 0x0000002B,
    FileFormatUnrecognized              /**/ = 0x0000002C,
    FileDataCorrupt                     /**/ = 0x0000002D,
    FileNamingNA                        /**/ = 0x0000002E,

    /////////////////////////////////////////////////////////////////////////// 
    // Directory errors

    DirNotFound                         /**/ = 0x00000040,
    DirIOError                          /**/ = 0x00000041,
    DirEntryNotFound                    /**/ = 0x00000042,
    DirEntryExists                      /**/ = 0x00000043,
    DirNotEmpty                         /**/ = 0x00000044,

    /////////////////////////////////////////////////////////////////////////// 
    // Property errors

    [Description("")]
    PropertiesUnavailable               /**/ = 0x00000050,
    PropertiesMismatch                  /**/ = 0x00000051,
    PropertiesNotLoaded                 /**/ = 0x00000053,

    /////////////////////////////////////////////////////////////////////////// 
    // Function Parameter errors

    InvalidParameter                    /**/ = 0x00000060,
    InvalidHandle                       /**/ = 0x00000061,
    InvalidPointer                      /**/ = 0x00000062,
    InvalidIndex                        /**/ = 0x00000063,
    InvalidLength                       /**/ = 0x00000064,
    InvalidFnPointer                    /**/ = 0x00000065,
    InvalidSortFn                       /**/ = 0x00000066,

    /////////////////////////////////////////////////////////////////////////// 
    // Device errors

    /////////////////////////////////////////////////////////////////////////// 
    // Stream errors

    /////////////////////////////////////////////////////////////////////////// 
    // Communication errors

    /////////////////////////////////////////////////////////////////////////// 
    // Lock / Unkock

    UsbDeviceLockError                  /**/ = 0x000000D0,
    UsbDeviceUnlockError                /**/ = 0x000000D1,

}