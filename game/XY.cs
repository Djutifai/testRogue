using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class XY 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAlive { get; set; }
        private LivingObject creature;
        public XY()  // non-creature constructor (using in the mapArray)
        {
            IsAlive = false;
        }
        public XY(int x, int y,char image) //creature constructor
        {
            X = x;
            Y = y;
            IsAlive = false;
        }
       
        public void SetCoordinate(int x,int y)
        {
            X = x;
            Y = y;
        }
        public void AddTo(int x,int y)
        {
            X += x;
            Y += y;
        }
        public void Creature(LivingObject creature)
        {
            this.creature = creature;
            IsAlive = true;
        }
        public LivingObject GetCreature()
        {
            return creature;
        }
        public void Die()
        {
            IsAlive = false;
            
        }

    }
}
