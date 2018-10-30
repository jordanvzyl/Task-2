using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    public abstract class Building
    {
        // Protected field definitions
        protected int pos_X;
        protected int pos_Y;
        protected int health;
        protected string team;
        protected string symbol;

        // Paramterised constructor
        public Building(int pos_X, int pos_Y, int health, string team, string symbol)
        {
            this.pos_X = pos_X;
            this.pos_Y = pos_Y;
            this.health = health;
            this.team = team;
            this.symbol = symbol;
        }

        // Method to handle death
        public abstract bool Death();

        // ToString method to display building information
        public abstract string toString();


    }
}
