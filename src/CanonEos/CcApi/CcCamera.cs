﻿namespace CanonEos.CcApi;

internal class CcCamera : Camera
{
    internal CcCamera(nint camera)
    {
        this.Name = "";
        this.Volumes = [];
    }

    public override string Name { get; }
    public override string? ProductName { get; }
    public override string? OwnerName { get; }
    public override string? FirmwareVersion { get; }
    public override string? CurrentStorage { get; }
    public override string? CurrentFolder { get; }
    public override string? BodyIDEx { get; }
    public override string? LensName { get; }
    public override string? Artist { get; }
    public override string? Copyright { get; }

    public override IEnumerable<Volume> Volumes { get; }

}
