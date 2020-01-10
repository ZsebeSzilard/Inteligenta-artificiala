using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{
    public class FurnitureStatus
    {
        private Furniture furniture;
        public bool IsAvailable()
        {
            if (furniture.GetMaxCapacity() > furniture.GetCurrentNumberOfUsers())
            {
                return true;
            }
            return false;
        }
        public void SetFurniture(Furniture furniture)
        {
            this.furniture = furniture;
        }
        public int GetMaxCapacity()
        {
            return furniture.GetMaxCapacity();
        }
        public Point GetPosition()
        {
            return furniture.GetPosition();
        }
        public float GetRefillPower()
        {
            return furniture.GetRefillPower();
        }
        public int GetCurrentUsers()
        {
            return furniture.GetCurrentNumberOfUsers();
        }

        public void IncreaseNumberOfUsers()
        {
            furniture.IncreaseNumberOfUsers();
        }
        public void DecreaseNumberOfUsers()
        {
            furniture.DecreaseNumberOfUsers();
        }
    }
}
