using Marajoara.Cinema.Management.Domain.Common;
using Marajoara.Cinema.Management.Domain.MovieModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marajoara.Cinema.Management.Application.Features
{
    public class FileImageService : IFileImageService
    {
        private const int MAX_IMAGE_SIZE = 1024 * 500;

        public byte[] GetImageBytes(Stream stream)
        {
            if (stream == null)
                throw new ArgumentException("Stream parameter cannot be null.", nameof(stream));

            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }

                byte[] imageBytes = mStream.ToArray();
                ValidateImage(imageBytes);

                return imageBytes;
            }
        }

        private void ValidateImage(byte[] fileBytes)
        {
            if (!IsImage(fileBytes))
                throw new FormatException("Invalid Stream Format. File is not an Image (png, bmp or jpg).");
            if (fileBytes.Length > MAX_IMAGE_SIZE)
                throw new Exception($"Image file size cannot be greater than 500kb. File size: {(fileBytes.Length / 1024)}kb");
        }

        private bool IsImage(byte[] fileBytes)
        {
            var headers = new List<byte[]>
            {
                Encoding.ASCII.GetBytes("BM"),      // BMP
                new byte[] { 137, 80, 78, 71 },     // PNG
                new byte[] { 255, 216, 255, 224 },  // JPEG
                new byte[] { 255, 216, 255, 225 }   // JPEG CANON
            };

            return headers.Any(x => x.SequenceEqual(fileBytes.Take(x.Length)));
        }
    }
}
