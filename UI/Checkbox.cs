using System;


namespace UI
{
    class Checkbox : Game.GCore.MovieClip
    {
        bool _isChecked = false;

        public Checkbox()
        {
            this.load(new string[] {"L:\\1\\UI\\Checkbox\\off.png", "L:\\1\\UI\\Checkbox\\on.png"});
            off();
            Game.GCore.Mouse.getInstance().addEventListener(Game.GCore.MouseEvent.CLICK, handleEvent);
        }

        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                if (value)
                {
                    gotoAndStop(0);
                }
                else
                {
                    gotoAndStop(1);
                }
            }
        }

        protected override void handleEvent(Game.GCore.Event e)
        {
            if (e.type == Game.GCore.MouseEvent.CLICK)
            {
                if (hitTestPoint(((Game.GCore.MouseEvent) e).localX, ((Game.GCore.MouseEvent) e).localY))
                {
                    e = (Game.GCore.MouseEvent) e;
                    e.target = this;
                    dispatchEvent(e);
                    Console.WriteLine("Click to checkbox");
                }
            }
        }

        public void on()
        {
            isChecked = true;
        }

        public void off()
        {
            isChecked = false;
        }
    }
}