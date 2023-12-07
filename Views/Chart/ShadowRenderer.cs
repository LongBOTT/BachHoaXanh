using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Views.Chart
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class ShadowRenderer
    {
        private int size = 5;
        private float opacity = 0.5f;
        private Color color = Color.Black;

        public ShadowRenderer()
        {
            this.size = 5;
            this.opacity = 0.5f;
            this.color = Color.Black;
        }

        public ShadowRenderer(int size, float opacity, Color color)
        {
            this.size = size;
            this.opacity = opacity;
            this.color = color;
        }

        public Color GetColor()
        {
            return color;
        }

        public float GetOpacity()
        {
            return opacity;
        }

        public int GetSize()
        {
            return size;
        }

        public Bitmap CreateShadow(Bitmap image)
        {
            int shadowSize = size * 2;
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            int dstWidth = srcWidth + shadowSize;
            int dstHeight = srcHeight + shadowSize;
            int left = size;
            int right = shadowSize - left;
            int yStop = dstHeight - right;
            int shadowRgb = color.ToArgb() & 0x00FFFFFF;
            int[] aHistory = new int[shadowSize];
            int historyIdx;
            int aSum;
            Bitmap dst = new Bitmap(dstWidth, dstHeight);
            int[] dstBuffer = new int[dstWidth * dstHeight];
            int[] srcBuffer = new int[srcWidth * srcHeight];
            GraphicsUtilities.GetPixels(image, 0, 0, srcWidth, srcHeight, srcBuffer);
            int lastPixelOffset = right * dstWidth;
            float hSumDivider = 1.0f / shadowSize;
            float vSumDivider = opacity / shadowSize;
            int[] hSumLookup = new int[256 * shadowSize];
            for (int i = 0; i < hSumLookup.Length; i++)
            {
                hSumLookup[i] = (int)(i * hSumDivider);
            }
            int[] vSumLookup = new int[256 * shadowSize];
            for (int i = 0; i < vSumLookup.Length; i++)
            {
                vSumLookup[i] = (int)(i * vSumDivider);
            }
            int srcOffset;
            for (int srcY = 0, dstOffset = left * dstWidth; srcY < srcHeight; srcY++)
            {
                for (historyIdx = 0; historyIdx < shadowSize;)
                {
                    aHistory[historyIdx++] = 0;
                }
                aSum = 0;
                historyIdx = 0;
                srcOffset = srcY * srcWidth;
                for (int srcX = 0; srcX < srcWidth; srcX++)
                {
                    int a = hSumLookup[aSum];
                    dstBuffer[dstOffset++] = a << 24;
                    aSum -= aHistory[historyIdx];
                    a = srcBuffer[srcOffset + srcX] >> 24;
                    aHistory[historyIdx] = a;
                    aSum += a;
                    if (++historyIdx >= shadowSize)
                    {
                        historyIdx -= shadowSize;
                    }
                }
                for (int i = 0; i < shadowSize; i++)
                {
                    int a = hSumLookup[aSum];
                    dstBuffer[dstOffset++] = a << 24;
                    aSum -= aHistory[historyIdx];
                    if (++historyIdx >= shadowSize)
                    {
                        historyIdx -= shadowSize;
                    }
                }
            }
            for (int x = 0, bufferOffset = 0; x < dstWidth; x++, bufferOffset = x)
            {
                aSum = 0;
                for (historyIdx = 0; historyIdx < left;)
                {
                    aHistory[historyIdx++] = 0;
                }
                for (int y = 0; y < right; y++, bufferOffset += dstWidth)
                {
                    int a = dstBuffer[bufferOffset] >> 24;
                    aHistory[historyIdx++] = a;
                    aSum += a;
                }
                bufferOffset = x;
                historyIdx = 0;
                for (int y = 0; y < yStop; y++, bufferOffset += dstWidth)
                {
                    int a = vSumLookup[aSum];
                    dstBuffer[bufferOffset] = a << 24 | shadowRgb;
                    aSum -= aHistory[historyIdx];
                    a = dstBuffer[bufferOffset + lastPixelOffset] >> 24;
                    aHistory[historyIdx] = a;
                    aSum += a;
                    if (++historyIdx >= shadowSize)
                    {
                        historyIdx -= shadowSize;
                    }
                }
                for (int y = yStop; y < dstHeight; y++, bufferOffset += dstWidth)
                {
                    int a = vSumLookup[aSum];
                    dstBuffer[bufferOffset] = a << 24 | shadowRgb;
                    aSum -= aHistory[historyIdx];
                    if (++historyIdx >= shadowSize)
                    {
                        historyIdx -= shadowSize;
                    }
                }
            }
            GraphicsUtilities.SetPixels(dst, 0, 0, dstWidth, dstHeight, dstBuffer);
            return dst;
        }
    }

    public static class GraphicsUtilities
    {
        public static void GetPixels(Bitmap image, int x, int y, int width, int height, int[] pixels)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            BitmapData data = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(data.Scan0, pixels, 0, pixels.Length);
            image.UnlockBits(data);
        }

        public static void SetPixels(Bitmap image, int x, int y, int width, int height, int[] pixels)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            BitmapData data = image.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
            image.UnlockBits(data);
        }
    }

}
