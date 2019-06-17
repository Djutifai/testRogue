using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Enemy : LivingObject
    {
        Random rand = new Random();
        byte temp;
        public Enemy(int x, int y, string name, int hp, int attack, int armor, char image)
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
        public void Ai(Dungeon dung, Player player)
        {

            if (0 < Math.Abs(player.X - being.X) && 0 < Math.Abs(player.Y - being.Y))
                if (5 > Math.Abs(player.X - being.X) && 5 > Math.Abs(player.Y - being.Y))
                {
                    AiCheck(dung, player);
                    MoveToPl(dung, player);
                }
                else if (5 < Math.Abs(player.X - being.X) && 5 < Math.Abs(player.Y - being.Y))
                {
                    RandBehave(dung);
                }
        }


        private void MoveToPl(Dungeon dung, Player player)
        {
            if (being.X > player.X && being.Y > player.Y)
            {
                if (dung.CreatureCheck(being.X - 1, being.Y - 1)) { dung.Change(being); being.AddTo(-1, -1); dung.Change(being); being.Creature(this, 1); Move(-1, -1, dung); }

            }
            else if (being.X < player.X && being.Y < player.Y)
            {
                if (dung.CreatureCheck(being.X + 1, being.Y + 1))
                {
                    dung.Change(being);
                    {
                        dung.Change(being); being.AddTo(1, 1); dung.Change(being); being.Creature(this, 1); Move(1, 1, dung);
                    }
                }
                dung.Change(being);
            }

            else if (being.X > player.X && being.X < player.Y)
            {
                if (dung.CreatureCheck(being.X - 1, being.Y + 1))
                { dung.Change(being); being.AddTo(-1, +1); dung.Change(being); being.Creature(this, 1); Move(-1, 1, dung); }

            }
            else if (being.X < player.X && being.Y > player.Y)
            {
                if (dung.CreatureCheck(being.X + 1, being.Y - 1))
                { dung.Change(being); being.AddTo(+1, -1); dung.Change(being); being.Creature(this, 1); Move(1, -1, dung); }

            }

        }

        private void AiCheck(Dungeon dung, Player player)

        {
            temp = (byte)rand.Next(2);
            if (temp==0)
            {
                if (being.X > player.X)
                {
                    if (dung.CheckTile(being.X-1, being.Y) != SolidTiles.Wall)
                    {
                        if (dung.CreatureCheck(being.X - 1, being.Y))
                        {
                            dung.Change(being);
                            being.X--;
                            dung.Change(being);
                             being.Creature(this, 1);
                        }
                    }
                }
                else if (being.X < player.X)
                {
                    if (dung.CheckTile(being.X+1, being.Y) != SolidTiles.Wall)
                    {
                        if (dung.CreatureCheck(being.X + 1, being.Y))
                        {
                            dung.Change(being);
                            being.X++;
                            dung.Change(being);
                              being.Creature(this, 1);
                        }
                    }
                }
            }
            else if (temp==1)
            {

                if (being.Y > player.Y)
                {
                    if (dung.CheckTile(being.X, being.Y - 1) != SolidTiles.Wall)
                    {
                        if (dung.CreatureCheck(being.X, being.Y - 1))
                        {
                            dung.Change(being);
                            being.Y--;
                            dung.Change(being);
                        }
                    }
                }
                
                else if (being.Y<player.Y)
                {
                    if (dung.CheckTile(being.X, being.Y + 1) != SolidTiles.Wall)
                    {
                        if (dung.CreatureCheck(being.X, being.Y + 1))
                        {
                            dung.Change(being);
                            being.Y++;
                            dung.Change(being);
                        }
                    }
                }
            }
            

        }

        private void Move(int x, int y, Dungeon dung)

        {
            if (dung.CheckTile(being.X + x, being.Y + y) != SolidTiles.Wall)
            {
                dung.Change(being);
                being.AddTo(x, y);
                dung.Change(being);
                being.Creature(this, 1);
            }
        }   

        private void RandBehave(Dungeon dung)
        {
            temp = (byte)(rand.Next(5) + 1);
            if (temp == 1)
            {

                Move(1, 1, dung);
            }



        }
    }
}