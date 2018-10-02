using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jordan_van_Zyl___GADE___Task_2
{
    public partial class Form1 : Form
    {
        Map map = new Map();
        GameEngine engine = new GameEngine();

        bool flag;
        public bool Flag { get => flag; set => flag = value; }

        public Form1()
        {
            InitializeComponent();

            map.newBattlefield();
            lblMap.Text = map.redraw();
        }

        // Boolean method to return status of the game (paused/unpaused)
        public bool gameStatus()
        {
            bool status = false;
            if (Flag == true)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        // Class objects
        MeleeUnit m_Unit;
        RangedUnit r_Unit;
        FactoryBuilding f_Building;
        ResourceBuilding r_Building;


        private void btnUp_Click(object sender, EventArgs e)
        {

        }

        private void btnRight_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDown_Click(object sender, EventArgs e)
        {

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

        }

        // Timer and game control
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(gameStatus() == true)
            {
                engine.updateMap(time);
                map.TimerTick1 = time;
                //r_Building.ManageResources(time);
                // f_Building.SpawnUnits(time);
                time++;
                lblTime.Text = "" + time;
                lblMap.Text = map.redraw();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Flag = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Flag = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("saves") != true)
            {
                Directory.CreateDirectory("saves");
                Console.WriteLine("Direcotry created!");
            }
            else
            {
                Console.WriteLine("Directory already exists!");
            }

            map.Save();
            // f_Building.Save();
            // r_Building.Save();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            map.Read();
        }
    }
}
