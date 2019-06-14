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
        public void Ai (Dungeon dung, Player player)
        {
           
            if (0 <Math.Abs(player.X-being.X)&&0<Math.Abs(player.Y-being.Y)) 
            {
                AiCheck(dung, player);
            }
        }

        public void Die()
        {
            being.Die();
            
        }

        private void AiCheck(Dungeon dung, Player player)
        {
            if (being.X > player.X && being.Y > player.Y)
            {
                if (dung.CreatureCheck(being.X - 1, being.Y - 1)) { dung.Change(being.X, being.Y); being.AddTo(-1, -1); dung.Change(being.X, being.Y); being.Creature(this,1); }
            }
            else if (being.X < player.X && being.Y < player.Y)
            {
                if (dung.CreatureCheck(being.X + 1, being.Y + 1)){ dung.Change(being.X, being.Y);
                    { dung.Change(being.X, being.Y); being.AddTo(1, 1); dung.Change(being.X, being.Y); being.Creature(this,1);
                    }
                }
                dung.Change(being.X, being.Y); }
            else if (being.X > player.X && being.X < player.Y)
            {
                if (dung.CreatureCheck(being.X - 1, being.Y + 1))
                        { dung.Change(being.X, being.Y); being.AddTo (-1, +1); dung.Change(being.X, being.Y); being.Creature(this,1); }
            }
            else if (being.X < player.X && being.Y > player.Y)
            {
                if (dung.CreatureCheck(being.X + 1, being.Y - 1))
                            { dung.Change(being.X, being.Y); being.AddTo(+1, -1); dung.Change(being.X, being.Y); being.Creature(this,1); }
            }
           
        }
       
    }
}
