using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace Game
{
    public class Resource
    {
        private static Dictionary<String, XmlDocument> xmls = new Dictionary<string, XmlDocument>();
        private static Dictionary<String, Engine.Bitmap> btms = new Dictionary<string, Engine.Bitmap>();
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
        public static Engine.Bitmap getBitmap(String path)
        {
            if (!btms.ContainsKey(path))
            {
                Console.WriteLine("Load bitmap resource " + path);
                Engine.Bitmap b = new Engine.Bitmap();
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
