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
        public static Random rnd;
        static List<Entity> entities = new List<Entity>();
        static Label label1;
        static int deathcounter;
        public static void Initialize(PictureBox pictureBox,Label label)
        {
            rnd = new Random();
            InitializeDrawEngine(pictureBox);
            label1 = label;
            deathcounter = 0;
        }

        

        public static void AddHuman()
        {
            int humanSize = 25;
            Point humanPosition = new Point(rnd.Next(humanSize / 2, Engine.GetBitmapWidth() - humanSize / 2), rnd.Next(humanSize / 2, Engine.GetBitmapHeight() - humanSize / 2));
            Color humanColor = Color.Orange;
            Human human = new Human(humanPosition,humanSize,humanColor);
            entities.Add(human);
            DrawFrame();
        }
        public static void AddFurniture()
        {
            List<Furniture> furnitures = new List<Furniture>();
            foreach(Entity entity in entities)
            {
                if(entity is Furniture)
                {
                    furnitures.Add((Furniture)entity);
                }
            }
            if (GetFurnitureCount() + 4 < 22)
            {
                Furniture furniture1 = new Furniture(FurnitureType.Fridge, Furniture.GenerateFurniturePosition(GetFourniturePositions()), 50, Color.Blue);
                entities.Add(furniture1);
                Furniture furniture2 = new Furniture(FurnitureType.Bed, Furniture.GenerateFurniturePosition(GetFourniturePositions()), 50, Color.Red);
                entities.Add(furniture2);
                Furniture furniture3 = new Furniture(FurnitureType.Toilet, Furniture.GenerateFurniturePosition(GetFourniturePositions()), 50, Color.Green);
                entities.Add(furniture3);
                Furniture furniture4 = new Furniture(FurnitureType.Television, Furniture.GenerateFurniturePosition(GetFourniturePositions()), 50, Color.Purple);
                entities.Add(furniture4);
            }
            
        }
        public static void AddFurniture(Furniture furniture)
        {
            if(GetFurnitureCount() + 1 < 22)
                entities.Add(furniture);
        }
        public static void RemoveHumans()
        {
            List<Human> humans = new List<Human>();
            foreach (Entity entity in entities)
            {
                if (entity is Human)
                {
                    humans.Add((Human)entity);
                }
            }
            foreach (Human human in humans)
            {
                human.Die();
            }
        }
        public static void RemoveFurnitures()
        {
            List<Furniture> furnitures = new List<Furniture>();
            List<Human> humans = new List<Human>();
            foreach (Entity entity in entities)
            {
                if (entity is Furniture)
                {
                    furnitures.Add((Furniture)entity);
                }
                else if(entity is Human)
                {
                    humans.Add((Human)entity);
                }
            }
            foreach (Furniture furniture in furnitures)
            {
                entities.Remove(furniture);
            }
            foreach(Human human in humans)
            {
                human.StopFromActions();
            }

        }
        public static int GetFurnitureCount()
        {
            int counter = 1;
            foreach (Entity entity in entities)
            {
                if (entity is Furniture)
                {
                    counter++;
                }
            }
            return counter;
        }

        public static List<Point> GetFourniturePositions()
        {
            List<Point> furniturePoints = new List<Point>();

            foreach (Entity entity in entities)
            {
                if(entity is Furniture)
                {
                    Furniture furniture = (Furniture)entity;
                    furniturePoints.Add(furniture.GetPosition());
                }
                
            }
            return furniturePoints;
        }

        public static List<Point> GetFourniturePositions(FurnitureType furnitureType)
        {
            List<Point> furniturePoints = new List<Point>();

            foreach (Entity entity in entities)
            {
                if (entity is Furniture)
                {
                    Furniture furniture = (Furniture)entity;
                    if(furniture.GetFurnitureType()==furnitureType)
                    {
                        furniturePoints.Add(furniture.GetPosition());
                    }
                }

            }
            return furniturePoints;
        }


        public static List<FurnitureStatus> GetFournitureStats(FurnitureType furnitureType)
        {
            List<FurnitureStatus> furnitureStats = new List<FurnitureStatus>();

            foreach (Entity entity in entities)
            {
                if (entity is Furniture)
                {
                    Furniture furniture = (Furniture)entity;
                    if (furniture.GetFurnitureType() == furnitureType)
                    {
                        FurnitureStatus furnitureStatus = new FurnitureStatus();
                        furnitureStatus.SetFurniture(furniture);
                        furnitureStats.Add(furnitureStatus);
                    }
                }

            }
            return furnitureStats;
        }

        public static void Update()
        {
            int humancount = 0;
            CheckForDeaths();
            Human testHuman=new Human();
            foreach (Entity entity in entities)
            {
                if (entity is Human)
                {
                    humancount++;
                    Human human = (Human)entity;
                    human.Move();
                    testHuman = (Human)entity;

                }

            }
            label1.Text = "Number of Humans Alive: " + humancount.ToString()+"\n"+"Number of deaths "+deathcounter.ToString()+"\n"+testHuman.GetDecision().ToString();
            DrawFrame();
        }

        private static void CheckForDeaths()
        {
            for(int i = 0; i < entities.Count; i++)
            {
                    if (entities[i] is Human)
                    {
                        Human human = (Human)entities[i];
                        if (!human.IsAlive())
                        {
                            entities.Remove(entities[i]);
                            i--;
                            deathcounter++;
                        }
                        
                    }
            }
        }
        
    }
}
