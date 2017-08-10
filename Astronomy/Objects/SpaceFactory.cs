using System;
using System.Xml;
using Astronomy;

namespace Game.Astronomy.Objects
{
    public class SpaceFactory : Factory
    {
        public static void generate(XmlDocument config)
        {
            SpaceObject space = new SpaceObject("Вселенная", 0, null, new RelativeMovement());
            RegistrySystem.Registry.getInstance().addElement(space, "universe");
            foreach (XmlNode table in config.DocumentElement)
            {
                String id = rs(table.Attributes["id"]);
                String name = rs(table.Attributes["name"]);
                PlanetarySystem ps = new PlanetarySystem(name);
                RegistrySystem.Registry.getInstance().addElement(ps, id);
                f(table, ps);
            }
        }

        private static float rf(XmlNode n)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Single.Parse(n.Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            return 0.0f;
        }

        private static bool rb(XmlNode n)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return Boolean.Parse(n.Value);
                }
            }
            return false;
        }

        private static string rs(XmlNode n)
        {
            if (n != null)
            {
                if (n.Value != "")
                {
                    return n.Value;
                }
            }
            return "DATA\\Planets\\anim.png";
        }

        private static void f(XmlNode xml, RegistrySystem.RegistryObject parent)
        {
            foreach (XmlNode table in xml)
            {
                string name = "";
                string id = "";
                if (table.Attributes["id"] != null)
                {
                    id = table.Attributes["id"].Value;
                }
                if (table.Attributes["name"] != null)
                {
                    name = table.Attributes["name"].Value;
                }

                RelativeMovement rm = new RelativeMovement();
                rm.radius = rf(table.Attributes["radius"]) * 400;
                rm.ell_angle = rf(table.Attributes["ell_angle"]);
                rm.eccentr = rf(table.Attributes["eccentr"]);
                rm.ang_v = rf(table.Attributes["ang_v"]) / 8;
                rm.orb_angle = rf(table.Attributes["orb_angle"]);
                rm.precession = rf(table.Attributes["precession"]);
                rm.spin_speed = rf(table.Attributes["spin_speed"]);
                rm.spin_angle = rf(table.Attributes["spin_angle"]);
                rm.hor_axis = rf(table.Attributes["hor_axis"]);

                rm.ell_cw = rb(table.Attributes["ell_cw"]);
                rm.spin_cw = rb(table.Attributes["spin_cw"]);
                rm.prec_cw = rb(table.Attributes["prec_cw"]);

                SpaceObject obj = new SpaceObject(name, 0, parent.my_id, rm);
                obj.size = rf(table.Attributes["size"]) / 16;
                //if (obj.size == 0) obj.size = 1;
                obj.src = rs(table.Attributes["src"]);
                RegistrySystem.Registry.getInstance().addElement(obj, id);
                switch (table.Name)
                {
                    case "solid":
                    {
                        obj.setType(SpaceObject.Solid_Body);
                    }
                        break;
                    case "star":
                    {
                        obj.setType(SpaceObject.Star);
                    }
                        break;
                    case "asteroid":
                    {
                        obj.setType(SpaceObject.Asteroid);
                    }
                        break;
                    case "system":
                    {
                        obj.setType(SpaceObject.SpaceSystem);
                        f(table, obj);
                    }
                        break;
                }
            }
        }
    }
}