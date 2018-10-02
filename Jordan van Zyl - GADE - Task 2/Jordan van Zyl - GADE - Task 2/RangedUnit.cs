using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    class RangedUnit : Unit
    {
        // RangedUnit constructor that inherits from Unit class
        public RangedUnit(string name, int pos_X, int pos_Y, int health, int speed, int attack, int attackRange, string team, string symbol, bool isAttacking) : base(name, pos_X, pos_Y, health, speed, attack, attackRange, team, symbol, isAttacking)
        {

        }


        // Overrride method to set a new position of the unit
        public override void newPosition(int new_X, int new_Y)
        {
            if (new_X <= 19 && new_X >= 0)
            {
                pos_X = new_X;
            }
            if (pos_Y <= 19 && new_Y >= 0)
            {
                pos_Y = new_Y;
            }
        }

        // Override method to handle the combat with another unit
        public override void combat(Unit enemy)
        {
            if (withinAtkRange(enemy) == true)
            {
                enemy.Health -= attack;
                isAttacking = true;
                attackState(isAttacking);
            }
        }

        // Override method to determine whether another unit is within range
        public override bool withinAtkRange(Unit enemy)
        {
            string myTeam = team;
            string enemyTeam = enemy.Team;

            if (myTeam != enemyTeam)
            {
                int distance_X = Math.Abs(pos_X - enemy.Pos_X);
                int distance_Y = Math.Abs(pos_Y - enemy.Pos_Y);

                int totalDistance = distance_X + distance_Y;

                if (totalDistance <= AttackRange)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Override method to return closest unit's position
        public override string closestUnit(Unit[] enemy)
        {
            int closest_X = 1000;
            int closest_Y = 1000;

            int enemy_X;
            int enemy_Y;

            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].Team != team)
                {
                    enemy_X = enemy[i].Pos_X;
                    enemy_Y = enemy[i].Pos_Y;

                    if (enemy_X != pos_X && enemy_Y != pos_Y)
                    {
                        int dist_X = Math.Abs(pos_X - enemy_X);
                        int dist_Y = Math.Abs(pos_Y - enemy_Y);

                        if (dist_X < closest_X)
                        {
                            closest_X = enemy_X;
                        }
                        if (dist_Y < closest_Y)
                        {
                            closest_Y = enemy_Y;
                        }
                    }
                }
            }

            string coordinates = closest_X + ";" + closest_Y;
            return coordinates;
        }

        // Method to determine if the unit is attacking or not
        public override bool attackState(bool isAttacking)
        {
            if (isAttacking == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to handle death of unit
        ~RangedUnit()
        {
            Health = 0;
        }

        // Override method to return a ToString
        public override string toString()
        {
            string rangedUnit = "";
            rangedUnit += Name + "," + Pos_X + "," + Pos_Y + "," + Health + "," + Speed + "," + Attack + "," + AttackRange + "," + Team + "," + Symbol + "," + IsAttacking + "\n";
            return rangedUnit;
        }

        // Override Save method
        public override void Save()
        {
            if (Directory.Exists("saves") != true)
            {
                Directory.CreateDirectory("saves");
                Console.WriteLine("Directory created!");
            }

            FileStream file = new FileStream("saves/RangedUnit.file", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(toString());
            writer.Close();
            file.Close();
        }
    }
}
