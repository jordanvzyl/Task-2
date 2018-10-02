using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    class Map
    {
        // Array to store map of battlefield and units on the battlefield
        string[,] arrMap = new string[20, 20];

        // Array to store units
        Unit[] arrUnit;
        FactoryBuilding[] arrFBuilding = new FactoryBuilding[2];

        // Random object rnd declared
        Random rnd;

        // Unit objects declared
        RangedUnit r_Unit;
        MeleeUnit m_Unit;
        FactoryBuilding f_Building;
        ResourceBuilding r_Building;


        int TimerTick;

        public Unit[] ArrUnit { get => arrUnit; set => arrUnit = value; }
        public RangedUnit R_Unit { get => r_Unit; set => r_Unit = value; }
        public MeleeUnit M_Unit { get => m_Unit; set => m_Unit = value; }
        public string[,] ArrMap { get => arrMap; set => arrMap = value; }
        public int TimerTick1 { get => TimerTick; set => TimerTick = value; }

        // Method to generate new battlefield
        public void newBattlefield()
        {
            rnd = new Random();
            // Populate the arrMap array with the battlefield
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    ArrMap[i, j] = ".";
                }
            }


            // Randomly generate a number of units to placed on the map and instantiate the arrunit array
            int numUnits = rnd.Next(1, 51);
            ArrUnit = new Unit[numUnits];

            // Add the units to the map
            int unitCount = 0;
            while (unitCount < numUnits)
            {
                // Generate random properties for each unit
                int pos_X = rnd.Next(0, 20);
                int pos_Y = rnd.Next(0, 20);
                int health = 100;
                int speed = rnd.Next(1, 3);
                int attack = rnd.Next(1, 5);
                int atkRange = rnd.Next(1, 11);
                string team = "";
                string symbol = "";
                bool isAttacking = false;
                string name1 = "Steve";
                string name2 = "Bob";

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
                        M_Unit = new MeleeUnit(name1, pos_X, pos_Y, health, speed, attack, 1, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {

                            ArrUnit[unitCount] = M_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "m";
                        M_Unit = new MeleeUnit(name2, pos_X, pos_Y, health, speed, attack, 1, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {

                            ArrUnit[unitCount] = M_Unit;
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
                        R_Unit = new RangedUnit(name1, pos_X, pos_Y, health, speed, attack, atkRange, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            ArrUnit[unitCount] = R_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                    else
                    {
                        team = "Villain";
                        symbol = "r";
                        R_Unit = new RangedUnit(name2, pos_X, pos_Y, health, speed, attack, atkRange, team, symbol, isAttacking);

                        if (ArrMap[pos_Y, pos_X] == ".")
                        {
                            ArrUnit[unitCount] = R_Unit;
                            ArrMap[pos_Y, pos_X] = symbol;
                            unitCount++;
                        }
                    }
                }
            }
            int buildingCount = 0;
            while (buildingCount < 2)
            {
                bool isEmpty = false;
                while (isEmpty == false)
                {
                    int spawn_X = rnd.Next(1, 20);
                    int spawn_Y = rnd.Next(1, 20);
                    int health = 100;
                    string team = "";
                    string symbol = "";
                    switch (buildingCount)
                    {
                        case 0:
                            {
                                team = "Hero";
                                symbol = "F";
                            }
                            break;
                        case 1:
                            {
                                team = "Villain";
                                symbol = "f";
                            }
                            break;
                    }
                    if (ArrMap[spawn_Y, spawn_X] == ".")
                    {
                        if(buildingCount < 2)
                        {
                            arrFBuilding[buildingCount] = new FactoryBuilding(spawn_X, spawn_Y, health, team, symbol);
                            ArrMap[spawn_Y, spawn_X] = arrFBuilding[buildingCount].Symbol;
                            buildingCount++;
                        }  
                    }
                }
            }

            f_Building.SpawnUnits(TimerTick);
        }


        // Method to move the unit to a specific position on the visual of the map
        public void newPosition(Unit unit, int newPos_X, int newPos_Y)
        {
            int old_X = unit.Pos_X;
            int old_Y = unit.Pos_Y;
            ArrMap[old_Y, old_X] = ".";
            UpdatePosition(unit, newPos_X, newPos_Y);
        }

        // Method to update the internal position of the map
        public void UpdatePosition(Unit unit, int pos_X, int pos_Y)
        {
            int count_X = 0;
            int count_Y = 0;
            string movement = "";

            if (count_X != pos_X)
            {
                if (count_X < pos_X)
                {
                    movement = "right";
                    if (unit.Speed > 1)
                    {
                        if (unit.Speed > Math.Abs(unit.Pos_X - pos_X))
                        {
                            count_X = Math.Abs(unit.Pos_X - pos_X);
                        }
                        else
                        {
                            count_X += unit.Speed;
                        }
                    }
                    else
                    {
                        count_X++;
                    }
                }
                else
                {
                    movement = "left";
                    if (unit.Speed > 1)
                    {
                        if (unit.Speed > Math.Abs(unit.Pos_X - pos_X))
                        {
                            count_X = Math.Abs(unit.Pos_X - pos_X);
                        }
                        else
                        {
                            count_X += unit.Speed;
                        }
                    }
                    else
                    {
                        count_X++;
                    }
                }
            }
            if (count_Y < pos_Y)
            {
                if (count_Y < pos_Y)
                {
                    movement = "up";
                    if (unit.Speed > 1)
                    {
                        if (unit.Speed > Math.Abs(unit.Pos_Y - pos_Y))
                        {
                            count_Y = Math.Abs(unit.Pos_Y - pos_Y) - 1;
                        }
                        else
                        {
                            count_Y += unit.Speed;
                        }
                    }
                    else
                    {
                        count_Y++;
                    }
                }
                else
                {
                    movement = "down";
                    if (unit.Speed > 1)
                    {
                        if (unit.Speed > Math.Abs(unit.Pos_Y - pos_Y))
                        {
                            count_Y = Math.Abs(unit.Pos_Y - pos_Y) - 1;
                        }
                        else
                        {
                            count_Y += unit.Speed;
                        }
                    }
                    else
                    {
                        count_Y++;
                    }
                }
            }

            string[] arrType = unit.GetType().ToString().Split('.');
            string type = arrType[arrType.Length - 1];

            if (type == "MeleeUnit")
            {
                switch (movement)
                {
                    case "right":
                        {
                            if (pos_X + count_X <= 19)
                            {
                                M_Unit.newPosition(pos_X + count_X, pos_Y);
                                ArrMap[pos_Y, pos_X + count_X] = unit.Symbol;
                                ArrMap[pos_Y, pos_X] = ".";
                            }
                        }
                        break;
                    case "left":
                        {
                            if (pos_X - count_X >= 0)
                            {
                                M_Unit.newPosition(pos_X - count_X, pos_Y);
                                ArrMap[pos_Y, pos_X - count_X] = unit.Symbol;
                                ArrMap[pos_Y, pos_X] = ".";
                            }
                        }
                        break;
                    case "up":
                        {
                            if (pos_Y + count_Y <= 19)
                            {
                                M_Unit.newPosition(pos_X, pos_Y + count_Y);
                                ArrMap[pos_Y + count_Y, pos_X] = unit.Symbol;
                                ArrMap[pos_Y, pos_X] = ".";
                            }
                        }
                        break;
                    case "down":
                        {
                            if (pos_Y - count_Y >= 0)
                            {
                                M_Unit.newPosition(pos_X, pos_Y - count_Y);
                                ArrMap[pos_Y - count_Y, pos_X] = unit.Symbol;
                                ArrMap[pos_Y, pos_X] = ".";
                            }
                        }
                        break;
                }
            }
        }

        // Output the map
        public string redraw()
        {
            string s = "";
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    s += ArrMap[i, j];
                }
                s += "\n";
            }
            return s;
        }

        // Save method
        public void Save()
        {
            for(int i = 0; i < ArrUnit.Length; i++)
            {
                string[] type = ArrUnit[i].GetType().ToString().Split('.');
                string finalType = type[type.Length - 1];
                if(finalType == "RangedUnit")
                {
                    R_Unit.Save();
                }
                else
                {
                    M_Unit.Save();
                }
            }
            f_Building.SpawnUnits(TimerTick1);
            r_Building.ManageResources(TimerTick1);

            f_Building.Save();
            r_Building.Save();

            
        }

        // Read method to read in values from files
        public void Read()
        {
            // Read in MeleeUnits
            FileStream fileMeleeUnit = new FileStream("saves/MeleeUnit.file", FileMode.Open, FileAccess.Read);
            StreamReader readerMelee = new StreamReader(fileMeleeUnit);
            string line = readerMelee.ReadLine();
            while (line != null)
            {
                string[] unit = line.Split(',');
                string name = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                int speed = Convert.ToInt32(unit[4]);
                int attack = Convert.ToInt32(unit[5]);
                int atkRange = Convert.ToInt32(unit[6]);
                string team = unit[7];
                string symbol = unit[8];
                bool isAttacking = Convert.ToBoolean(unit[9]);

                MeleeUnit newMelee = new MeleeUnit(name, pos_X, pos_Y, health, speed, attack, atkRange, team, symbol, isAttacking);
                Console.WriteLine(newMelee.toString());

                line = readerMelee.ReadLine();
            }
            readerMelee.Close();
            fileMeleeUnit.Close();

            // Read in RangedUnits
            FileStream fileRangedUnit = new FileStream("saves/RangedUnit.file", FileMode.Open, FileAccess.Read);
            StreamReader readerRanged = new StreamReader(fileRangedUnit);
            string line2 = readerRanged.ReadLine();
            while (line2 != null)
            {
                string[] unit = line2.Split(',');
                string name = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                int speed = Convert.ToInt32(unit[4]);
                int attack = Convert.ToInt32(unit[5]);
                int atkRange = Convert.ToInt32(unit[6]);
                string team = unit[7];
                string symbol = unit[8];
                bool isAttacking = Convert.ToBoolean(unit[9]);

                RangedUnit newRanged = new RangedUnit(name, pos_X, pos_Y, health, speed, attack, atkRange, team, symbol, isAttacking);
                Console.WriteLine(newRanged.toString());

                line2 = readerRanged.ReadLine();
            }
            readerRanged.Close();
            fileRangedUnit.Close();

            // Read in resource building
            FileStream fileResource = new FileStream("saves/ResourceBuilding.file", FileMode.Open, FileAccess.Read);
            StreamReader readerResource = new StreamReader(fileResource);
            string line3 = readerResource.ReadLine();
            while (line3 != null)
            {
                string[] unit = line3.Split(',');
                string resourceType = unit[0];
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                string team = unit[4];
                string symbol = unit[5];

                ResourceBuilding newResource = new ResourceBuilding(pos_X, pos_Y, health, team, symbol);
                Console.WriteLine(newResource.toString());

                line3 = readerResource.ReadLine();
            }
            readerResource.Close();
            fileResource.Close();

            // Read in factory building
            FileStream fileFactory = new FileStream("saves/FactoryBuilding.file", FileMode.Open, FileAccess.Read);
            StreamReader readerFactory= new StreamReader(fileFactory);
            string line4 = readerFactory.ReadLine();
            while (line4 != null)
            {
                string[] unit = line4.Split(',');
                int unitsToProduce = Convert.ToInt32(unit[0]);
                int pos_X = Convert.ToInt32(unit[1]);
                int pos_Y = Convert.ToInt32(unit[2]);
                int health = Convert.ToInt32(unit[3]);
                string team = unit[4];
                string symbol = unit[5];

                FactoryBuilding newFactory = new FactoryBuilding(pos_X, pos_Y, health, team, symbol);
                Console.WriteLine(newFactory.toString());

                line4 = readerFactory.ReadLine();
            }
            readerFactory.Close();
            fileFactory.Close();
        }
    }
    
}
