using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jordan_van_Zyl___GADE___Task_2
{
    class ResourceBuilding : Building
    {
        // Private fields
        private string resourceType;
        private int resourcePerTick = 2;
        private int resourceRemaining;

        // ResourceBuilding array
        ResourceBuilding[] arrRBuilding = new ResourceBuilding[10];

        // Map object
        Map map;

        public ResourceBuilding[] ArrRBuilding { get => arrRBuilding; set => arrRBuilding = value; }

        // ResourceBuilding constructor that inherits from Building class
        public ResourceBuilding(int pos_X, int pos_Y, int health, string team, string symbol) : base(pos_X, pos_Y, health, team, symbol)
        {

        }

        // Override death method
        public override bool Death(Building rBuilding)
        {
            if (rBuilding.Health <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to generate and remove resources
        public void ManageResources(int GameTick)
        {
            Random rnd = new Random();
            if(GameTick % resourcePerTick == 0)
            {
                bool flag = false;
                while(flag == false)
                {
                    int pos_X = rnd.Next(0, 20);
                    int pos_Y = rnd.Next(0, 20);
                    int health = 100;
                    int teamRoll = rnd.Next(1, 3);
                    string team = "";
                    string symbol = "";

                    switch (teamRoll)
                    {
                        case 1:
                            {
                                team = "Hero";
                                symbol = "B";
                            }
                            break;
                        case 2:
                            {
                                team = "Villain";
                                symbol = "b";
                            }
                            break;
                    }

                    int type = rnd.Next(1, 3);
                    switch (type)
                    {
                        case 1:
                            {
                                resourceType = "Health";
                            }
                            break;
                        case 2:
                            {
                                resourceType = "Extra damage";
                            }
                            break;
                    }

                    if (map.ArrMap[pos_Y, pos_X] == ".")
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }

                    if(flag == true)
                    {
                        ResourceBuilding rBuilding = new ResourceBuilding(pos_X, pos_Y, health, team, symbol);
                        map.ArrMap[pos_Y, pos_X] = symbol;
                        if(GameTick == 2)
                        {
                            ArrRBuilding[0] = rBuilding;
                        }
                        else
                        {
                            ArrRBuilding[GameTick / 2] = rBuilding;
                        }
                        resourceRemaining++;
                    }
                }
            }
            for(int i = 0; i < ArrRBuilding.Length; i++)
            {
                if(Death(ArrRBuilding[i]) == true)
                {
                    map.ArrMap[ArrRBuilding[i].Pos_Y, ArrRBuilding[i].Pos_X] = ".";
                    ArrRBuilding[i] = null;
                    resourceRemaining--;
                }
            }

        }

        // Override toString method
        public override string toString()
        {
            string s = "";
            s += resourceType + "," + Pos_X + "," + Pos_Y + "," + Health + "," + Team + "," + Symbol + "\n";
            return s;
        }

        // Override Save method
        public override void Save()
        {
            if (Directory.Exists("saves") != true)
            {
                Directory.CreateDirectory("saves");
                Console.WriteLine("Directory created!");
            }

            FileStream file = new FileStream("saves/ResourceBuilding.file", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(toString());
            writer.Close();
            file.Close();
        }
    }
}
