using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sims
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Initialize(pictureBox1,label1);
            foreach (FurnitureType furnitureType in Enum.GetValues(typeof(FurnitureType)))
                comboBox1.Items.Add(furnitureType);
            comboBox1.SelectedIndex = 0;
            frameUpdateTimer.Start();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Engine.AddHuman();
            pictureBox1.Image = Engine.GetBitmap();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Engine.AddFurniture();
            pictureBox1.Image = Engine.GetBitmap();
        }

        private void frameUpdateTimer_Tick(object sender, EventArgs e)
        {
            Engine.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FurnitureType selectedFurnitureType = (FurnitureType)comboBox1.SelectedItem;
            Color color=Color.Black;
            if (selectedFurnitureType == FurnitureType.Bed)
                color = Color.Red;
            else if (selectedFurnitureType == FurnitureType.Fridge)
                color = Color.Blue;
            else if(selectedFurnitureType == FurnitureType.Television)
                color = Color.Purple;
            else if(selectedFurnitureType == FurnitureType.Toilet)
                color = Color.Green;
            Furniture furniture = new Furniture(selectedFurnitureType, Furniture.GenerateFurniturePosition(Engine.GetFourniturePositions()), 50, color);
            
            Engine.AddFurniture(furniture);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Engine.RemoveHumans();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Engine.RemoveFurnitures();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 5; i++)
            {
                Engine.AddHuman();
            }
            pictureBox1.Image = Engine.GetBitmap();
        }
    }
}
