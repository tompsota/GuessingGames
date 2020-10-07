using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;

namespace GuessingGame
{
    /// <summary>
    /// Helper class with various utilities
    /// </summary>
    public static class Utils
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// resizes the image, so it is less blurred
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Image image, double currentPercentage)
        {
            int width = (int)Math.Round(image.Width * currentPercentage, MidpointRounding.AwayFromZero);
            int height = (int)Math.Round(image.Height * currentPercentage, MidpointRounding.AwayFromZero);
            var img = new Bitmap(image, width, height);
            width = (int)Math.Round(img.Width / currentPercentage, MidpointRounding.AwayFromZero);
            height = (int)Math.Round(img.Height / currentPercentage, MidpointRounding.AwayFromZero);
            return new Bitmap(img, width, height);
        }

        /// <summary>
        /// Convert image to bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        /// <summary>
        /// Get text from picture's description for guessing buttons
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetTitleValue(string location)
        {
            Image im = Image.FromFile(location);
            var title = im.PropertyItems.FirstOrDefault(x => x.Id == 0x10e);
            return Encoding.ASCII.GetString(title.Value);
        }
    }
}
