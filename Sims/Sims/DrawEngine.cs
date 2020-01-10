using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sims
{
    public static partial class Engine
    {
        static Bitmap bitmap;
        static Graphics graphics;
        static PictureBox pictureBox;
        //static Pen pen;
        //static Brush brush;


        public static void InitializeDrawEngine(PictureBox pictureBox)
        {
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            Engine.pictureBox = pictureBox;
        }

        public static int GetBitmapWidth()
        {
            return bitmap.Width;
        }
        public static int GetBitmapHeight()
        {
            return bitmap.Height;
        }

        public static void DrawFrame()
        {
            graphics.Clear(Color.White);
            foreach(Entity entity in entities)
            {
                if(entity is Human)
                {
                    Human human = (Human)entity;//Human human = entity; -incearca 
                    int size = human.GetSize();
                    graphics.DrawEllipse(new Pen(human.GetColor()), human.GetPosition().X - size / 2, human.GetPosition().Y - size / 2, size, size);
                    graphics.FillEllipse(new SolidBrush(human.GetColor()), human.GetPosition().X - size / 2, human.GetPosition().Y - size / 2, size, size);
                    DrawStatusBars(human);
                    
                }
                if (entity is Furniture)
                {
                    Furniture furniture = (Furniture)entity;//Human human = entity; -incearca 
                    int size = furniture.GetSize();
                    int capacity = furniture.GetMaxCapacity();
                    int numberOfUsers = furniture.GetCurrentNumberOfUsers();
                    graphics.DrawRectangle(new Pen(furniture.GetColor()), furniture.GetPosition().X - size / 2, furniture.GetPosition().Y - size / 2, size, size);
                    graphics.FillEllipse(new SolidBrush(furniture.GetColor()), furniture.GetPosition().X - size / 2, furniture.GetPosition().Y - size / 2, size, size);
                    graphics.DrawString(numberOfUsers.ToString()+"/"+capacity.ToString(),new Font("Arial",10F), new SolidBrush(Color.Black), furniture.GetPosition());
                }
            }
            pictureBox.Image = bitmap;
            
        }
        public static void DrawStatusBars(Human human)
        {
            int humanSize = human.GetSize();
            Point humanPozition = human.GetPosition();

            graphics.FillRectangle(new SolidBrush(Color.Blue), humanPozition.X - humanSize * 2, humanPozition.Y - 7f * humanSize / 2, (float)human.GetHunger() / 100 * humanSize * 4, humanSize / 2);
            graphics.FillRectangle(new SolidBrush(Color.Red), humanPozition.X - humanSize * 2, humanPozition.Y - 5.5f * humanSize / 2, (float)human.GetEnergy() / 100 * humanSize * 4, humanSize / 2);
            graphics.FillRectangle(new SolidBrush(Color.Green), humanPozition.X - humanSize * 2, humanPozition.Y - 4f * humanSize / 2, (float)human.GetBladder() / 100 * humanSize * 4, humanSize / 2);
            graphics.FillRectangle(new SolidBrush(Color.Purple), humanPozition.X - humanSize * 2, humanPozition.Y - 2.5f * humanSize / 2, (float)human.GetFun() / 100 * humanSize * 4, humanSize / 2);
            
        }
        public static Bitmap GetBitmap()
        {
            return bitmap;
        }

    }
}
