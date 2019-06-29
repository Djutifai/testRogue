using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Enemy : LivingObject
    {
        private Random rand = new Random();
        private byte temp;

        public Enemy(int x, int y, string name, int hp, int attack, int armor, char image)
        {
            Name = name;
            Hp = hp;
            Atk = attack;
            Arm = armor;
            Image = image;
            Being = new XY(x, y, image)
            {
                IsAlive = true
            };
        }

        public void Ai(Dungeon dung, Player player)
        {
            if (1 == Math.Abs(player.X - Being.X) && 1 == Math.Abs(player.Y - Being.Y))
            {
                Attack(dung, player, 0);
            }
            else if (5 > Math.Abs(player.X - Being.X) || 5 > Math.Abs(player.Y - Being.Y))
            {
                MoveToPl(dung, player);
            }
            else if (5 < Math.Abs(player.X - Being.X) || 5 < Math.Abs(player.Y - Being.Y))
            {
                RandBehave(dung);
            }
        }

        private void MoveToPl(Dungeon dung, Player player)
        {
            temp = (byte)rand.Next(2);
            if (temp == 0)
            {
                if (Being.X > player.X)
                {
                    Move(-1, 0, dung, this);
                }
                else if (Being.X < player.X)
                {
                    Move(+1, 0, dung, this);
                }
                else if (Being.X==player.X)
                {
                    if (Being.Y > player.Y)
                        Move(0, -1, dung, this);
                    else if (Being.Y < player.Y)
                        Move(0, 1, dung, this);
                }
            }
            else if (temp == 1)
            {
                if (Being.Y > player.Y)
                {
                    Move(0, -1, dung, this);
                }

                else if (Being.Y < player.Y)
                {
                    Move(0, +1, dung, this);
                }
                else if (Being.Y==player.Y)
                {
                    if (Being.X > Being.X)
                        Move(-1, 0, dung, this);
                    else if (Being.X > Being.Y)
                        Move(1, 0, dung, this);

                }
            }
        }
        private void RandBehave(Dungeon dung)
        {
            temp = (byte)rand.Next(4);
            if (temp == 0)
                Move(1, 0, dung, this);
            else if (temp == 1)
                Move(-1, 0, dung, this);
            else if (temp == 2)
                Move(0, 1, dung, this);
            else if (temp == 3)
                Move(0, -1, dung, this);
        }


    }
}