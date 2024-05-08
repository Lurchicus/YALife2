using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace YALife
{
    /// <summary>
    /// Creates a direct access disposable bitmap that is much faster than
    /// the standard bitmap as we don't need to waste time with locking.
    ///
    /// DirectBitmap by A.Konzel (https://stackoverflow.com/users/3117338/a-konzel) via StackOverflow 
    /// (https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f)
    /// 
    /// Note: All I've done is add comments to the original code authored by A.Konzel that was posted on StackOverflow.com
    ///  
    /// The speed increase realized by using this class is phenominal compared to graphic.bitmap.setpixel()
    /// </summary>
    public class DirectBitmap : IDisposable
    {
        /// <summary>
        /// Our directly accesable bitmap
        /// </summary>
        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Expresses the bitmap as an array of 32 bit integers
        /// </summary>
        public Int32[] Bits { get; private set; }

        /// <summary>
        /// Flag to show if the bitmap is disposed or not
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Our height
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Our width
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Handle to pinned bitmap in memory
        /// </summary>
        protected GCHandle BitsHandle { get; private set; }

        /// <summary>
        /// Constructor to define and create our directly accessable bitmap 
        /// </summary>
        /// <param name="width">Bitmap width</param>
        /// <param name="height">Bitmap height</param>
        public DirectBitmap(int width, int height)
        {
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            //YALife.Paper = Bitmap;
        }

        /// <summary>
        /// Set the pixel at x by y to color
        /// </summary>
        /// <param name="x">Width in pixels</param>
        /// <param name="y">Height in pixels</param>
        /// <param name="colour">32 bit argb color</param>
        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int RGB = colour.ToArgb();

            Bits[index] = RGB;
        }

        /// <summary>
        /// Return the color of the pixel in x by y of the bitmap
        /// </summary>
        /// <param name="x">Pixel column of interest</param>
        /// <param name="y">Pixel row of interest</param>
        /// <returns>32 bit argb color</returns>
        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int RGB = Bits[index];
            Color result = Color.FromArgb(RGB);

            return result;
        }

        /// <summary>
        /// Dispose of our bitmap
        /// </summary>
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}