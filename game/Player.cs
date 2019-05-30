using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Player : LivingObject
    {
        private Movement _move = new Movement();
        public Player (int x, int y) : base("Boy",10,5,2,'@')
        {
            being = new XY(x, y, Image)
            {
                IsAlive = true
            };
        }


        public void Move(Dungeon dung)
        {
            _move.Move(being, dung, this);
        }
        
        
    }
}
