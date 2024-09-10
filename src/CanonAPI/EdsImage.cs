namespace CanonAPI;

public class EdsImage
{
    private nint image;

    internal EdsImage(nint image)
    {
        this.image = image;

        Eds.EdsGetImageInfo(this.image, EdsImageSource.Thumbnail, out EdsImageInfo thumbnailImageInfo);
        Eds.EdsGetImageInfo(this.image, EdsImageSource.Preview, out EdsImageInfo previewImageInfo);
        Eds.EdsGetImageInfo(this.image, EdsImageSource.FullView, out EdsImageInfo fullViewImageInfo);
        Eds.EdsGetImageInfo(this.image, EdsImageSource.RAWThumbnail, out EdsImageInfo rawThumbnailImageInfo);
        Eds.EdsGetImageInfo(this.image, EdsImageSource.RAWFullView, out EdsImageInfo rawFullViewImageInfo);
        
    }

    public IEnumerable<Property> Properties
    {
        get => Eds.GetPictureProperties(image);
    }
}
