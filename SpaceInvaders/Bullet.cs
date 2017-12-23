namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * Bullet - пуля игрока
     */
    class Bullet : Creature
    {
        public int X;
        public int Y;
        public bool Hit = false;

        public Bullet(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (!Conflict())
            {
                Game.Map[Y, X] = null;
                Y--;
                if (!(Game.Map[Y, X] is Border) && !(Game.Map[Y, X] is Alien) && !(Game.Map[Y, X] is AlienBullet))
                    Game.Map[Y, X] = this;
            }
        }

        //обнаружение конфликта с другими ячейками карты
        public bool Conflict()
        {
            if (Game.Map[Y - 1, X] is AlienBullet)
            {
                Game.Map[Y, X] = null;
                Game.Map[Y - 1, X] = null;
                return true;
            }
            if (Game.Map[Y - 1, X] is Boss)
            {
                Game.Map[Y, X] = null;
                Game.bossHP -= 10;
                return true;
            }
            else if (Game.Map[Y - 1, X] is Alien)
            {
                Game.Map[Y, X] = null;
                Game.Map[Y - 1, X] = null;
                Game.yourPoints += 10;
                return true;
            }
            return false;
        }
    }
}
