using System;

namespace Game.GCore
{
    public class MovieClip : InteractiveObject, IDisposable, ISource
    {
        private int _frameRate = 2;
        private int _currentFrame = 0;
        private int _totalFrames = 0;
        private GCore.Bitmap[] frames = null;
        private bool _isPlaying = true;
        private int _loadedFrames = 0;
        private int _k = 0;
        private bool isHovered = false;

        public MovieClip()
        {
            Mouse.getInstance().follow(Event.ADDED_TO_STAGE, this);
        }

        ~MovieClip()
        {
            Dispose();
        }

        public void Dispose()
        {
            dispatchEvent(new Event(this, Event.REMOVED_FROM_STAGE));
            for (int i = 0; i < _totalFrames; i++)
            {
                frames[i].Dispose();
            }
        }

        public void load(String[] files)
        {
            _totalFrames = files.Length;
            frames = new GCore.Bitmap[_totalFrames];
            for (int i = 0; i < _totalFrames; i++)
            {
                var frame = new Bitmap();
                frames[i] = frame;
                frame.addEventListener(Event.COMPLETE, handleEvent);
                frame.load(files[i]);
            }
        }

        virtual protected void onMouseMove(Event e)
        {
        }

        virtual protected void onMouseOver(Event e)
        {
        }

        virtual protected void onMouseOut(Event e)
        {
        }

        override protected void handleEvent(Event e)
        {
            if (e.type == Event.COMPLETE)
            {
                _width = Math.Max(_width, frames[0].getSource().PixelSize.Width);
                _height = Math.Max(_height, frames[0].getSource().PixelSize.Height);
                e.target.removeEventListener(Event.COMPLETE, handleEvent);
                _loadedFrames++;
                if (_loadedFrames == _totalFrames)
                {
                    _isLoaded = true;
                    dispatchEvent(new Event(this, Event.COMPLETE));
                }
            }
            else if (e.type == MouseEvent.MOUSE_MOVE)
            {
                onMouseMove(e);
                bool b = this.hitTestPoint(((MouseEvent) e).localX, ((MouseEvent) e).localY);
                if (isHovered)
                {
                    if (!b)
                    {
                        onMouseOut(e);
                        isHovered = !isHovered;
                    }
                }
                else
                {
                    if (b)
                    {
                        onMouseOver(e);
                        isHovered = !isHovered;
                    }
                }
            }
            if (e is MouseEvent)
            {
                if (this.hitTestPoint(((MouseEvent) e).localX, ((MouseEvent) e).localY))
                {
                    MouseEvent ev = (MouseEvent) e;
                    dispatchEvent(new MouseEvent(this, ev.type, ev.localX, ev.localY, false, ev.delta));
                }
            }
        }

        public void play()
        {
            _isPlaying = true;
        }

        public void stop()
        {
            _isPlaying = false;
        }

        public void gotoAndPlay(int frame)
        {
            if (frame < _totalFrames && frame >= 0)
            {
                _currentFrame = frame;
            }
            play();
        }

        public SharpDX.Direct2D1.Bitmap getSource()
        {
            if (_isPlaying)
            {
                _k++;
                if (_k == _frameRate)
                {
                    _k = 0;
                    this.nextFrame();
                }
            }
            return frames[_currentFrame].getSource();
        }

        public void nextFrame()
        {
            _currentFrame++;
            if (_currentFrame == _totalFrames)
            {
                _currentFrame = 0;
            }
        }

        public void prevFrame()
        {
            _currentFrame--;
            if (_currentFrame == 0)
            {
                _currentFrame = _totalFrames - 1;
            }
        }

        public void gotoAndStop(int frame)
        {
            if (frame < _totalFrames && frame >= 0)
            {
                _currentFrame = frame;
            }
            stop();
        }

        public void gotoAndPlay(String label)
        {
            play();
        }

        public void gotoAndStop(String label)
        {
            stop();
        }

        public bool isPlaying
        {
            get { return _isPlaying; }
        }

        public int totalFrames
        {
            get { return _totalFrames; }
        }

        public int currentFrame
        {
            get { return _currentFrame; }
        }

        public int frameRate
        {
            get { return _frameRate * GraphicCore.getInstance().frame_rate; }
            set { _frameRate = (int) (GraphicCore.getInstance().frame_rate / value); }
        }
    }
}