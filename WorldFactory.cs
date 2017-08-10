using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using Astronomy;
using Game.Astronomy;
using Game.Astronomy.Objects;

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
                            SpaceFactory.generate(GCore.Resource.getXml(table.Attributes["src"].Value));
                        }break;
                    case "components":
                        {
                            ComponentsFactory.generate(GCore.Resource.getXml(table.Attributes["src"].Value));
                        }break;
                }
            }
        }
    }
}
