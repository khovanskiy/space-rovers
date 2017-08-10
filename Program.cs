using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.RawInput;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SharpDX.Multimedia;
using System.Threading;

using Game.GCore;

namespace Game
{
    static class Program
    {
        public static GraphicCore core;
        private static System.Object lockThis = new System.Object();

        [STAThread]
        private static void run()
        {
            Game game = new Game();
        }
        private static void Main()
        {
            core = GraphicCore.getInstance();
            run();
            var brush=new SolidColorBrush(core.render2d,Color.Gold);
            RenderLoop.Run(core.form, () =>
            {
                if (core.running)
                {
                    core.render2d.BeginDraw();
                    core.render2d.Clear(Color.Black);
                    GraphicCore.MCOUNT = 0;
                    core.render(Game.background, RenderType.BACKGROUND);
                    core.render(Game.stage, RenderType.STAGE);
                    core.render(Game.interfaceView, RenderType.INTERFACE);
                    core.render2d.EndDraw();
                    GraphicCore.TOTAL_COUNT = GraphicCore.MCOUNT;
                    core.swapChain.Present(0, PresentFlags.None);
                }
            });
            core.Dispose();
        }
    }
}