using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Movement
    {
        private ConsoleKey _move;
        private LivingObject _livingobj;
        public void Move(XY xy, Dungeon dung, Player player)
        {
            _move = Console.ReadKey().Key;
            switch (_move)
            {
                case ConsoleKey.UpArrow:

                    Check(-1, 0, xy, dung, player);
                    break;
                case ConsoleKey.DownArrow:
                    Check(+1, 0, xy, dung, player);
                    break;
                case ConsoleKey.LeftArrow:
                    Check(0, -1, xy, dung, player);
                    break;
                case ConsoleKey.RightArrow:
                    Check(0, +1, xy, dung, player);
                    break;
                default:
                    break;
            }
        }
        private void Check(int x, int y, XY xy, Dungeon dung, Player player)
        {
            /* пока думаю как реализовать исчезновение существа с карты, т.е. если у него 0>= хп, а у игрока не 0, то энеми должен пропасть, на днях доделаю, может сегодня*/
            if (dung.CreatureCheck(player.X+x,player.Y+y))
            {
                _livingobj = xy.GetCreature();
                player.Attack(dung.GiveObject(player.X + x, player.Y + y));
                if (player.Hp <= 0) dung.GameOver();
                
                else if(_livingobj.Hp<=0)
                {
                    _livingobj.Die();
                    Move(x, y, xy, dung);
                }
            }
            else if (dung.CheckTile(player.X + x, player.Y + y) != SolidTiles.Wall)
            {
                Move(x, y, xy, dung);
                

            }
            else Console.Write("Error in movement");
        }
        private void Move(int x,int y,XY xy,Dungeon dung)
        {
            dung.Change(xy.X, xy.Y);
            xy.AddTo(x, y);
            dung.Change(xy.X, xy.Y);
        }
    }
}
