using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___18013347___GADE_POE
{
    class ResourceBuilding : Building
    {
        // Private fields
        private string resourceType;
        private int maxGeneration;
        private int generationPerTick;
        private int generatedResources;

        // ResourceBuilding constructor that inherits from Building class
        public ResourceBuilding(int pos_X, int pos_Y, int health, string team, string symbol, string resourceType, int maxGeneration, int generationPerTick) : base(pos_X, pos_Y, health, team, symbol)
        {
            this.resourceType = resourceType;
            this.maxGeneration = maxGeneration;
            this.generationPerTick = generationPerTick;
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

        public string Team
        {
            get
            {
                return team;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }
        }

        public string ResourceType
        {
            get
            {
                return resourceType;
            }
        }


        public int MaxGeneration
        {
            get
            {
                return maxGeneration;
            }
        }

        public int GenerationPerTick
        {
            get
            {
                return generationPerTick;
            }
        }

        public int GeneratedResources
        {
            get
            {
                return generatedResources;
            }
            set
            {
                generatedResources = value;
            }
        }

        // Override death method
        public override bool Death()
        {
            if (this.health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to generate and remove resources
        public void Generate(int GameTick)
        {
            if(GameTick % this.generationPerTick == 0)
            {
                int max = maxGeneration;
                if (generatedResources != 40)
                {
                    max -= 1;
                    generatedResources += 1;
                }
            }
        }

        // Override toString method
        public override string toString()
        {
            string s = "";
            s += "I can generate " + maxGeneration + " " + resourceType + ". I have " + (maxGeneration - generatedResources) + " resources remaining,";
            s += " so I have generated " + generatedResources + " " + resourceType + "\n";
            return s;
        }
    }

    }
