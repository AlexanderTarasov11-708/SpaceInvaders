namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * Border - границы карты
     */
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
