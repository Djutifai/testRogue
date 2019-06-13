using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Enemy : LivingObject
    {
        public Enemy (int x,int y,string name, int hp, int attack, int armor, char image) 
        {
            Name = name;
            Hp = hp;
            Atk = attack;
            Arm = armor;
            Image = image;
            being = new XY(x, y, image)
            {
                IsAlive = true
            };  
        }
        public bool Check()
        {
            return being.IsAlive;
        }
        public void Die()
        {
            being.Die();
            
        }
       
    }
}
