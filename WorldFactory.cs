using System.Xml;
using Astronomy;

namespace Game
{
    public class WorldFactory
    {
        XmlDocument xml;

        public WorldFactory(XmlDocument xml)
        {
            this.xml = xml;
        }

        public void generate()
        {
            foreach (XmlNode table in xml.DocumentElement.ChildNodes)
            {
                switch (table.Name)
                {
                    case "space":
                    {
                        SpaceObject space = SpaceFactory.create(GCore.Resource.getXml(table.Attributes["src"].Value));
                    }
                        break;
                }
            }
        }
    }
}