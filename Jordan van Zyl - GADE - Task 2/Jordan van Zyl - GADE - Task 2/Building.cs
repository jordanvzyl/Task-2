using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    public abstract class Building
    {
        // Protected field definitions
        protected int pos_X;
        protected int pos_Y;
        protected int health;
        protected string team;
        protected string symbol;

        // Accessor methods for fields of Building class
        public int Pos_X { get => pos_X; set => pos_X = value; }
        public int Pos_Y { get => pos_Y; set => pos_Y = value; }
        public int Health { get => health; set => health = value; }
        public string Team { get => team; set => team = value; }
        public string Symbol { get => symbol; set => symbol = value; }

        // Paramterised constructor
        public Building(int pos_X, int pos_Y, int health, string team, string symbol)
        {
            this.Pos_X = pos_X;
            this.Pos_Y = pos_Y;
            this.Health = health;
            this.Team = team;
            this.Symbol = symbol;
        }

        // Method to handle death
        public abstract bool Death(Building building);

        // ToString method to display building information
        public abstract string toString();

        // Save method
        public abstract void Save();
    }
}
