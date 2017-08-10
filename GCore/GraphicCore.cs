using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Collections.Generic;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device1 = SharpDX.Direct3D10.Device1;
using DriverType = SharpDX.Direct3D10.DriverType;

namespace Game.GCore
{
    public enum RenderType
    {
        STAGE = 1,
        INTERFACE = 2,
        BACKGROUND = 3
    }

    public class GraphicCore : EventDispatcher, IDisposable
    {
        public RenderTarget render2d;
        public RenderForm form;
        public readonly static int MAX_DEPTH = 20;
        private Device1 device;
        public SwapChain swapChain;
        private SharpDX.DXGI.Factory factoryDX;
        private SharpDX.Direct2D1.Factory factory2d;
        private SharpDX.DirectWrite.Factory factoryText;
        private SolidColorBrush brush;
        static GraphicCore _instance = null;
        private bool _isRunning = false;
        public readonly int frame_rate = 30;
        public static int MCOUNT = 0;
        public static int TOTAL_COUNT = 0;
        public static bool DEBUG = false;

        private GraphicCore()
        {
            form = new RenderForm("");
            form.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
            form.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            form.ControlBox = false;
            form.MaximizeBox = false;
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                    new ModeDescription(form.Width, form.Height, new Rational(frame_rate, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            Camera.width = form.Width;
            Camera.height = form.Height;

            Device1.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, desc,
                SharpDX.Direct3D10.FeatureLevel.Level_10_0, out device, out swapChain);

            factory2d = new SharpDX.Direct2D1.Factory();
            factoryText = new SharpDX.DirectWrite.Factory();

            // Ignore all windows events
            factoryDX = swapChain.GetParent<SharpDX.DXGI.Factory>();
            //factoryDX.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            var renderView = new RenderTargetView(device, backBuffer);

            Surface surface = backBuffer.QueryInterface<Surface>();
            this.render2d = new RenderTarget(factory2d, surface,
                new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));

            brush = new SolidColorBrush(this.render2d, Color.White);
            _isRunning = true;
        }

        public bool running
        {
            get { return _isRunning; }
        }

        private bool inView(RectangleF r)
        {
            return (r.Top <= Camera.height && r.Bottom >= 0 && r.Left <= Camera.width && r.Right >= 0);
        }

        public void render(DisplayObjectContainer child, RenderType type, int k = 0)
        {
            dispatchEvent(new Event(this, Event.ENTER_FRAME));
            if (k > MAX_DEPTH) return;
            List<DisplayObject> list = child.getChildrenList();
            for (int i = 0; i < list.Count; i++)
            {
                DisplayObject d = list[i];
                if (d.visible)
                {
                    if (d is ISource)
                    {
                        if (d.isLoaded)
                        {
                            SharpDX.Direct2D1.Bitmap b = ((ISource) d).getSource();
                            float alpha = d.alpha;
                            bool needRender = true;
                            RectangleF rf = d.getRenderBounds();
                            needRender = inView(rf);
                            if (needRender)
                            {
                                this.render2d.Transform = d.getRenderMatrix();
                                this.render2d.DrawBitmap(b, alpha, BitmapInterpolationMode.NearestNeighbor);
                                if (DEBUG)
                                {
                                    this.render2d.Transform = SharpDX.Matrix.Identity;
                                    brush.Color = Color.GreenYellow;
                                    this.render2d.DrawRectangle(rf, brush);

                                    brush.Color = Color.Yellow;
                                    Point p = d.globalToLocal(d.rotationPoint);
                                    Ellipse c = new Ellipse(new DrawingPointF(p.x, p.y), 5, 5);
                                    this.render2d.DrawEllipse(c, brush);
                                }
                            }
                        }
                    }
                    else if (d is TextField)
                    {
                        GCore.TextField tf = (GCore.TextField) d;

                        if (DEBUG)
                        {
                            this.render2d.Transform = SharpDX.Matrix.Identity;
                            brush.Color = Color.Aquamarine;
                            this.render2d.DrawRectangle(d.getRenderBounds(), brush);
                        }

                        this.render2d.Transform = d.getRenderMatrix();
                        var format = new SharpDX.DirectWrite.TextFormat(factoryText, "Arial", tf.size);
                        brush.Color = new Color4(tf.color);
                        this.render2d.DrawText(tf.text, format, new RectangleF(0, 0, tf.size * tf.text.Length, 0),
                            brush);
                        format.Dispose();
                    }
                    else if (d is DisplayObjectContainer)
                    {
                        this.render((DisplayObjectContainer) d, type, k + 1);
                        if (DEBUG)
                        {
                            this.render2d.Transform = SharpDX.Matrix.Identity;
                            brush.Color = Color.Red;
                            this.render2d.DrawRectangle(d.getRenderBounds(), brush);

                            brush.Color = Color.IndianRed;
                            //Point p = d.globalToLocal(d.rotationPoint);
                            Point p = new Point(d.x, d.y);
                            Ellipse c = new Ellipse(new DrawingPointF(p.x, p.y), 5, 5);
                            this.render2d.DrawEllipse(c, brush);
                        }
                    }
                }
            }
        }

        public bool check(DisplayObjectContainer child, Dictionary<int, bool> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<int, bool>();
            }
            bool ok = true;
            List<DisplayObject> list = child.getChildrenList();
            foreach (DisplayObject d in list)
            {
                if (!dic.ContainsKey(d.GetHashCode()))
                {
                    dic.Add(d.GetHashCode(), true);
                }
                else
                {
                    return false;
                }
                if (d is DisplayObjectContainer)
                {
                    if (!check((DisplayObjectContainer) d, dic))
                    {
                        ok = false;
                        break;
                    }
                }
            }
            return ok;
        }

        public void Dispose()
        {
            _isRunning = false;
            render2d.Dispose();
            device.Dispose();
            swapChain.Dispose();
            factory2d.Dispose();
            factoryText.Dispose();
            factoryDX.Dispose();
            brush.Dispose();
        }

        public static GraphicCore getInstance()
        {
            if (_instance == null)
            {
                _instance = new GraphicCore();
            }
            return _instance;
        }
    }
}