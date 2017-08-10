using System;

namespace Game.GCore
{
    public class TextField : InteractiveObject
    {
        private String _text = "";
        private float _size;
        private uint _color = 0xFFFFFFFF;

        public TextField(String text)
        {
            this.text = text;
            size = 12;
            Mouse.getInstance().follow(Event.ADDED_TO_STAGE, this);
        }

        public uint color
        {
            get { return _color; }
            set
            {
                uint R = value >> 24;
                uint G = (value << 8) >> 24;
                uint B = (value << 16) >> 24;
                uint A = (value << 24) >> 24;
                _color = (A << 24) | (B << 16) | (G << 8) | R;
            }
        }

        override public float alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                _color = _color & 0x00FFFFFF | ((uint) (alpha * 0xFF) % 256) << 24;
            }
        }

        override public float width
        {
            get { return _width; }
            set { _width = value; }
        }

        override public float height
        {
            get { return _height; }
            set { _height = value; }
        }

        public float size
        {
            get { return _size; }
            set
            {
                _size = value;
                height = _size * (delCount(_text) + 1);
                width = _size * _text.Length;
            }
        }

        private int delCount(String s)
        {
            int c = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    c++;
                }
            }
            return c;
        }

        public String text
        {
            get { return _text; }
            set
            {
                _text = value;
                width = _size * _text.Length;
            }
        }
    }
}