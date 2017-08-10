using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using Game.GCore;

namespace Game
{
    public class Resource
    {
        private static Dictionary<String, XmlDocument> xmls = new Dictionary<string, XmlDocument>();
        private static Dictionary<String, Bitmap> btms = new Dictionary<string, Bitmap>();
        private Resource()
        {
        }
        public static XmlDocument getXml(String path)
        {
            if (!xmls.ContainsKey(path))
            {
                Console.WriteLine("Load xml resource " + path);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                xmls[path] = xmlDoc;
                return xmlDoc;
            }
            else
            {
                Console.WriteLine("Get xml resource " + path);
                return xmls[path];
            }
        }
        public static Bitmap getBitmap(String path)
        {
            if (!btms.ContainsKey(path))
            {
                Console.WriteLine("Load bitmap resource " + path);
                Bitmap b = new Bitmap();
                b.loadFromFile(path);
                btms[path] = b;
                return b;
            }
            else
            {
                Console.WriteLine("Get bitmap resource " + path);
                return btms[path];
            }
        }
    }
}
