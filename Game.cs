using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class Game
    {
        public static GraphicCore core;
        public static Stage stage;
        public static Interface interfaceView;
        public static Background background;
        public static Camera camera;
        public static Mouse mouse;
        public static Keyboard keyboard;
        public static Random random;
        private StateContext controller;
        //private static System.Object lockThis = new System.Object();
        //public static bool DEBUG = true;
        public Game()
        {
            random = new Random();
            core = GraphicCore.getInstance();
            mouse = Mouse.getInstance();
            keyboard = Keyboard.getInstance();
            background = Background.getInstance();
            camera = new Camera();
            stage = Stage.getInstance();
            interfaceView = Interface.getInstance();
            controller = new StateContext();
            //core.form.Text = "Angels' Hunter"; // �� ������ ���: ���������� ���� ��������� ��-�� ������ �����!!!
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          