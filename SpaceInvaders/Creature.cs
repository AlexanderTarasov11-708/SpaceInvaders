using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public interface Creature
    {
        //если true, то игра продолжается
        //если false, конец
        void Act();
        bool Conflict();
    }
}
