using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    class GameEngine
    {
        Map map;
        // Declared the required map object from map class
        public GameEngine()
        {
            Map map = new Map();
        }

        // Declare a random object
        Random rnd = new Random();

        public string updateMap(int time)
        {
            string toString = "";

            string closest = "";

            map.newBattlefield();

            for (int i = 0; i < map.ArrUnit.Length; i++)
            {
                // Get the team of the unit
                string team = map.ArrUnit[i].Team;

                // Get the type of unit
                string unitType = map.ArrUnit[i].GetType().ToString();
                string[] arrType = unitType.Split('.');
                unitType = arrType[arrType.Length - 1];

                // If the unit type is a RangedUnit
                if (unitType == "RangedUnit")
                {
                    closest = map.ArrUnit[i].closestUnit(map.ArrUnit);

                    string[] closestPos = closest.Split(';');
                    int closest_X = Convert.ToInt32(closestPos[0]);
                    int closest_Y = Convert.ToInt32(closestPos[1]);

                    int current_X = map.ArrUnit[i].Pos_X;
                    int current_Y = map.ArrUnit[i].Pos_Y;

                    int moveDistanceX = current_X - closest_X;
                    int absoluteX = Math.Abs(moveDistanceX);
                    int moveDistanceY = current_Y - closest_Y;
                    int absoluteY = Math.Abs(moveDistanceY);

                    // map.updatePosition(unitType, map.R_Unit.Symbol, map.R_Unit.Pos_X, map.R_Unit.Pos_Y, closest_X, closest_Y, map.R_Unit.Speed);

                    // Check if unit is in combat 
                    if(map.ArrUnit[i].withinAtkRange(map.ArrUnit[i]) != true && map.ArrUnit[i].IsAttacking != true)
                    {
                        map.newPosition(map.ArrUnit[i], closest_X, closest_Y);
                    }
                    else
                    {
                        // Determining if the unit is within the attack range
                        if (map.R_Unit.withinAtkRange(map.ArrUnit[i]))
                        {
                            // If it is within the attack range then go into combat (but don't move)
                            map.R_Unit.combat(map.ArrUnit[i]);

                            // If health is less than 25 then run in random direction
                            if (map.R_Unit.Health < 25)
                            {
                                int rndMove = rnd.Next(1, 5);
                                switch (rndMove)
                                {
                                    // move right
                                    case 1:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.R_Unit.Pos_X + 1, map.R_Unit.Pos_Y);
                                        }
                                        break;
                                    // move left
                                    case 2:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.R_Unit.Pos_X - 1, map.R_Unit.Pos_Y);
                                        }
                                        break;
                                    // move up
                                    case 3:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.R_Unit.Pos_X, map.R_Unit.Pos_Y - 1);
                                        }
                                        break;
                                    // move down
                                    case 4:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.R_Unit.Pos_X, map.R_Unit.Pos_Y + 1);
                                        }
                                        break;
                                }
                            }
                        }
                    }  
                }
                // If the unit type is a MeleeUnit
                else
                {
                    int current_X = map.ArrUnit[i].Pos_X;
                    int current_Y = map.ArrUnit[i].Pos_Y;

                    closest = map.ArrUnit[i].closestUnit(map.ArrUnit);

                    string[] closestPos = closest.Split(';');
                    int closest_X = Convert.ToInt32(closestPos[0]);
                    int closest_Y = Convert.ToInt32(closestPos[1]);

                    int moveDistanceX = current_X - closest_X;
                    int absoluteX = Math.Abs(moveDistanceX);
                    int moveDistanceY = current_Y - closest_Y;
                    int absoluteY = Math.Abs(moveDistanceY);

                    // map.updatePosition(unitType, map.M_Unit.Symbol, map.M_Unit.Pos_X, map.M_Unit.Pos_Y, closest_X, closest_Y, map.M_Unit.Speed);

                    // Check if unit is in combat 
                    if (map.ArrUnit[i].withinAtkRange(map.ArrUnit[i]) != true && map.ArrUnit[i].IsAttacking != true)
                    {
                        map.newPosition(map.ArrUnit[i], closest_X, closest_Y);
                    }
                    else
                    {
                        // Determining if the unit is within the attack range
                        if (map.M_Unit.withinAtkRange(map.ArrUnit[i]) == true)
                        {
                            // If it is within the attack range then go into combat (but don't move)
                            map.M_Unit.combat(map.ArrUnit[i]);

                            // If health is less than 25 then run in random direction
                            if (map.M_Unit.Health < 25)
                            {
                                int rndMove = rnd.Next(1, 5);
                                switch (rndMove)
                                {
                                    // move right
                                    case 1:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.M_Unit.Pos_X + 1, map.M_Unit.Pos_Y);
                                        }
                                        break;
                                    // move left
                                    case 2:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.M_Unit.Pos_X - 1, map.M_Unit.Pos_Y);
                                        }
                                        break;
                                    // move up
                                    case 3:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.M_Unit.Pos_X, map.M_Unit.Pos_Y - 1);
                                        }
                                        break;
                                    // move down
                                    case 4:
                                        {
                                            map.newPosition(map.ArrUnit[i], map.M_Unit.Pos_X, map.M_Unit.Pos_Y + 1);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                toString += map.redraw();
                
            }
            return toString;
        }
    }
}
