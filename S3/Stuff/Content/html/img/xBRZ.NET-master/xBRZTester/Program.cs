using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using xBRZNet;

namespace xBRZTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SaveScaledImage();
        }

        private static void SaveScaledImage()
        {
            DirectoryInfo d = new DirectoryInfo(@"..\..\Images\pmstock\");

            foreach (var file in d.GetFiles("*.png"))
            {
                var originalImage = new Bitmap(file.FullName);

                string fileName = file.Name;
                const string imageExtension = ".png";

                //originalImage.Save(fileName + "-orig" + imageExtension, ImageFormat.Png);

                const int scaleSize = 3;
                var scaledImage = new xBRZScaler().ScaleImage(originalImage, scaleSize);

                scaledImage.Save(fileName, ImageFormat.Png);
            }
        }
    }
}
