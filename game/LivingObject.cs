using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class LivingObject : Movement
    {
        public string Name { get; set; }
        public int Hp { get; set;}
        public int Atk { get; set; }
        public int Arm { get; set; }
        public char Image { get; set; }
        private bool isDead = false;
        public XY Being { get; protected set; }
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
            return Being;
        }
        public int X => Being.X;
        public int Y => Being.Y;
        /// <summary>
        /// Attack method
        /// </summary>
        /// <param name="dung"></param>
        /// <param name="enemy">target of the attack</param>
        /// <param name="type">If 0 - attack without taking damage and 1 for a classic combat (attack -> take damage) </param>
        public void Attack (Dungeon dung, LivingObject enemy, byte type)
        {
            if (enemy == null) {}
            else
            {
                if (enemy.Arm - Atk < 0)
                {
                    enemy.Hp = enemy.Hp + enemy.Arm - Atk;
                }
                else if (enemy.Arm - Atk >= 0)
                {
                    enemy.Hp = enemy.Hp - 1;
                }
                if (type == 1)
                {
                    if (enemy.Hp >= 0)
                    {
                        if (Arm - enemy.Atk >= 0)
                            Hp = Hp - 1;
                        else if (Arm - enemy.Atk < 0)
                            Hp = Hp + Arm - enemy.Atk;
                    }
                }
                if (enemy.Hp <= 0)
                { 
                    enemy.Die(dung);
                }
            }
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void Die(Dungeon dung)
        {
            Being.Die();
            isDead = true;
            dung.Change(Being);
            
        }
        
    }
}
