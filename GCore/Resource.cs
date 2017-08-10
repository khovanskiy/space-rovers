using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace Game.GCore
{
    public class Resource
    {
        private static Dictionary<String, XmlDocument> xmls = new Dictionary<string, XmlDocument>();
        private static Dictionary<String, GCore.Bitmap> btms = new Dictionary<string, GCore.Bitmap>();
        private Resource()
        {
        }
        public static XmlDocument getXml(String path)
        {
            if (!xmls.ContainsKey(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                xmls[path] = xmlDoc;
                return xmlDoc;
            }
            else
            {
                return xmls[path];
            }
        }
        public static Bitmap getBitmap(String path)
        {
            if (!btms.ContainsKey(path))
            {
                GCore.Bitmap b = new GCore.Bitmap();
                b.loadFromFile(path);
                btms[path] = b;
                return b;
            }
            else
            {
                return btms[path];
            }
        }
    }
}
