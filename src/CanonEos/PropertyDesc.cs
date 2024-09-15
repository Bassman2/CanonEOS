namespace CanonEos;

public class PropertyDesc
{
    internal PropertyDesc(EdsPropertyID id, EdsPropertyDesc propertyDesc)
    {
        this.Id = id;
        this.Form = propertyDesc.Form;
        this.Access = propertyDesc.Access;
        this.NumElements = propertyDesc.NumElements;
        this.PropDesc = propertyDesc.PropDesc;
    }

    public EdsPropertyID Id { get; }
    public int Form { get; }

    public int Access { get; }

    public int NumElements { get; }

    public int[] PropDesc { get; }
}
