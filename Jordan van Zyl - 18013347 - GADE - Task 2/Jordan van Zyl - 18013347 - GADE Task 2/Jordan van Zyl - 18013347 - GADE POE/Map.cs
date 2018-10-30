using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class Map
    {
        // Arrays
        string[,] arrMap;
        Unit[] unit;
        Building[] building;

        // Random object
        Random rnd = new Random();

        public string [,]ArrMap
        {
            get
            {
                return arrMap;
            }
        }

        public Unit[] unitArray
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
            }
        }

        public Building[] buildingArray
        {
            get
            {
                return building;
            }
        }

        public Map(int numUnits, int numBuildings)
        {
            createMap();
            populateMap(numUnits, numBuildings);
        }

        public void createMap()
        {
            arrMap = new string[20, 20];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    arrMap[i, j] = ".";
                }
            }
        }

        public string fillMap()
        {
            string map = "";
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    map += arrMap[i, j];
                }
                map += "\n";
            }
            return map;
        }

        public void createUnits(int numUnits)
        {
            int unitCount = 0;
            unit = new Unit[numUnits];
            while(unitCount < numUnits)
            {
                // Generate random properties for each unit
                int pos_X = rnd.Next(0, 20);
                int pos_Y = rnd.Next(0, 20);
                int maxHealth = 100;
                int health = 100;
                int speed = rnd.Next(1, 3);
                int attack = rnd.Next(1, 3);
                int atkRange = rnd.Next(1, 5);
                string team = "";
                string symbol = "";
                bool isAttacking = false;
                int nameOption = rnd.Next(1, 11);
                string name = "";
                switch(nameOption)
                {
                    case 1:
                        {
                            name = "Josh";
                        }
                        break;
                    case 2:
                        {
                            name = "Cameron";
                        }
                        break;
                    case 3:
                        {
                            name = "Luke";
                        }
                        break;
                    case 4:
                        {
                            name = "Andrew";
                        }
                        break;
                    case 5:
                        {
                            name = "Matthew";
                        }
                        break;
                    case 6:
                        {
                            name = "Liall";
                        }
                        break;
                    case 7:
                        {
                            name = "Paul";
                        }
                        break;
                    case 8:
                        {
                            name = "Jesse";
                        }
                        break;
                    case 9:
                        {
                            name = "Peter";
                        }
                        break;
                    case 10:
                        {
                            name = "Will";
                        }
                        break;
                }

                // Randomise the unit type that will be added to the array
                int unitType = rnd.Next(1, 3);
                int typeteam = rnd.Next(1, 3);

                // Add a Melee unit to the map array
                if (unitType == 1)
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "M";
           
                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            MeleeUnit m_Unit = new MeleeUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);
                            unit[unitCount] = m_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "m";

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            MeleeUnit m_Unit = new MeleeUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, 1, team, symbol, isAttacking);
                            unit[unitCount] = m_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                }
                // Add a Ranged unit to the map array
                else
                {
                    if (typeteam == 1)
                    {
                        team = "Hero";
                        symbol = "R";
                        
                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            RangedUnit r_Unit = new RangedUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);
                            unit[unitCount] = r_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "r";

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            RangedUnit r_Unit = new RangedUnit(name, pos_X, pos_Y, health, maxHealth, speed, attack, atkRange, team, symbol, isAttacking);
                            unit[unitCount] = r_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                }
            }
        }

        public void createBuildings(int numBuildings)
        {
            int buildingCount = 0;
            building = new Building[numBuildings];
            while(buildingCount < numBuildings)
            {
                // Generate random properties for each building
                int pos_X = rnd.Next(0, 20);
                int pos_Y = rnd.Next(0, 20);
                int health = 100;
                string team = "";
                string symbol = "";

                if (buildingCount == 0)
                {
                    team = "Hero";
                    symbol = "F";
                    if (arrMap[pos_Y, pos_X] == ".")
                    {
                        FactoryBuilding f_building = new FactoryBuilding(pos_X, pos_Y, health, team, symbol);
                        building[buildingCount] = f_building;
                        ArrMap[pos_Y, pos_X] = symbol;
                        buildingCount++;
                    }
                }
                if(buildingCount == 1)
                {
                    team = "Villain";
                    symbol = "f";
                    if (arrMap[pos_Y, pos_X] == ".")
                    {
                        FactoryBuilding f_building = new FactoryBuilding(pos_X, pos_Y, health, team, symbol);
                        building[buildingCount] = f_building;
                        ArrMap[pos_Y, pos_X] = symbol;
                        buildingCount++;
                    }
                }
                if(buildingCount == 2)
                {
                    team = "Hero";
                    symbol = "W";
                    string resourceType = "guns";
                    int maxGeneration = 40;
                    int generationPerTick = 4;
                    if (arrMap[pos_Y, pos_X] == ".")
                    {
                        ResourceBuilding r_building = new ResourceBuilding(pos_X, pos_Y, health, team, symbol, resourceType, maxGeneration, generationPerTick);
                        building[buildingCount] = r_building;
                        ArrMap[pos_Y, pos_X] = symbol;
                        buildingCount++;
                    }
                }
                if(buildingCount == 3)
                {
                    team = "Villain";
                    symbol = "w";
                    if (arrMap[pos_Y, pos_X] == ".")
                    {
                        string resourceType = "guns";
                        int maxGeneration = 40;
                        int generationPerTick = 4;
                        ResourceBuilding r_building = new ResourceBuilding(pos_X, pos_Y, health, team, symbol, resourceType, maxGeneration, generationPerTick);
                        building[buildingCount] = r_building;
                        ArrMap[pos_Y, pos_X] = symbol;
                        buildingCount++;
                    }
                }
            }
        }

        public void populateMap(int numUnits, int numBuildings)
        {
            createUnits(numUnits);
            createBuildings(numBuildings);
        }

        public void spawn(int time)
        {
            int newSize = unit.Length + 1;
            // Spawn units from a factory building
            for (int i = 0; i < buildingArray.Length; i++)
            {
                string[] buildingType = buildingArray[i].GetType().ToString().Split('.');
                string type = buildingType[buildingType.Length - 1];
                if (type == "FactoryBuilding")
                {
                    FactoryBuilding f_building = (FactoryBuilding)buildingArray[i];

                    int currentResources = 0;
                    for(int j = 0; j < buildingArray.Length; j++)
                    {
                        ResourceBuilding temp = null;
                        string[] findResource = buildingArray[j].GetType().ToString().Split('.');
                        string resource = findResource[findResource.Length - 1];
                        if (resource == "ResourceBuilding")
                        {
                            temp = (ResourceBuilding)buildingArray[j];
                            if(temp.Team == f_building.Team)
                            {
                                currentResources = temp.GeneratedResources;
                            }
                        }
                    }
                    if(time % f_building.TicksPerProduction == 0 && f_building.UnitsToProduce != 0)
                    {
                        Unit newUnit = f_building.SpawnUnits(ArrMap, currentResources);

                        if(newUnit != null)
                        {
                            Array.Resize(ref unit, newSize);
                            unit[newSize - 1] = newUnit;
                            string[] unitType = newUnit.GetType().ToString().Split('.');
                            string newUnitType = unitType[unitType.Length - 1];

                            string symbol = "";
                            if (newUnitType == "MeleeUnit")
                            {
                                MeleeUnit m = (MeleeUnit)newUnit;
                                symbol = m.Symbol;
                            }
                            else
                            {
                                RangedUnit r = (RangedUnit)newUnit;
                                symbol = r.Symbol;
                            }

                            arrMap[f_building.Pos_Y - 1, f_building.Pos_X] = symbol;
                        }
                    }
                }
            }
        }

        public string unitInfo()
        {
            string s = "";
            for(int i = 0; i < unitArray.Length; i++)
            {
                if(unitArray[i] != null)
                {
                    string[] unit = unitArray[i].GetType().ToString().Split('.');
                    string type = unit[unit.Length - 1];
                    if (type == "RangedUnit")
                    {
                        RangedUnit r = (RangedUnit)unitArray[i];
                        s += r.toString();
                    }
                    else
                    {
                        MeleeUnit m = (MeleeUnit)unitArray[i];
                        s += m.toString();
                    }
                }
            }
            return s;
        }

        public string numUnits()
        {
            string s = "";
            s += "" + unitArray.Length;
            return s;
        }

        public void Generate(int time)
        {
            for(int i = 0; i < buildingArray.Length; i++)
            {
                string[] buildingType = buildingArray[i].GetType().ToString().Split('.');
                string type = buildingType[buildingType.Length - 1];
                if(type == "ResourceBuilding")
                {
                    ResourceBuilding temp = (ResourceBuilding)buildingArray[i];
                    temp.Generate(time);
                }
            }
        }

    }
}
