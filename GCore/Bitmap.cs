using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

namespace Game.GCore
{
    public class Bitmap : DisplayObject, IDisposable, ISource
    {
        private SharpDX.Direct2D1.Bitmap source;
        public bool self_control = true;

        ~Bitmap()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (self_control)
            {
                source.Dispose();
            }
        }

        public void load(String path)
        {
            source = Resource.getBitmap(path).getSource();
            self_control = false;
            _width = source.PixelSize.Width;
            _height = source.PixelSize.Height;
            _isLoaded = true;
            dispatchEvent(new Event(this, Event.COMPLETE));
        }

        /*public void loadFromResource(System.Drawing.Bitmap bitmap)
        {
            var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));
            var size = new System.Drawing.Size(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
            int stride = bitmap.Width * sizeof(int);
            using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
            {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }

                    }
                    bitmap.UnlockBits(bitmapData);
                    _width = bitmap.Width;
                    _height = bitmap.Height;
                    tempStream.Position = 0;
                    GraphicCore core = GraphicCore.getInstance();
                    source = new SharpDX.Direct2D1.Bitmap(core.render2d, size, tempStream, stride, bitmapProperties);

            }
            _isLoaded = true;
            dispatchEvent(new Event(this, Event.COMPLETE));
        }*/
        public void loadFromFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new Exception("Не найден файл");
            }
            using (var bitmap = (System.Drawing.Bitmap) Image.FromFile(file))
            {
                var sourceArea = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties =
                    new BitmapProperties(new PixelFormat(Format.B8G8R8A8_UNorm,
                        AlphaMode.Premultiplied));
                var size = new Size(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }
                    }
                    bitmap.UnlockBits(bitmapData);
                    _width = bitmap.Width;
                    _height = bitmap.Height;
                    tempStream.Position = 0;
                    GraphicCore core = GraphicCore.getInstance();
                    source = new SharpDX.Direct2D1.Bitmap(core.render2d, size, tempStream, stride, bitmapProperties);
                }
            }
            _isLoaded = true;
            dispatchEvent(new Event(this, Event.COMPLETE));
        }

        public SharpDX.Direct2D1.Bitmap getSource()
        {
            return source;
        }
    }
}