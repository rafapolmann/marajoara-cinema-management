using System.IO;

namespace Marajoara.Cinema.Management.Domain.Common
{
    public interface IFileImageService
    {
        /// <summary>
        /// Process an image stream data validating if is a valid format and is a compatible size transforming in bytes array.
        /// Valid formats for Stream are PNG, JPG and BMP.
        /// Max size data is 500kb
        /// </summary>
        /// <param name="stream">Stream that contains image data.</param>
        /// <returns>Bytes array read from image stream.</returns>
        byte[] GetImageBytes(Stream stream);
    }
}