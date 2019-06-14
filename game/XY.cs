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
        public XY()
        {
            IsAlive = false;
        }
        public XY(int x, int y,char image)
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
        public void Creature(LivingObject creature,int x)
        {
            this.creature = creature;
            if (x == 1) IsAlive = true;
            else if (x == 0) IsAlive = false;
            else Console.Write("Error");
        }
        public void Creature(int x)
        {
            creature = new LivingObject();
            if (x == 0) IsAlive = false;
            else Console.Write("Error");
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
