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
            if (dung.CheckImage(player.X+x,player.Y+y)=='r')
            {
                player.Attack(dung.GiveObject(player.X + x, player.Y + y));
            }
            else if (dung.CheckImage(player.X + x, player.Y + y) !='#' )
            {
                dung.Change(player.X, player.Y, dung.CheckImage(player.X + x, player.Y + y),0);
                xy.AddTo(x, y);
                dung.Change(player.X, player.Y, player.Image,1);
            }
        }
    }
}
