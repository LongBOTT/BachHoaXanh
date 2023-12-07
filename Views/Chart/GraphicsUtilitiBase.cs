using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Net;
    using System.Runtime.InteropServices;

    public static class GraphicsUtilitiBase
    {
        private static Graphics GetGraphics(Bitmap image)
        {
            return Graphics.FromImage(image);
        }

        public static Bitmap CreateColorModelCompatibleImage(Bitmap image)
        {
            return new Bitmap(image);
        }

        public static Bitmap CreateCompatibleImage(Bitmap image)
        {
            return CreateCompatibleImage(image, image.Width, image.Height);
        }

        public static Bitmap CreateCompatibleImage(Bitmap image, int width, int height)
        {
            return new Bitmap(width, height, image.PixelFormat);
        }

        public static Bitmap CreateCompatibleImage(int width, int height)
        {
            return new Bitmap(width, height);
        }
        public static Bitmap CreateCompatibleTranslucentImage(int width, int height)
        {
            return new Bitmap(width, height, PixelFormat.Format32bppArgb);
        }

        public static Bitmap LoadCompatibleImage(Uri resource)
        {
            using (Bitmap image = new Bitmap(Image.FromStream(WebRequest.Create(resource).GetResponse().GetResponseStream())))
            {
                return ToCompatibleImage(image);
            }
        }

        public static Bitmap ToCompatibleImage(Bitmap image)
        {
            if (image.PixelFormat == PixelFormat.Format32bppArgb)
            {
                return image;
            }
            Bitmap compatibleImage = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(compatibleImage))
            {
                g.DrawImage(image, 0, 0);
            }
            return compatibleImage;
        }

        public static Bitmap CreateThumbnailFast(Bitmap image, int newSize)
        {
            float ratio;
            int width = image.Width;
            int height = image.Height;

            if (width > height)
            {
                if (newSize >= width)
                {
                    throw new ArgumentException("newSize must be lower than the image width");
                }
                else if (newSize <= 0)
                {
                    throw new ArgumentException("newSize must be greater than 0");
                }

                ratio = (float)width / (float)height;
                width = newSize;
                height = (int)(newSize / ratio);
            }
            else
            {
                if (newSize >= height)
                {
                    throw new ArgumentException("newSize must be lower than the image height");
                }
                else if (newSize <= 0)
                {
                    throw new ArgumentException("newSize must be greater than 0");
                }

                ratio = (float)height / (float)width;
                height = newSize;
                width = (int)(newSize / ratio);
            }

            Bitmap temp = CreateCompatibleImage(image, width, height);

            using (Graphics g2 = Graphics.FromImage(temp))
            {
                g2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g2.DrawImage(image, 0, 0, temp.Width, temp.Height);
            }

            return temp;
        }

        public static Bitmap CreateThumbnailFast(Bitmap image, int newWidth, int newHeight)
        {
            if (newWidth >= image.Width || newHeight >= image.Height)
            {
                throw new ArgumentException("newWidth and newHeight cannot be greater than the image dimensions");
            }
            else if (newWidth <= 0 || newHeight <= 0)
            {
                throw new ArgumentException("newWidth and newHeight must be greater than 0");
            }

            Bitmap temp = CreateCompatibleImage(image, newWidth, newHeight);

            using (Graphics g2 = Graphics.FromImage(temp))
            {
                g2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g2.DrawImage(image, 0, 0, temp.Width, temp.Height);
            }

            return temp;
        }

        public static Bitmap CreateThumbnail(Bitmap image, int newSize)
        {
            int width = image.Width;
            int height = image.Height;
            bool isWidthGreater = width > height;

            if (isWidthGreater)
            {
                if (newSize >= width)
                {
                    throw new ArgumentException("newSize must be lower than the image width");
                }
            }
            else if (newSize >= height)
            {
                throw new ArgumentException("newSize must be lower than the image height");
            }

            if (newSize <= 0)
            {
                throw new ArgumentException("newSize must be greater than 0");
            }

            float ratioWH = (float)width / (float)height;
            float ratioHW = (float)height / (float)width;
            Bitmap thumb = image;

            do
            {
                if (isWidthGreater)
                {
                    width /= 2;
                    if (width < newSize)
                    {
                        width = newSize;
                    }
                    height = (int)(width / ratioWH);
                }
                else
                {
                    height /= 2;
                    if (height < newSize)
                    {
                        height = newSize;
                    }
                    width = (int)(height / ratioHW);
                }

                Bitmap temp = CreateCompatibleImage(image, width, height);

                using (Graphics g2 = Graphics.FromImage(temp))
                {
                    g2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g2.DrawImage(thumb, 0, 0, temp.Width, temp.Height);
                }

                thumb = temp;
            } while (newSize != (isWidthGreater ? width : height));

            return thumb;
        }

        public static Bitmap CreateThumbnail(Bitmap image, int newWidth, int newHeight)
        {
            int width = image.Width;
            int height = image.Height;

            if (newWidth >= width || newHeight >= height)
            {
                throw new ArgumentException("newWidth and newHeight cannot be greater than the image dimensions");
            }
            else if (newWidth <= 0 || newHeight <= 0)
            {
                throw new ArgumentException("newWidth and newHeight must be greater than 0");
            }

            Bitmap thumb = image;

            do
            {
                if (width > newWidth)
                {
                    width /= 2;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }

                if (height > newHeight)
                {
                    height /= 2;
                    if (height < newHeight)
                    {
                        height = newHeight;
                    }
                }

                Bitmap temp = CreateCompatibleImage(image, width, height);

                using (Graphics g2 = Graphics.FromImage(temp))
                {
                    g2.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g2.DrawImage(thumb, 0, 0, temp.Width, temp.Height);
                }

                thumb = temp;
            } while (width != newWidth || height != newHeight);

            return thumb;
        }

        public static int[] GetPixels(Bitmap img, int x, int y, int w, int h, int[] pixels)
        {
            if (w == 0 || h == 0)
            {
                return new int[0];
            }

            if (pixels == null)
            {
                pixels = new int[w * h];
            }
            else if (pixels.Length < w * h)
            {
                throw new ArgumentException("pixels array must have a length >= w*h");
            }

            PixelFormat imageFormat = img.PixelFormat;

            if (imageFormat == PixelFormat.Format32bppArgb || imageFormat == PixelFormat.Format32bppRgb)
            {
                BitmapData data = img.LockBits(new Rectangle(x, y, w, h), ImageLockMode.ReadOnly, imageFormat);
                Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
                img.UnlockBits(data);
            }
            else
            {
                BitmapData data = img.LockBits(new Rectangle(x, y, w, h), ImageLockMode.WriteOnly, imageFormat);
                int bytesPerPixel = 3; // 1 byte cho mỗi thành phần màu (R, G, B)
                int stride = data.Stride; // Số byte trong mỗi dòng của dữ liệu pixel

                for (int row = 0; row < h; row++)
                {
                    for (int col = 0; col < w; col++)
                    {
                        int dataIndex = row * stride + col * bytesPerPixel;
                        int pixelValue = pixels[row * w + col];
                        byte blue = (byte)(pixelValue & 0xFF);
                        byte green = (byte)((pixelValue >> 8) & 0xFF);
                        byte red = (byte)((pixelValue >> 16) & 0xFF);
                        Marshal.WriteByte(data.Scan0, dataIndex, blue);
                        Marshal.WriteByte(data.Scan0, dataIndex + 1, green);
                        Marshal.WriteByte(data.Scan0, dataIndex + 2, red);
                    }
                }
                img.UnlockBits(data);
            }

            return pixels;
        }


        public static void SetPixels(Bitmap img, int x, int y, int w, int h, int[] pixels)
        {
            if (pixels == null || w == 0 || h == 0)
            {
                return;
            }
            else if (pixels.Length < w * h)
            {
                throw new ArgumentException("pixels array must have a length >= w*h");
            }

            PixelFormat imageFormat = img.PixelFormat;

            if (imageFormat == PixelFormat.Format32bppArgb || imageFormat == PixelFormat.Format32bppRgb)
            {
                BitmapData data = img.LockBits(new Rectangle(x, y, w, h), ImageLockMode.WriteOnly, imageFormat);
                Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
                img.UnlockBits(data);
            }
            else
            {
                BitmapData data = img.LockBits(new Rectangle(x, y, w, h), ImageLockMode.WriteOnly, imageFormat);
                int bytesPerPixel = 3; // 1 byte cho mỗi thành phần màu (R, G, B)
                int stride = data.Stride; // Số byte trong mỗi dòng của dữ liệu pixel

                for (int row = 0; row < h; row++)
                {
                    for (int col = 0; col < w; col++)
                    {
                        int dataIndex = row * stride + col * bytesPerPixel;
                        int pixelValue = pixels[row * w + col];
                        byte blue = (byte)(pixelValue & 0xFF);
                        byte green = (byte)((pixelValue >> 8) & 0xFF);
                        byte red = (byte)((pixelValue >> 16) & 0xFF);
                        Marshal.WriteByte(data.Scan0, dataIndex, blue);
                        Marshal.WriteByte(data.Scan0, dataIndex + 1, green);
                        Marshal.WriteByte(data.Scan0, dataIndex + 2, red);
                    }
                }
                img.UnlockBits(data);
            }
        }
    }
}
