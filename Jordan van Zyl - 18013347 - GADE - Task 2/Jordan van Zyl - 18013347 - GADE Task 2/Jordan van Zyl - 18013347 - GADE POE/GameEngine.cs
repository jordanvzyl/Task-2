using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class GameEngine
    {
        Map map;
        Random rnd = new Random();

        public GameEngine(int numUnits, int numBuildings)
        {
            map = new Map(numUnits, numBuildings);
        }

        public string Redraw()
        {
            string s = "";
            s += map.fillMap();
            return s;
        }

        public void playGame(int time)
        {
            map.Generate(time);
            map.spawn(time);
            for(int i = 0; i < map.unitArray.Length; i++)
            {
                if(map.unitArray[i] != null)
                {
                    string[] unitType = map.unitArray[i].GetType().ToString().Split('.');
                    string type = unitType[unitType.Length - 1];
                    if (type == "RangedUnit" && map.unitArray[i] != null)
                    {
                        RangedUnit r = (RangedUnit)map.unitArray[i];
                        if (r.isDead() == false)
                        {
                            map.ArrMap[r.Pos_Y, r.Pos_X] = r.Symbol;
                            for(int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if(buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if(buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            // Test if this works
                            Unit closestEnemy = r.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                Console.WriteLine("Current unit: " + "\n" + map.unitArray[i].toString());
                                Console.WriteLine("Closest unit: " + "\n" + closestEnemy.toString());
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }

                                if ((r.Health / r.MaxHealth * 100) <= 25)
                                {
                                    runAway(r);
                                    r.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (r.IsAttacking == true)
                                        {
                                            r.combat(closestEnemy);
                                        }

                                        if (r.IsAttacking == false)
                                        {
                                            if (r.withinAtkRange(closestEnemy) == true)
                                            {
                                                r.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % r.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(r, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                Console.WriteLine("Closest is null");
                            }
                            
                            
                            else
                            {
                                if(time % r.Speed == 0 )
                                {
                                    runAway(r);
                                }
                            }
                            */

                        }
                        else
                        {
                            map.ArrMap[r.Pos_Y, r.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                    if (type == "MeleeUnit" && map.unitArray[i] != null)
                    {
                        MeleeUnit m = (MeleeUnit)map.unitArray[i];
                        if (m.isDead() == false)
                        {
                            map.ArrMap[m.Pos_Y, m.Pos_X] = m.Symbol;
                            for (int j = 0; j < map.buildingArray.Length; j++)
                            {
                                string[] building = map.buildingArray[j].ToString().Split('.');
                                string buidlingType = building[building.Length - 1];
                                if (buidlingType == "FactoryBuilding")
                                {
                                    FactoryBuilding f_Building = (FactoryBuilding)map.buildingArray[j];
                                    map.ArrMap[f_Building.Pos_Y, f_Building.Pos_X] = f_Building.Symbol;
                                }
                                if (buidlingType == "ResourceBuilding")
                                {
                                    ResourceBuilding r_Building = (ResourceBuilding)map.buildingArray[j];
                                    map.ArrMap[r_Building.Pos_Y, r_Building.Pos_X] = r_Building.Symbol;
                                }
                            }
                            Unit closestEnemy = m.closestUnit(map.unitArray);

                            if (closestEnemy != null)
                            {
                                string[] enemy = closestEnemy.GetType().ToString().Split('.');
                                string enemyType = enemy[enemy.Length - 1];
                                int enemyHealth = 0;
                                if (enemyType == "RangedUnit")
                                {
                                    RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                    enemyHealth = r_Enemy.Health;
                                }
                                if (enemyType == "MeleeUnit")
                                {
                                    MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                    enemyHealth = m_Enemy.Health;
                                }

                                if ((m.Health / m.MaxHealth * 100) <= 25)
                                {
                                    runAway(m);
                                    m.IsAttacking = false;
                                }
                                else
                                {
                                    if (enemyHealth > 0)
                                    {
                                        if (m.IsAttacking == true)
                                        {
                                            m.combat(closestEnemy);
                                        }

                                        if (m.IsAttacking == false)
                                        {
                                            if (m.withinAtkRange(closestEnemy) == true)
                                            {
                                                m.combat(closestEnemy);
                                            }
                                            else
                                            {
                                                if (time % m.Speed == 0 && enemyHealth > 0)
                                                {
                                                    moveUnits(m, closestEnemy);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int enemyPos = 0;

                                        for (int j = 0; j < map.unitArray.Length; j++)
                                        {
                                            if (map.unitArray[j] == closestEnemy)
                                            {
                                                enemyPos = j;
                                            }
                                        }

                                        if (enemyType == "RangedUnit")
                                        {
                                            RangedUnit r_Enemy = (RangedUnit)closestEnemy;
                                            map.ArrMap[r_Enemy.Pos_Y, r_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                        if (enemyType == "MeleeUnit")
                                        {
                                            MeleeUnit m_Enemy = (MeleeUnit)closestEnemy;
                                            map.ArrMap[m_Enemy.Pos_Y, m_Enemy.Pos_X] = ".";
                                            //map.unitArray[enemyPos] = null;
                                        }
                                    }
                                }
                            }
                            /*
                            else
                            {
                                if(time % m.Speed == 0)
                                {
                                    runAway(m);
                                }
                            }
                            */
                            
                        }
                        else
                        {
                            map.ArrMap[m.Pos_Y, m.Pos_X] = ".";
                            //map.unitArray[i] = null;
                        }
                    }
                }
            }
            Redraw();
        }

        // Method to move
        public void moveUnits(Unit unit, Unit enemy)
        {
            string[] unitType = unit.GetType().ToString().Split('.');
            string thisType = unitType[unitType.Length - 1];

            string[] enemyType = enemy.GetType().ToString().Split('.');
            string thisEnemy = enemyType[enemyType.Length - 1];

            if(thisType == "MeleeUnit")
            {
                MeleeUnit m = (MeleeUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if(Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if(distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if(distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if(distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if(distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }
                else
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = m.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = m.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X + 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && m.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = m.Pos_X;
                            int new_X = m.Pos_X - 1;
                            m.newPosition(new_X, m.Pos_Y);
                            map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                            map.ArrMap[m.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y + 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && m.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = m.Pos_Y;
                            int new_Y = m.Pos_Y - 1;
                            m.newPosition(m.Pos_X, new_Y);
                            map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                            map.ArrMap[old_Y, m.Pos_X] = ".";
                        }
                    }
                }

            }
            else
            {
                RangedUnit r = (RangedUnit)unit;
                if (thisEnemy == "MeleeUnit")
                {
                    MeleeUnit enemyUnit = (MeleeUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
                else
                {
                    RangedUnit enemyUnit = (RangedUnit)enemy;

                    int distance_X = r.Pos_X - enemyUnit.Pos_X;
                    int distance_Y = r.Pos_Y - enemyUnit.Pos_Y;

                    if (Math.Abs(distance_X) < Math.Abs(distance_Y))
                    {
                        if (distance_X < 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X + 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                        if (distance_X > 0 && r.Pos_X != enemyUnit.Pos_X)
                        {
                            int old_X = r.Pos_X;
                            int new_X = r.Pos_X - 1;
                            r.newPosition(new_X, r.Pos_Y);
                            map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                            map.ArrMap[r.Pos_Y, old_X] = ".";
                        }
                    }
                    else
                    {
                        if (distance_Y < 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y + 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                        if (distance_Y > 0 && r.Pos_Y != enemyUnit.Pos_Y)
                        {
                            int old_Y = r.Pos_Y;
                            int new_Y = r.Pos_Y - 1;
                            r.newPosition(r.Pos_X, new_Y);
                            map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                            map.ArrMap[old_Y, r.Pos_X] = ".";
                        }
                    }
                }
            }
        }

        // Method to runaway
        public void runAway(Unit unit)
        {
            string[] unitType = unit.GetType().ToString().Split('.');
            string UnitType = unitType[unitType.Length - 1];
            int rndMove = rnd.Next(1, 5);
            switch(rndMove)
            {
                case 1:
                    {
                        // Randomly move up
                        if(UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if(r.Pos_Y != 0)
                            {
                                int old_Y = r.Pos_Y;
                                int new_Y = r.Pos_Y - 1;
                                r.newPosition(r.Pos_X, new_Y);
                                map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                                map.ArrMap[old_Y, r.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_Y != 0)
                            {
                                int old_Y = m.Pos_Y;
                                int new_Y = m.Pos_Y - 1;
                                m.newPosition(m.Pos_X, new_Y);
                                map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                                map.ArrMap[old_Y, m.Pos_X] = ".";
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        // Randomly move left
                        if(UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if(r.Pos_X != 0)
                            {
                                int old_X = r.Pos_X;
                                int new_X = r.Pos_X - 1;
                                r.newPosition(new_X, r.Pos_Y);
                                map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                                map.ArrMap[r.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_X != 0)
                            {
                                int old_X = m.Pos_X;
                                int new_X = m.Pos_X - 1;
                                m.newPosition(new_X, m.Pos_Y);
                                map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                                map.ArrMap[m.Pos_Y, old_X] = ".";
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        // Randomly move down
                        if(UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if(r.Pos_Y != 19)
                            {
                                int old_Y = r.Pos_Y;
                                int new_Y = r.Pos_Y + 1;
                                r.newPosition(r.Pos_X, new_Y);
                                map.ArrMap[new_Y, r.Pos_X] = r.Symbol;
                                map.ArrMap[old_Y, r.Pos_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_Y != 19)
                            {
                                int old_Y = m.Pos_Y;
                                int new_Y = m.Pos_Y + 1;
                                m.newPosition(m.Pos_X, new_Y);
                                map.ArrMap[new_Y, m.Pos_X] = m.Symbol;
                                map.ArrMap[old_Y, m.Pos_X] = ".";
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        // Randomly move right
                        if(UnitType == "RangedUnit")
                        {
                            RangedUnit r = (RangedUnit)unit;
                            if(r.Pos_X != 19)
                            {
                                int old_X = r.Pos_X;
                                int new_X = r.Pos_X + 1;
                                r.newPosition(new_X, r.Pos_Y);
                                map.ArrMap[r.Pos_Y, new_X] = r.Symbol;
                                map.ArrMap[r.Pos_Y, old_X] = ".";
                            }
                        }
                        if (UnitType == "MeleeUnit")
                        {
                            MeleeUnit m = (MeleeUnit)unit;
                            if (m.Pos_X != 19)
                            {
                                int old_X = m.Pos_X;
                                int new_X = m.Pos_X + 1;
                                m.newPosition(new_X, m.Pos_Y);
                                map.ArrMap[m.Pos_Y, new_X] = m.Symbol;
                                map.ArrMap[m.Pos_Y, old_X] = ".";
                            }
                        }
                    }
                    break;
            }
        }

        public int getArrSize()
        {
            return map.unitArray.Length;
        }

        public Unit[] getArray()
        {
            return map.unitArray;
        }

        public string getNum()
        {
            int count = 0;
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    if(map.ArrMap[i, j] == "m" || map.ArrMap[i, j] == "M" || map.ArrMap[i, j] == "r" || map.ArrMap[i, j] == "R")
                    {
                        count++;
                    }
                }
            }

            return "number of units is: " + count;
        }

        public string display(int time)
        {
            string s = "";
            for(int i = 0; i < map.buildingArray.Length; i++)
            {
                string[] buildingType = map.buildingArray[i].GetType().ToString().Split('.');
                string type = buildingType[buildingType.Length - 1];

                if(type == "ResourceBuilding")
                {
                    ResourceBuilding r_building = (ResourceBuilding)map.buildingArray[i];
                    // r_building.Generate(time);
                    s += r_building.toString();
                }
                else
                {
                    s += "No resource buildings were found in the array";
                }
            }
            return s;
        }

        public string getUnitInfo()
        {
            return map.unitInfo();
        }
    }
}
