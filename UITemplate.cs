using System;
using System.Collections.Generic;
using System.Xml;

namespace Game
{
    public class UITemplate : GCore.EventDispatcher
    {
        public GCore.Sprite layout = new GCore.Sprite();
        private GCore.ListenersManager keeper;

        private void f(GCore.Sprite parent, XmlNode node)
        {
            //Console.WriteLine("Function f " + node.Name);
            foreach (XmlNode table in node.ChildNodes)
            {
                float x = rf(table.Attributes["x"]);
                float y = rf(table.Attributes["y"]);
                float width = rf(table.Attributes["width"]);
                float height = rf(table.Attributes["height"]);
                switch (table.Name)
                {
                    case "img":
                    {
                        GCore.Bitmap b = new GCore.Bitmap();
                        b.load(rs(table.Attributes["src"]));
                        b.x = x;
                        b.y = y;
                        b.ratio = rb(table.Attributes["ratio"], true);
                        if (width != 0 && height != 0)
                        {
                            b.width = width;
                            b.height = height;
                        }
                        keeper.add(parent, b);
                    }
                        break;
                    case "button":
                    {
                        UI.Button b = new UI.Button();
                        b.x = x;
                        b.y = y;
                        b.id = rs(table.Attributes["id"]);
                        List<string> imgs = new List<string>();
                        foreach (XmlNode img in table.ChildNodes)
                        {
                            imgs.Add(img.Attributes["src"].Value);
                        }
                        b.load(imgs.ToArray());
                        keeper.add(b, GCore.MouseEvent.CLICK, onClick);
                        keeper.add(parent, b);
                    }
                        break;
                    case "movieclip":
                    {
                        GCore.MovieClip m = new GCore.MovieClip();
                        m.x = x;
                        m.y = y;
                        m.frameRate = 1; // ri(table.Attributes["frameRate"], 20);
                        List<string> imgs = new List<string>();
                        foreach (XmlNode img in table.ChildNodes)
                        {
                            imgs.Add(img.Attributes["src"].Value);
                        }
                        m.load(imgs.ToArray());
                        keeper.add(parent, m);
                    }
                        break;
                    case "textfield":
                    {
                        String value = table.Attributes["value"].Value;
                        GCore.TextField tf = new GCore.TextField(value);
                        tf.x = x;
                        tf.y = y;
                        if (table.Attributes["size"] != null)
                        {
                            int size = Int32.Parse(table.Attributes["size"].Value);
                            tf.size = size;
                        }
                        keeper.add(parent, tf);
                    }
                        break;
                    case "plane":
                    {
                        GCore.Sprite sp = new GCore.Sprite();
                        sp.x = x;
                        sp.y = y;
                        keeper.add(parent, sp);
                        f(sp, table);
                    }
                        break;
                }
            }
        }

        protected static int ri(XmlNode n, int d = 0)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Int32.Parse(n.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return d;
        }

        protected static float rf(XmlNode n, float d = 0.0f)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Single.Parse(n.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return d;
        }

        protected static bool rb(XmlNode n, bool d = false)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Boolean.Parse(n.Value);
                }
            }
            return d;
        }

        protected static string rs(XmlNode n)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return n.Value;
                }
            }
            return "";
        }

        public UITemplate(XmlDocument xmlDoc)
        {
            keeper = new GCore.ListenersManager();
            f(layout, xmlDoc.DocumentElement);
        }

        public void clear()
        {
            layout.clear();
            layout = new GCore.Sprite();
        }

        private void onClick(GCore.Event e)
        {
            dispatchEvent(e);
        }
    }
}