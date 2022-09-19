using System.IO;

namespace Marajoara.Cinema.Management.Domain.Common
{
    public interface IFileImageService
    {
        /// <summary>
        /// Process a given stream to a byte array, validating if its a valid image. (Size and Format)
        /// Valid formats: Must be PNG, JPG or BMP and Max image size of 500kb.
        /// Max size: 500kb.
        /// Throws exception if not valid. 
        /// </summary>
        /// <param name="stream">Stream that contains image data.</param>
        /// <returns>Bytes array read from image stream.</returns>
        byte[] GetImageBytes(Stream stream);
    }
}