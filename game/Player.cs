﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Player : LivingObject
    {
        private Movement move = new Movement();
        public Player (int x, int y) : base("Boy",10,5,2,'@')
        {
            being = new XY(x, y, Image)
            {
                IsAlive = true
            };
        }


        public void Move(Dungeon dung)
        {
            move.Move(being, dung, this);
        }
        
        public bool IsAlive()
        {
            return being.IsAlive;
        }
        
    }
}
