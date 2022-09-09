using System.IO;

namespace Marajoara.Cinema.Management.Domain.Common
{
    public interface IFileImageService
    {
        byte[] GetImageBytes(Stream stream);
    }
}
