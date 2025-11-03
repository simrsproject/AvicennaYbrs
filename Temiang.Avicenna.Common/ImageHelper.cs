using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Temiang.Avicenna.Common
{
    /// Untuk ImageHelper ini jangan memakai method static karena dicurigai membuat crash app pool (Handono 2022 Sept) ///
    public class ImageHelper
    {
        public Image ResizeImage(byte[] image, Size size)
        {
            return ResizeImage(ConvertByteArrayToImage(image), size, true, InterpolationMode.Default);
        }

        public Image ResizeImage(Image image, Size size)
        {
            return ResizeImage(image, size, true, InterpolationMode.Default);
        }
        public Image ResizeImage(byte[] image, Size size, bool preserveAspectRatio, InterpolationMode interpolationMode)
        {
            return ResizeImage(ConvertByteArrayToImage(image), size, preserveAspectRatio, interpolationMode);
        }
        public Image ResizeImage(Image image, Size size, bool preserveAspectRatio, InterpolationMode interpolationMode)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = interpolationMode;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return ret;
        }

        public Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public Image ConvertBase64StringToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
        }

        public byte[] LoadImageToArray(string fileName)
        {
            if (!System.IO.File.Exists(fileName)) return null;

            //Initialize a file stream to read the image file
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            //Initialize a byte array with size of stream
            var imgByteArr = new byte[fs.Length];
            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return imgByteArr;
        }

        public MemoryStream LoadImageToMemoryStream(string fullPathFileName)
        {
            if (!System.IO.File.Exists(fullPathFileName)) return null;

            MemoryStream mstream = new MemoryStream();
            // Load blank Image Template
            using (FileStream file = new FileStream(fullPathFileName, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                mstream.Write(bytes, 0, (int)file.Length);
            }
            return mstream;
        }

        // Save the file with a specific compression level.
        public void Compress(Image image, string fileName, long compression)
        {
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality, compression);

            var imageCodecInfo = GetEncoderInfo("image/jpeg");
            File.Delete(fileName);
            image.Save(fileName, imageCodecInfo, encoderParams);
        }
        //public byte[] CompressImageToArray(this Image image, int jpegQuality = 20)
        //{
        //    return Compress(ToByteArray(image, ImageFormat.Jpeg), jpegQuality);
        //}
        public byte[] CompressImageToArray(Image image, int jpegQuality = 20)
        {
            return Compress(ToByteArray(image, ImageFormat.Jpeg), jpegQuality);
        }
        public byte[] Compress(byte[] byteArray, int jpegQuality = 20)
        {
            // Sample some photo compressed using 20, 50 and 80 as the value of jpegQuality. 
            // Sizes are 4.99, 8.28 and 12.9 KB.
            byte[] retval = null;
            using (var inputStream = new MemoryStream(byteArray))
            {
                var imageTmp = Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality);
                using (var outputStream = new MemoryStream())
                {
                    imageTmp.Save(outputStream, jpegEncoder, encoderParameters);
                    retval = outputStream.ToArray(); // Yeah 1KB from 14KB
                }
            }
            return retval;
        }
        // Return an ImageCodecInfo object for this mime type.
        public ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i <= encoders.Length; i++)
            {
                if (encoders[i].MimeType == mimeType) return encoders[i];
            }
            return null;
        }

        //public string ToBase64String(this Image img, ImageFormat imageFormat)
        public string ToBase64String(Image img, ImageFormat imageFormat)
        {
            string base64String = string.Empty;
            byte[] byteBuffer;
            using (var memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, imageFormat);
                memoryStream.Position = 0;
                byteBuffer = memoryStream.ToArray();
                memoryStream.Close();
            }
            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;
            return base64String;
        }
        //public string ToBase64DataImage(this Image img, ImageFormat imageFormat)
        public string ToBase64DataImage(Image img, ImageFormat imageFormat)
        {
            string base64String = string.Empty;
            byte[] byteBuffer;
            using (var memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, imageFormat);
                memoryStream.Position = 0;
                byteBuffer = memoryStream.ToArray();
                memoryStream.Close();
            }
            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;
            return string.Format("data:image/{0};base64,{1}", imageFormat.ToString(), base64String);
        }

        //public Image ToImage(this string base64String)
        public Image ToImage(string base64String)
        {
            return ConvertBase64StringToImage(base64String);
            //// Convert base 64 string to byte[]
            //byte[] imageBytes = Convert.FromBase64String(base64String);
            //// Convert byte[] to Image
            //using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            //{
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            //    return image;
            //}
        }

        //public byte[] ToByteArray(this Image img, ImageFormat imageFormat)
        public byte[] ToByteArray(Image img, ImageFormat imageFormat)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, imageFormat);
            return ms.ToArray();
        }
    }
}
