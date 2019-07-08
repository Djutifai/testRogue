using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Movement
    {
        private ConsoleKey _move;

        public void PlMove(Dungeon dung, Player player)
        {
            _move = Console.ReadKey().Key;
            switch (_move)
            {
                case ConsoleKey.UpArrow:

                    Check(-1, 0, dung, player); // move up
                    break;
                case ConsoleKey.DownArrow:
                    Check(+1, 0, dung, player); // move down
                    break;
                case ConsoleKey.LeftArrow:
                    Check(0, -1, dung, player); // move left 
                    break;
                case ConsoleKey.RightArrow:
                    Check(0, +1, dung, player); // move right
                    break;
                case ConsoleKey.Z:
                    player.Interact(player.X,player.Y,dung, player);
                    break;
                default:
                    break;
            }
        }

        private void Check(int x, int y, Dungeon dung, Player creature) // checking if there are a creature on a cell where player tries to move
        {
            
            if (dung.CreatureCheck(creature.X+x,creature.Y+y))
            {
                creature.Attack(dung, dung.GetEnemyAtCoordinates(creature.X + x, creature.Y + y),1);
                if (creature.Hp <= 0)
                {
                    creature.Die(dung);
                    dung.GameOver();
                }
                else if (!dung.CreatureCheck(creature.X + x, creature.Y + y))
                    Move(x, y, dung, creature);  
            }
            else if (dung.GiveInteraction(creature.X+x,creature.Y+y)==Interaction.UpLadder||dung.GiveInteraction(creature.X+x,creature.Y+y)==Interaction.DownLadder)
            {
                if (dung.GiveInteraction(creature.X + x, creature.Y + y) == Interaction.UpLadder)
                    creature.NextLvl(dung, +1);
                else if (dung.GiveInteraction(creature.X + x, creature.Y + y) == Interaction.DownLadder)
                    creature.NextLvl(dung, -1);
            }
            else
            {
                Move(x, y, dung, creature);
            }
        }   
         
        protected void Move(int x, int y, Dungeon dung, LivingObject creature) // Basic movement method for a livingobject
        {
            if (!dung.CreatureCheck(creature.X + x, creature.Y + y)) //checking if there are no creatures on a cell
            {
                if (dung.CheckTile(creature.X + x, creature.Y + y) != SolidTiles.Wall&&!dung.InteractiveCheck(creature.X+x,creature.Y+y))
                {
                    dung.Change(creature.Being);    //Making creature's current XY cell not alive
                    creature.Being.AddTo(x, y);     //Changing coordinates of a creature 
                    dung.Change(creature.Being);    //Making updated creature's XY cell alive
                }
                else Console.Write("Error in movement");
            }
        }
    }
}
