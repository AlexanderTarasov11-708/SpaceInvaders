namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * Boss - босс в финальной волне
     */
    class Boss : Alien
    {
        public Boss(int y, int x) : base(y, x)
        {
            X = x;
            Y = y;
        }
    }
}
