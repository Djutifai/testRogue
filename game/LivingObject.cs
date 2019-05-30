using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class LivingObject
    {
        public string Name { get; set; }
        public int Hp { get; set;}
        public int Atk { get; set; }
        public int Arm { get; set; }
        public char Image { get; set; }
        public XY being;
        public LivingObject (string name,int hp,int attack,int armor,char image)
        {
            Name = name;
            Hp = hp;
            Atk = attack;
            Arm = armor;
            Image = image;
        }
        public LivingObject()
        {
        }
        public XY GetCoordinates()
        {
            return being;
        }
        public int X => being.X;
        public int Y => being.Y;
        private void Die()
        {
            //being.Die();
        }
        public void Attack (LivingObject enemy)
        {
            if (enemy.Arm < Atk)
                enemy.Hp = enemy.Hp + enemy.Arm - Atk;
            if (enemy.Hp != 0)
            {
                if (enemy.Atk > Arm)
                    Hp = Hp + Arm - enemy.Atk;
            }
            else
            {

            }
        }
    }
}
