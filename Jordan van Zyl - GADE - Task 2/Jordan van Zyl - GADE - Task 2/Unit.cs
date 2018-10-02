using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    public abstract class Unit
    {
        // Protected variables for the unit class
        protected string name;
        protected int pos_X;
        protected int pos_Y;
        protected int health;
        protected int speed;
        protected int attack;
        protected int attackRange;
        protected string team;
        protected string symbol;
        protected bool isAttacking;

        // Accessor methods for the properties of the RangedUnit class
        public int Pos_X { get => pos_X; set => pos_X = value; }
        public int Pos_Y { get => pos_Y; set => pos_Y = value; }
        public int Health { get => health; set => health = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Attack { get => attack; set => attack = value; }
        public int AttackRange { get => attackRange; set => attackRange = value; }
        public string Team { get => team; set => team = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
        public string Name { get => name; set => name = value; }

        // Unit class constructor to read in values to the base class
        public Unit(string name, int pos_X, int pos_Y, int health, int speed, int attack, int attackRange, string team, string symbol, bool isAttacking)
        {
            this.name = name;
            this.pos_X = pos_X;
            this.pos_Y = pos_Y;
            this.health = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.team = team;
            this.symbol = symbol;
            this.isAttacking = isAttacking;
        }

        // Method to move the unit to a new position
        public abstract void newPosition(int new_X, int new_Y);

        // Method use to handle combat with another unit
        public abstract void combat(Unit unit);

        // Method used to determine whether unit is within attacking range
        public abstract bool withinAtkRange(Unit unit);

        // Method to return the position of the closest unit
        public abstract string closestUnit(Unit[] unit);

        // Method to return attacking status
        public abstract bool attackState(bool isAttacking);

        // Method to handle death/destruction of the unit
        ~Unit()
        {

        }

        // ToString method to display all unit's information
        public abstract string toString();

        // Save method
        public abstract void Save();
    }
}
