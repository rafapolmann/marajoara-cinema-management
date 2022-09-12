using FluentAssertions;
using Marajoara.Cinema.Management.Application.Features;
using Marajoara.Cinema.Management.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Marajoara.Cinema.Management.Tests.Integration.Application
{
    [TestClass]
    public class FileImageServiceTests
    {
        private IFileImageService _fileImageService;

        [TestInitialize]
        public void Initialize()
        {
            _fileImageService = new FileImageService();
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Return_Image_Bytes_When_File_Is_A_PNG()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.PNG_FILE.png");
            byte[] fileBytes = _fileImageService.GetImageBytes(stream);

            fileBytes.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Return_Image_Bytes_When_File_Is_A_JPG()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.JPG_FILE.jpg");
            byte[] fileBytes = _fileImageService.GetImageBytes(stream);

            fileBytes.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Return_Image_Bytes_When_File_Is_A_BMP()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.BMP_FILE.bmp");
            byte[] fileBytes = _fileImageService.GetImageBytes(stream);

            fileBytes.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Throw_FormatException_When_Is_Not_A_Valid_Format_File()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.INVALID_FILE.txt");
            Action action = () => _fileImageService.GetImageBytes(stream);

            action.Should().Throw<FormatException>().WithMessage("Invalid Stream Format. File is not an Image (png, bmp or jpg).");
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Throw_Exception_When_File_Size_Is_Greater_Than_500kb()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetEmbeddedResourceStream("Resources.MUCH_BIG_FILE.bmp");
            Action action = () => _fileImageService.GetImageBytes(stream);
            
            action.Should().Throw<Exception>().WithMessage($"Image file size cannot be greater than 500kb. File size: {stream.Length / 1024}kb");
        }

        [TestMethod]
        public void FileImageService_GetImageBytes_Should_Throw_ArgumentException_When_File_Stream_Is_Null()
        {
            Stream stream = null;
            Action action = () => _fileImageService.GetImageBytes(stream);

            action.Should().Throw<ArgumentException>().WithMessage("Stream parameter cannot be null. (Parameter 'stream')");
        }
    }

    public static class AssemblyResourcesExtension
    {
        public static Stream GetEmbeddedResourceStream(this Assembly assembly, string relativeResourcePath)
        {
            if (string.IsNullOrEmpty(relativeResourcePath))
                throw new ArgumentNullException("relativeResourcePath");

            var resourcePath = String.Format("{0}.{1}",
                               Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$", string.Empty, RegexOptions.IgnoreCase),
                               relativeResourcePath);

            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException(String.Format("The specified embedded resource \"{0}\" is not found.", relativeResourcePath));

            return stream;
        }
    }
}
