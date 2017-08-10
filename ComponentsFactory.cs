using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

namespace Game
{
    public class ComponentsFactory : Factory
    {
        public static void generate(XmlDocument config)
        {
            foreach (XmlNode table in config.DocumentElement)
            {
                String id = rs(table.Attributes["id"]);
                int level = ri(table.Attributes["level"]);
                int type = ri(table.Attributes["type"]);
                switch (table.Name)
                {
                    case "cannon":
                        {
                            int damage = ri(table.Attributes["damage"]);
                            int radius = ri(table.Attributes["radius"]);
                            int rate = ri(table.Attributes["rate"]);
                            Ships.Cannon cannon = new Ships.Cannon();
                            cannon.damage = damage;
                            cannon.radius = radius;
                            cannon.rate = rate;
                            cannon.level = level;
                            cannon.type = type;
                            Economy.cannons.Add(cannon);
                            Registry.getInstance().addElement(cannon, id);
                        } break;
                    case "engine":
                        {
                            int power = ri(table.Attributes["power"]);
                            Ships.Engine engine = new Ships.Engine();
                            engine.power = power;
                            engine.level = level;
                            engine.type = type;
                            Economy.engines.Add(engine);
                            Registry.getInstance().addElement(engine, id);
                        } break;
                    case "hull":
                        {
                            float protection = rf(table.Attributes["protection"]);
                            int cannons = ri(table.Attributes["cannons"]);
                            int slots = ri(table.Attributes["slots"]);
                            Ships.Hull hull = new Ships.Hull();
                            hull.protection = protection;
                            hull.number_of_cannons = cannons;
                            hull.number_of_free_slots = slots;
                            hull.level = level;
                            hull.type = type;
                            Economy.hulls.Add(hull);
                            Registry.getInstance().addElement(hull, id);
                        } break;
                }
            }
        }
    }
}
