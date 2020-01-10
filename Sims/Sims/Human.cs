using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{

    public class Human:Entity
    {

        //needs
        float hunger;
        float energy;
        float bladder;
        float fun;
        float speed;
        bool isDecided;
        bool isRefilling;
        bool isAlive;
        FurnitureType decision;
        FurnitureStatus destination;
        float towardX;
        float towardY;

        public Human()
        {

        }
        public Human(Point position,int size,Color color):base(position,size,color)
        {
            hunger = 100;
            energy = 100;
            bladder = 100;
            fun = 100;
            speed = 20;
            isDecided = false;
            isRefilling = false;
            isAlive = true;
        }
        
        public void Move()
        {
            if (!isDecided)
            {
                MakeDecision();
                SearchForClosest(this.decision);
                MoveTowardTarget();
            }

            
            if (isRefilling)
            {
                Refill();
                
                if (energy >= 100)
                {
                    energy = 100;
                    isRefilling = false;
                    isDecided = false;
                    destination.DecreaseNumberOfUsers();
                    destination = null;
                }
                else if (fun >= 100)
                {
                    fun = 100;
                    isRefilling = false;
                    isDecided = false;
                    destination.DecreaseNumberOfUsers();
                    destination = null;
                }
                else if (bladder >= 100)
                {
                    bladder = 100;
                    isRefilling = false;
                    isDecided = false;
                    destination.DecreaseNumberOfUsers();
                    destination = null;
                }
                else if (hunger >= 100)
                {
                    hunger = 100;
                    isRefilling = false;
                    isDecided = false;
                    destination.DecreaseNumberOfUsers();
                    destination = null;
                }
            }
            else
            {
                if (destination != null)
                {
                    if (Calculator.GetDistance(this.GetPosition(), destination.GetPosition()) < 50)
                    {
                        if (destination.IsAvailable())
                        {
                            isRefilling = true;
                            destination.IncreaseNumberOfUsers();
                        }
                    }
                    else
                    {
                        this.position.X += (int)towardX;
                        this.position.Y += (int)towardY;
                    }
                }
                else
                {
                    isDecided = false;
                }
            }
            ConsumeResources();
        }

        private void Refill()
        {
            if (decision == FurnitureType.Bed)
            {
                energy += destination.GetRefillPower();
            }
            if (decision == FurnitureType.Fridge)
            {
                hunger += destination.GetRefillPower();
            }
            if (decision == FurnitureType.Television)
            {
                fun += destination.GetRefillPower();
            }
            if (decision == FurnitureType.Toilet )
            {
                bladder += destination.GetRefillPower();
            }
        }
        private void MoveTowardTarget()
        {
            if (destination != null)
            {
                float dx = destination.GetPosition().X - position.X;
                float dy = destination.GetPosition().Y - position.Y;
                Normalize(ref dx, ref dy);
                towardX = dx * speed;
                towardY = dy * speed;
            }
            

        }
        void Normalize(ref float x, ref float y)
        {
            if (Math.Abs(x) >= Math.Abs(y))
            {
                y = y / Math.Abs(x);
                x = Math.Abs(x) / x;
            }
            else
            {
                x = x / Math.Abs(y);
                y = Math.Abs(y) / y;
            }
        }

        private void SearchForClosest(FurnitureType decision) 
        {
            List<FurnitureStatus>furnitureStats=Engine.GetFournitureStats(decision);
            if (furnitureStats.Count > 0)
            {
                destination = furnitureStats[0];
                for(int i=1;i<furnitureStats.Count;i++)
                {
                    if(Calculator.GetDistance(this.GetPosition(),destination.GetPosition())> Calculator.GetDistance(this.GetPosition(), furnitureStats[i].GetPosition())&&destination.IsAvailable())
                    {
                        destination = furnitureStats[i];
                    }
                }

            }
            
        }

        public void MakeDecision()
        {
            FurnitureType[] furnitures = new FurnitureType[4];
            furnitures[0] = FurnitureType.Bed;
            furnitures[1] = FurnitureType.Fridge;
            furnitures[2] = FurnitureType.Television;
            furnitures[3] = FurnitureType.Toilet;
            float[] status = new float[4];
            status[0] = GetEnergy();
            status[1] = GetHunger();
            status[2] = GetFun();
            status[3] = GetBladder();

            bool whereSwaps = true;
            while (whereSwaps)
            {
                whereSwaps = false;
                for (int i=0;i<3;i++)
                    if(status[i]<status[i+1])
                    {
                        float aux1 = status[i];
                        status[i] = status[i + 1];
                        status[i + 1] = aux1;

                        FurnitureType aux2 = furnitures[i];
                        furnitures[i] = furnitures[i + 1];
                        furnitures[i + 1] = aux2;
                        whereSwaps = true;
                    }
            }
            FurnitureType[] ponders = new FurnitureType[10];
            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    ponders[k] = furnitures[i];
                    k++;
                }
            }
            this.decision = ponders[Calculator.GetRandom(10)];

            this.isDecided = true;
            
        }
        public void ConsumeResources()
        {
            if (hunger > 0)
                hunger -= 0.19f;//0.16
            else
                Die();
            if (energy > 0)
                energy -= 0.18f;//0,14
            else
                Die();
            if (bladder > 0)
                bladder -= 0.15f;
            else
                Die();
            if (fun > 0)
                fun -= 0.20f;//0.15
            else
                Die();
        }
        public void StopFromActions()
        {
            isDecided=false;
            isRefilling=false;
            destination=null;
        }
        public void Die()
        {
            isAlive = false;
            if (isRefilling)
            {
                destination.DecreaseNumberOfUsers();
            }
        }
        public float GetHunger()
        {
            return hunger;
        }
        public float GetEnergy()
        {
            return energy;
        }
        public float GetBladder()
        {
            return bladder;
        }
        public float GetFun()
        {
            return fun;
        }
        public bool IsAlive()
        {
            return isAlive;
        }
        public FurnitureType GetDecision()
        {
            return this.decision;
        }
    }
}
