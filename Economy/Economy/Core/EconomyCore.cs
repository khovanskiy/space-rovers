using System;
using System.Collections.Generic;
using Engine.Economy.PlanetEconomies;
using Engine.Economy.PlanetEconomies.Buildings;
using Engine.Economy.PlanetEconomies.Buildings.Actions;
using Engine.Economy.PlanetEconomies.Shops;
using Engine.Economy.PlanetEconomies.Shops.Objects;
using Engine.Economy.ERegistry;
using Engine.RegistrySystem.Registry;
using Engine.RegistrySystem.Objects;
using Engine.Astronomy.Objects;

namespace Engine.Economy.Core
{
    public static class EconomyCore
    {
        private const float sd = -1000;
        private const float sa = 1000;
        private const float eps = 1e-9f;

        public static void NextEconomyTurn(PlanetEconomy PE, float dt)
        {
            EconomyRegistry ER = EconomyRegistry.getInstance();
            float s = SafetyCalculation(PE, ER);
            float se = Math.Max(Math.Min(s, sa), sd);
            float ke = (se - sd) / (sa - sd);
            float kw = 1 - ke;
            TaxCollection(PE, ke, ER);
            ResourceInvestment();
            FleetManagement();
            DemographyChange(PE, ER);
            EffectsHandler(PE, ER);            
        }
       
        private static float SafetyCalculation(PlanetEconomy PE, EconomyRegistry ER)
        {
            string planet_economy_id = PE.my_id;
            float res = 0;
            for (int i = 0; i < ER.Fractions_Num; ++i)
                if (ER.AttackRewards[i].Contains(planet_economy_id))
                    res += (float)ER.AttackRewards[i][planet_economy_id];
            string system_id = (string)ER.SystemOfPlanet[planet_economy_id];            
            for (int i = 0; i < ER.Fractions_Num; ++i)
            {
                if (ER.DefenceRewards[i].Contains(system_id))
                {
                    if (ER.Relationships[i][PE.fraction_id] == (int)EconomyRegistry.Relation.Alliance)
                        res += (float)ER.DefenceRewards[i][system_id];
                    else if (ER.Relationships[i][PE.fraction_id] == (int)EconomyRegistry.Relation.Enemy)
                        res -= (float)ER.DefenceRewards[i][system_id];
                }
            }
            Registry R = Registry.getInstance();
            List<string> innerobjects = R.GetElement(system_id).inner_objects;
            foreach (string s in innerobjects)
            {
                RegistryObject RO = R.GetElement(s);
                //if (RO is Ship) 
                    res += SpaceshipInfluence(RO, PE.fraction_id, ER);
            }
            return res;            
        }

        private static float SpaceshipInfluence(RegistryObject RO, int fraction_id, EconomyRegistry ER)
        {
            return 0;
        }

        private static void TaxCollection(PlanetEconomy PE, float ke, EconomyRegistry ER)
        {
            float tax = EconomyRegistry.getInstance().FractionEconomies[PE.fraction_id].tax_rate;
            float income = PE.current_properties.prop_floats[(int)PlanetProperties.Floats.taxes_collected];
            PE.current_properties.prop_floats[(int)PlanetProperties.Floats.taxes_collected] += ke * income * tax;
            PE.current_properties.prop_floats[(int)PlanetProperties.Floats.colony_resources] += income * (1 - ke * tax);
            if (ke < eps)
            {
                PE.current_properties.prop_floats[(int)PlanetProperties.Floats.colony_resources]
                    += PE.current_properties.prop_floats[(int)PlanetProperties.Floats.taxes_collected];
                PE.current_properties.prop_floats[(int)PlanetProperties.Floats.taxes_collected] = 0;
            }
        }

