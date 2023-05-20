using Microsoft.Win32;
using ModelLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client_Wpf.Models
{
    public static class Helper
    {
        //конвертувати BitmapImage в масив байтів
        public static byte[]? ImageToBytes(BitmapImage? image)
        {
            if (image == null)
            {
                return null;
            }
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                byteArray = stream.ToArray();
            }
            return byteArray;
        }
        //конвертувати  масив байтів в BitmapImage 
        public static BitmapImage? ImageFromBytes(byte[]? bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            // Load the byte array into a memory stream
            MemoryStream stream = new MemoryStream(bytes);

            // Create a new BitmapImage object
            BitmapImage bitmapImage = new BitmapImage();

            // Set the BitmapImage object's source to the memory stream
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            return bitmapImage;
        }
        //Вибрати лише Фотографії з файла
        public static BitmapImage OpenFromFile()
        {
            BitmapImage image = new();
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image files (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                image = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            return image;
        }

    }
}
