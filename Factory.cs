using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace Game
{
    abstract public class Factory
    {
        void generate(XmlDocument config)
        {

        }
        protected static string rs(XmlNode n, string s="")
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return n.Value;
                }
            }
            return s;
        }
        protected static int ri(XmlNode n, int def = 0)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Int32.Parse(n.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return def;
        }
        protected static float rf(XmlNode n, float def = 0.0f)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Single.Parse(n.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return def;
        }
    }
}
