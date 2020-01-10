using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{
    public class Entity
    {
        protected Point position;
        protected int size;
        protected Color color;

        public Entity()
        {

        }
        public Entity(Point position, int size,Color color)
        {
            this.position = position;
            this.size = size;
            this.color = color;
        }
        public int GetSize()
        {
            return size;
        }

        public Point GetPosition()
        {
            return position;
        }
        public Color GetColor()
        {
            return color;
        }

    }
}
