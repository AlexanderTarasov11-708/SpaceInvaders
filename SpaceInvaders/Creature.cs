﻿namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * Creature - интерфейс существо, расположенное в клетке карты 
     */
    public interface Creature
    {
        void Act();
        bool Conflict();
    }
}
