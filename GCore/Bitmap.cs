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
        private class Loader : EventDispatcher, Runnable
        {
            public String path = "";
            private SharpDX.Direct2D1.Bitmap data;
            private Object lockObject;

            public Loader(String path)
            {
                this.path = path;
                lockObject = new Object();
            }

            public void run()
            {
                lock (lockObject)
                {
                    data = getSource(path);
                }
                dispatchEvent(new Event(this, Event.COMPLETE));
            }

            public SharpDX.Direct2D1.Bitmap getData()
            {
                return data;
            }
        }

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
                if (source != null)
                {
                    source.Dispose();
                }
            }
        }

        public void loadAsync(String path)
        {
            Loader loader = new Loader(path);
            loader.addEventListener(Event.COMPLETE, onAsyncLoaded);
            ThreadManager.addTask(loader);
        }

        public void onAsyncLoaded(Event e)
        {
            Loader loader = (Loader) e.target;
            source = loader.getData();
            _width = source.PixelSize.Width;
            _height = source.PixelSize.Height;
            _isLoaded = true;
            dispatchEvent(new Event(this, Event.COMPLETE));
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

        public static SharpDX.Direct2D1.Bitmap getSource(String path)
        {
            using (var bitmap = (System.Drawing.Bitmap) Image.FromFile(path))
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
                    tempStream.Position = 0;
                    GraphicCore core = GraphicCore.getInstance();
                    return new SharpDX.Direct2D1.Bitmap(core.render2d, size, tempStream, stride, bitmapProperties);
                }
            }
        }

        public void loadFromFile(string file)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine("Не найден файл " + file);
                throw new Exception("Не найден файл " + file);
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