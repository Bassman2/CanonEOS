namespace CanonEos.CcApi.Internal;

[XmlRoot("root", Namespace="urn:schemas-upnp-org:device-1-0")]
public class CameraDevDesc
{
    [XmlElement("specVersion")]
    public SpecVersion? SpecVersion { get; set; }

    [XmlElement("URLBase")]
    public string? URLBase { get; set; }

    [XmlElement("device")]
    public Device? Device { get; set; }
}

public class SpecVersion
{
    [XmlElement("major")]
    public int Major { get; set; }

    [XmlElement("minor")]
    public int Minor { get; set; }
}

public class Device
{
    [XmlElement("deviceType")]
    public string? DeviceType { get; set; }

    [XmlElement("friendlyName")]
    public string? FriendlyName { get; set; }

    [XmlElement("manufacturer")]
    public string? Manufacturer { get; set; }

    [XmlElement("manufacturerURL")]
    public string? ManufacturerURL { get; set; }

    [XmlElement("modelDescription")]
    public string? ModelDescription { get; set; }

    [XmlElement("modelName")]
    public string? ModelName { get; set; }

    [XmlElement("serialNumber")]
    public string? SerialNumber { get; set; }

    [XmlElement("UDN")]
    public string? UDN { get; set; }

    [XmlArray("serviceList")]
    [XmlArrayItem("service")]
    public List<Service>? ServiceList { get; set; }

    [XmlElement("presentationURL")]
    public string? PresentationURL { get; set; }

    //[XmlElement("X_compatibleId", Namespace = "http://schemas.microsoft.com/windows/pnpx/2005/11")]
    //public string? X_compatibleId { get; set; }

    //[XmlElement("X_deviceCategory", Namespace = "http://schemas.microsoft.com/windows/pnpx/2005/11")]
    //public string? X_deviceCategory { get; set; }

    //[XmlElement("X_deviceCategory", Namespace = "http://schemas.microsoft.com/windows/2008/09/devicefoundation")]
    //public string? X_deviceCategory { get; set; }
}

public class Service
{
    [XmlElement("serviceType")]
    public string? ServiceType { get; set; }

    [XmlElement("serviceId")]
    public string? ServiceId { get; set; }

    [XmlElement("SCPDURL")]
    public string? SCPDURL { get; set; }

    [XmlElement("controlURL")]
    public string? ControlURL { get; set; }

    [XmlElement("eventSubURL")]
    public string? EventSubURL { get; set; }

    [XmlElement("X_targetId", Namespace = "urn:schemas-canon-com:schema-upnp")]
    public string? TargetId { get; set; }

    [XmlElement("X_onService", Namespace = "urn:schemas-canon-com:schema-upnp")]
    public string? OnService { get; set; }

    [XmlElement("X_accessURL", Namespace = "urn:schemas-canon-com:schema-upnp")]
    public string? AccessURL { get; set; }

    [XmlElement("X_deviceUsbId", Namespace = "urn:schemas-canon-com:schema-upnp")]
    public string? DeviceUsbId { get; set; }

    [XmlElement("X_deviceNickname", Namespace = "urn:schemas-canon-com:schema-upnp")]
    public string? DeviceNickname { get; set; }
}