using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Player : LivingObject
    {
        public Player (int x, int y) : base("Boy",10,5,2,'@')
        {
            Being = new XY(x, y, Image)
            {
                IsAlive = true
            };
        }

        public void Move(Dungeon dung)
        {
            PlMove(dung, this);
        }
        public bool IsAlive()
        {
            return Being.IsAlive;
        }
        
    }
}
