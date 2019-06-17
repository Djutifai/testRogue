using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Player : LivingObject
    {
        private Movement pl = new Movement();
        public Player (int x, int y) : base("Boy",10,5,2,'@')
        {
            being = new XY(x, y, Image)
            {
                IsAlive = true
            };
        }

        public void Move(Dungeon dung)
        {
            pl.Move(being, dung, this);
        }
        public XY Being()
        {
            return being;
        }
        public bool IsAlive()
        {
            return being.IsAlive;
        }
        
    }
}
