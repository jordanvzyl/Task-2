using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class MeleeUnit : Unit
    {
        // MeleeUnit constructor that inherits from Unit class
        public MeleeUnit(string name, int pos_X, int pos_Y, int health, int maxHealth, int speed, int attack, int attackRange, string team, string symbol, bool isAttacking) : base(name, pos_X, pos_Y, health, maxHealth, speed, attack, attackRange, team, symbol, isAttacking)
        {

        }

        // Accessor methods
        public int Pos_X
        {
            get
            {
                return pos_X;
            }

            set
            {
                pos_X = value;
            }
        }

        public int Pos_Y
        {
            get
            {
                return pos_Y;
            }

            set
            {
                pos_Y = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }
        }

        public int Attack
        {
            get
            {
                return attack;
            }
        }

        public int AttackRange
        {
            get
            {
                return attackRange;
            }
        }

        public string Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
            }
        }

        public bool IsAttacking
        {
            get
            {
                return isAttacking;
            }
            set
            {
                isAttacking = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        // Check if the unit is still alive
        public bool isDead()
        {
            if(this.Health > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                MeleeUnit m_Unit;
                RangedUnit r_Unit;

                string[] type = enemy.GetType().ToString().Split('.');
                string enemyType = type[type.Length - 1];
                if (enemyType == "MeleeUnit")
                {
                    m_Unit = (MeleeUnit)enemy;
                    m_Unit.Health -= this.Attack;
                    this.IsAttacking = true;
                    attackState(IsAttacking);
                }
                else
                {
                    r_Unit = (RangedUnit)enemy;
                    r_Unit.Health -= this.Attack;
                    this.IsAttacking = true;
                    attackState(IsAttacking);
                }
            }
            else
            {
                this.IsAttacking = false;
                attackState(IsAttacking);
            }
        }

        // Override method to determine whether another unit is within range
        public override bool withinAtkRange(Unit enemy)
        {
            MeleeUnit m_Unit;
            RangedUnit r_Unit;

            string[] type = enemy.GetType().ToString().Split('.');
            string enemyType = type[type.Length - 1];

            if (enemyType == "RangedUnit")
            {
                r_Unit = (RangedUnit)enemy;
                string myTeam = this.team;
                string enemyTeam = r_Unit.Team;

                if (myTeam != enemyTeam)
                {
                    int distance_X = Math.Abs(this.pos_X - r_Unit.Pos_X);
                    int distance_Y = Math.Abs(this.pos_Y - r_Unit.Pos_Y);

                    int totalDistance = distance_X + distance_Y;

                    if (totalDistance <= this.attackRange)
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
            else
            {
                m_Unit = (MeleeUnit)enemy;
                string myTeam = this.team;
                string enemyTeam = m_Unit.Team;

                if (myTeam != enemyTeam)
                {
                    int distance_X = Math.Abs(this.pos_X - m_Unit.Pos_X);
                    int distance_Y = Math.Abs(this.pos_Y - m_Unit.Pos_Y);

                    int totalDistance = distance_X + distance_Y;

                    if (totalDistance <= this.attackRange)
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


        }

        // Override method to return closest unit's position
        public override Unit closestUnit(Unit[] enemy)
        {
            int closest = 100;
            Unit enemyUnit = null;

            MeleeUnit m_Unit;
            RangedUnit r_Unit;

            int x_Dist = 0;
            int y_Dist = 0;

            for (int i = 0; i < enemy.Length; i++)
            {
                string[] type = enemy[i].GetType().ToString().Split('.');
                string enemyType = type[type.Length - 1];
                if (enemyType == "RangedUnit")
                {
                    r_Unit = (RangedUnit)enemy[i];
                    if (r_Unit.Team != this.team)
                    {
                        x_Dist = Math.Abs(this.Pos_X - r_Unit.Pos_X);
                        y_Dist = Math.Abs(this.Pos_Y - r_Unit.Pos_Y);

                        if ((x_Dist + y_Dist) < closest)
                        {
                            closest = x_Dist + y_Dist;
                            enemyUnit = enemy[i];
                        }
                    }

                }
                if (enemyType == "MeleeUnit")
                {
                    m_Unit = (MeleeUnit)enemy[i];
                    if (m_Unit.Team != this.team)
                    {
                        x_Dist = Math.Abs(this.Pos_X - m_Unit.Pos_X);
                        y_Dist = Math.Abs(this.Pos_Y - m_Unit.Pos_Y);

                        if ((x_Dist + y_Dist) < closest)
                        {
                            closest = x_Dist + y_Dist;
                            enemyUnit = enemy[i];
                        }
                    }

                }
            }
            return enemyUnit;

            /*
            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i] != null)
                {
                    string[] type = enemy[i].GetType().ToString().Split('.');
                    string enemyType = type[type.Length - 1];

                    if (enemyType == "RangedUnit")
                    {
                        r_Unit = (RangedUnit)enemy[i];
                        if (r_Unit.Team != this.team)
                        {
                            enemy_X = r_Unit.Pos_X;
                            enemy_Y = r_Unit.Pos_Y;

                            if (enemy_X != this.pos_X && enemy_Y != this.pos_Y)
                            {
                                int dist_X = Math.Abs(pos_X - enemy_X);
                                int dist_Y = Math.Abs(pos_Y - enemy_Y);

                                int totalDist = dist_X + dist_Y;
                                if (totalDist < closest)
                                {
                                    closest = totalDist;
                                    enemyUnit = r_Unit;
                                }
                            }
                        }
                    }
                    else
                    {
                        m_Unit = (MeleeUnit)enemy[i];
                        if (m_Unit.Team != this.team)
                        {
                            enemy_X = m_Unit.Pos_X;
                            enemy_Y = m_Unit.Pos_Y;

                            if (enemy_X != this.pos_X && enemy_Y != this.pos_Y)
                            {
                                int dist_X = Math.Abs(pos_X - enemy_X);
                                int dist_Y = Math.Abs(pos_Y - enemy_Y);

                                int totalDist = dist_X + dist_Y;
                                if (totalDist < closest)
                                {
                                    closest = totalDist;
                                    enemyUnit = m_Unit;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            } */
            return enemyUnit;
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
        ~MeleeUnit()
        {
            Health = 0;
        }

        // Override method to return a ToString
        public override string toString()
        {
            string meleeUnit = "";
            meleeUnit += "Name: " + Name + "\n" + "X-Coordinate: " + Pos_X + "\n" + "Y-Coordinate: " + Pos_Y + "\n";
            meleeUnit += "Health: " + Health + "\n" + "Speed: " + Speed + "\n" + "Attack: " + Attack + "\n";
            meleeUnit += "Attack Range: " + AttackRange + "\n" + "Team: " + Team + "\n" + "Symbol: " + Symbol + "\n" + "Attacking: " + IsAttacking + "\n";
            return meleeUnit;
        }

    }
}
