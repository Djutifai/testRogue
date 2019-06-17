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

                    Check(-1, 0, xy, dung, player); // move up
                    break;
                case ConsoleKey.DownArrow:
                    Check(+1, 0, xy, dung, player); // move down
                    break;
                case ConsoleKey.LeftArrow:
                    Check(0, -1, xy, dung, player); // move left 
                    break;
                case ConsoleKey.RightArrow:
                    Check(0, +1, xy, dung, player); // move right
                    break;
                default:
                    break;
            }
        }
        private void Check(int x, int y, XY xy, Dungeon dung, Player player) // checking if there are a creature on a cell where player tries to move
        {
            
            if (dung.CreatureCheck(player.X+x,player.Y+y))
            {
                
                player.Attack(dung.GiveObject(player.X + x, player.Y + y));
                if (player.Hp <= 0) dung.GameOver();
                else if (dung.GiveObject(player.X + x, player.Y + y).Hp <= 0)
                    Move(x, y, xy, dung, player);
                
            }
            else if (dung.CheckTile(player.X + x, player.Y + y) != SolidTiles.Wall)
            {
                Move(x, y, xy, dung, player);
            }
            else Console.Write("Error in movement");
        }
        private void Move(int x, int y, XY xy, Dungeon dung, Player player)
        {
            dung.Change(player.Being());
            xy.AddTo(x, y);
            dung.Change(player.Being());
        }
    }
}
