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
        private bool deathStatus = true;
        public XY being { get; protected set; }
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

        
        public void Attack (Dungeon dung, LivingObject enemy)
        {
            if (enemy == null)
            {
            }
            else
            {
                if (enemy.Arm - Atk > 0)
                {
                    enemy.Hp = enemy.Hp + enemy.Arm - Atk;
                    if (enemy.Hp >= 0)
                    {
                        if (Arm - enemy.Atk > 0)
                            Hp = Hp + Arm - enemy.Atk;
                        else if (Arm - enemy.Atk <= 0)
                            Hp = Hp - 1;
                    }
                }
                else if (enemy.Arm - Atk <= 0)
                {
                    enemy.Hp = enemy.Hp - 1;
                    if (enemy.Hp >= 0)
                    {
                        if (Arm - enemy.Atk > 0)
                            Hp = Hp + Arm - enemy.Atk;
                        else if (Arm - enemy.Atk <= 0)
                            Hp = Hp - 1;
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
            return deathStatus;
        }
        public void Die(Dungeon dung)
        {
            being.Die();
            deathStatus = true;
            dung.Change(being);
            
        }
    }
}
