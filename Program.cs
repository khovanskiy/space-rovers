using System;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Windows;
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
            var brush = new SolidColorBrush(core.render2d, Color.Gold);
            RenderLoop.Run(core.form, () =>
            {
                if (core.running)
                {
                    core.render2d.BeginDraw();
                    core.render2d.Clear(Color.Black);
                    core.render(Game.background, RenderType.BACKGROUND);
                    core.render(Game.stage, RenderType.STAGE);
                    core.render(Game.interfaceView, RenderType.INTERFACE);
                    core.render2d.EndDraw();
                    core.swapChain.Present(0, PresentFlags.None);
                }
            });
            core.Dispose();
        }
    }
}