        private static void EffectsHandler(PlanetEconomy PE, EconomyRegistry ER)
        {            
            float workers_eff = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.workers]
                * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.population_spirit]
                / (PE.current_properties.prop_ints[(int)PlanetProperties.Ints.empl_workers]);
            float engineers_eff = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.engineers]
                * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.population_spirit]
                / (PE.current_properties.prop_ints[(int)PlanetProperties.Ints.empl_engineers]);
            float scientists_eff = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.scientists]
                * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.population_spirit]
                / (PE.current_properties.prop_ints[(int)PlanetProperties.Ints.empl_scientists]);
            float soldiers_eff = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.soldiers]
                * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.population_spirit]
                / (PE.current_properties.prop_ints[(int)PlanetProperties.Ints.empl_soldiers]);
            if (workers_eff > 1) workers_eff = 1;
            if (engineers_eff > 1) engineers_eff = 1;
            if (scientists_eff > 1) scientists_eff = 1;
            if (soldiers_eff > 1) soldiers_eff = 1;
            Building B = ER.Buildings[0];
            float eff = 0;
            for (int i = 0; i < ER.Buildings_Num; ++i)
            {
                if (PE.colony_buildings_info[i].amount > 0)
                {
                    B = ER.Buildings[i];
                    eff = B.operating_conditions.automatic_contribution;                    
                    eff += workers_eff * B.operating_conditions.workers_contribution;
                    eff += engineers_eff * B.operating_conditions.engineers_contribution;
                    eff += soldiers_eff * B.operating_conditions.soldiers_contribution;
                    eff += scientists_eff * B.operating_conditions.scientists_contribution;
                    eff *= PE.colony_buildings_info[i].amount;
                    PE.colony_buildings_info[i].current_efficiency = eff;                    
                    foreach (LocalRepAction LRA in PE.colony_buildings_info[i].current_actions)
                    {
                        LRA.cur_cooldown -= 1;
                        if (LRA.cur_cooldown == 0)
                        {
                            HandleRepetitiveAction(PE, ER.RepetitiveActions[LRA.rep_action_id], eff);
                            LRA.cur_cooldown = ER.RepetitiveActions[LRA.rep_action_id].cooldown;
                        }
                    }
                }
            }   
            PE.current_properties = PE.basic_properties;
            for (int i = 0; i < ER.Buildings_Num; ++i)
            {
                if (PE.colony_buildings_info[i].amount > 0)
                {
                    foreach (int x in B.continuous_actions)
                    {
                        HandleContinuousAction(PE, ER.ContinuousActions[x], PE.colony_buildings_info[i].current_efficiency);
                    }
                }
            }
        }

        private static void HandleRepetitiveAction(PlanetEconomy PE,
            RepetitiveAction RA, float efficiency)
        {
            bool low_eff = (efficiency < RA.critical_efficiency);
            foreach (ElementaryAction EA in RA.el_actions)
                GetElementaryAction(PE, EA, efficiency, low_eff, true);
        }

        private static void HandleContinuousAction(PlanetEconomy PE,
            ContinuousAction CA, float efficiency)
        {
            bool low_eff = (efficiency < CA.critical_efficiency);
            foreach (ElementaryAction EA in CA.el_actions)
                GetElementaryAction(PE, EA, efficiency, low_eff, false);
        }            

        /// <summary>
        /// Элементарное воздействие на параметры планеты
        /// </summary>
        /// <param name="PE">Экономика планеты</param>
        /// <param name="EA">Элементарное действие</param>
        /// <param name="efficiency">Текущая эффективность</param>
        /// <param name="basic_prop">Воздействие на базовые свойства</param>
        /// <param name="low_eff">Действие в условиях низкой эффективности</param>

        private static void GetElementaryAction(PlanetEconomy PE,
            ElementaryAction EA, float efficiency, bool low_eff, bool basic_prop)
        {
            float act_koef = EA.action_koef;
            if (EA.efficiency_connected)
            {
                if (low_eff) act_koef = EA.low_eff_koef;
                else
                {
                    if ((EA.action_type == (int)ElementaryAction.ActionType.Add)
                        || (EA.action_type == (int)ElementaryAction.ActionType.Create))
                        act_koef *= efficiency;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Mul)
                        act_koef = (float)Math.Pow(act_koef, efficiency);
                }
            }
            if (basic_prop)
            {
                if (EA.prop_type == (int)ElementaryAction.PropType.Item)
                {
                    if (EA.action_type == (int)ElementaryAction.ActionType.Create) PE.pawnstore.items.Add(new Items(EA.prop_number, (int)Math.Floor(act_koef)));
                }
                else if (EA.prop_type == (int)ElementaryAction.PropType.Float)
                {
                    if (EA.action_type == (int)ElementaryAction.ActionType.Add) PE.basic_properties.prop_floats[EA.prop_number] += act_koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Set) PE.basic_properties.prop_floats[EA.prop_number] = act_koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Mul) PE.basic_properties.prop_floats[EA.prop_number] *= act_koef;
                }
                else if (EA.prop_type == (int)ElementaryAction.PropType.Int)
                {
                    int koef = (int)act_koef;
                    if (EA.action_type == (int)ElementaryAction.ActionType.Add) PE.basic_properties.prop_ints[EA.prop_number] += koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Set) PE.basic_properties.prop_ints[EA.prop_number] = koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Mul) PE.basic_properties.prop_ints[EA.prop_number] *= koef;
                }
            }
            else
            {
                if (EA.prop_type == (int)ElementaryAction.PropType.Item)
                {
                    if (EA.action_type == (int)ElementaryAction.ActionType.Create) PE.pawnstore.items.Add(new Items(EA.prop_number, (int)Math.Floor(act_koef)));
                }
                else if (EA.prop_type == (int)ElementaryAction.PropType.Float)
                {
                    if (EA.action_type == (int)ElementaryAction.ActionType.Add) PE.current_properties.prop_floats[EA.prop_number] += act_koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Set) PE.current_properties.prop_floats[EA.prop_number] = act_koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Mul) PE.current_properties.prop_floats[EA.prop_number] *= act_koef;
                }
                else if (EA.prop_type == (int)ElementaryAction.PropType.Int)
                {
                    int koef = (int)act_koef;
                    if (EA.action_type == (int)ElementaryAction.ActionType.Add) PE.current_properties.prop_ints[EA.prop_number] += koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Set) PE.current_properties.prop_ints[EA.prop_number] = koef;
                    else if (EA.action_type == (int)ElementaryAction.ActionType.Mul) PE.current_properties.prop_ints[EA.prop_number] *= koef;
                }
            }
        }

        private static void DemographyChange(PlanetEconomy PE, EconomyRegistry ER)
        {
            int people = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.total_population];
            float birthrate = PE.current_properties.prop_floats[(int)PlanetProperties.Floats.birthrate];
            int new_people = (int)Math.Floor(people * birthrate);
            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.total_population] += new_people;
            int new_engineers = (int)Math.Floor(people * birthrate * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.engineers_education]);
            int new_soldiers = (int)Math.Floor(people * birthrate * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.soldiers_education]);
            int new_scientists = (int)Math.Floor(people * birthrate * PE.current_properties.prop_floats[(int)PlanetProperties.Floats.scientists_education]);
            int new_workers = new_people - new_engineers - new_scientists - new_scientists;
            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.workers] += new_workers;
            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.engineers] += new_engineers;
            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.soldiers] += new_soldiers;
            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.scientists] += new_scientists;  
            int maxpopulation = PE.current_properties.prop_ints[(int)PlanetProperties.Ints.max_population];
            if (maxpopulation > people + new_people)
            {
                PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.workers] += maxpopulation - people - new_people;
                if (PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.workers] < 0)
                {
                    PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.soldiers]
                        += PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.workers];
                    PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.workers] = 0;
                    if (PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.soldiers] < 0)
                    {
                        PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.engineers]
                            += PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.soldiers];
                        PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.soldiers] = 0;
                        if (PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.engineers] < 0)
                        {
                            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.scientists]
                                += PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.engineers];
                            PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.engineers] = 0;
                            if (PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.scientists] < 0)
                                PE.basic_properties.prop_ints[(int)PlanetProperties.Ints.scientists] = 0;
                        }
                    }
                }
            }            
        }            
               
    }
}
