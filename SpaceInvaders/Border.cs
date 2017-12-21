using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Border : Creature
    {
        public int X;
        public int Y;

        public Border(int y, int x) { X = x; Y = y; }

        public void Act()
        {
        }

        public bool Conflict()
        {
            return false;
        }
    }
}
