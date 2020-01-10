using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{
    public enum FurnitureType
    {
        Fridge,
        Bed,
        Toilet,
        Television 
    }
    public class Furniture:Entity
    {
        FurnitureType furnitureType;
        int maxCapacity;
        int currentNumberOfUsers;
        float refillPower;

        public Furniture(FurnitureType furnitureType, Point position, int size, Color color) :base(position,size,color)
        {
            this.furnitureType = furnitureType;
            currentNumberOfUsers = 0;
            maxCapacity = SetMaxCapacity(furnitureType);
            refillPower = SetRefillPower(furnitureType);
        }
        private float SetRefillPower(FurnitureType furnitureType)
        {
            if (furnitureType == FurnitureType.Bed)
                return 5;
            if (furnitureType == FurnitureType.Fridge)
                return 10;
            if (furnitureType == FurnitureType.Television)
                return 5;
            if (furnitureType == FurnitureType.Toilet)
                return 10;
            return 1;
        }
        private int SetMaxCapacity(FurnitureType furnitureType)
        {
            if (furnitureType == FurnitureType.Bed)
                return 2;
            if (furnitureType == FurnitureType.Fridge)
                return 1;
            if (furnitureType == FurnitureType.Television)
                return 2;
            if (furnitureType == FurnitureType.Toilet)
                return 1;
            return 1;
        }
        public static Point GenerateFurniturePosition(List<Point> furniturePositions)
        {
            Point newlocation = new Point(0, 0);
            int size = 50;
            bool isValid = false;
            while (isValid == false)
            {
                isValid = true;
                newlocation = new Point(Engine.rnd.Next(size / 2, Engine.GetBitmapWidth() - size / 2), Engine.rnd.Next(size / 2, Engine.GetBitmapHeight() - size / 2));
                foreach (Point point in furniturePositions)
                {
                    if (Calculator.GetDistance(point, newlocation) < size*2)
                    {
                        isValid = false;
                    }
                }
            }
            return newlocation;
        }
        public void IncreaseNumberOfUsers()
        {
            currentNumberOfUsers++;
        }
        public void DecreaseNumberOfUsers()
        {
            currentNumberOfUsers--;
        }
        public FurnitureType GetFurnitureType()
        {
            return furnitureType;
        }
        public int GetMaxCapacity()
        {
            return maxCapacity;
        }
        public int GetCurrentNumberOfUsers()
        {
            return currentNumberOfUsers;
        }
        public float GetRefillPower()
        {
            return refillPower;
        }

    }
}
