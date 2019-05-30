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
           /* пока думаю как реализовать атаку, завтра доделаю, скорее всего попробую сделать проверку на тайл, если он занят чем-то, типа на полу есть дверь, то будет
             выполняться метод взаимодействия и там уже будет смотреться чё за объект и производить атаку/открытие и т.д.
             if (dung.CreatureCheck(x,y))
            {
                player.Attack(dung.GiveObject(player.X + x, player.Y + y));
            }*/
             if (dung.CheckTile(player.X + x, player.Y + y) !=SolidTiles.Wall )
            {
                xy.AddTo(x, y);
            }
        }
    }
}
