using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Player : LivingObject
    {
        private string[] inventory = new string[5];
        private int temp = 0;
        public Player (int x, int y) : base("Boy",10,5,1,'@')
        {
            Being = new XY(x, y, Image)
            {
                IsAlive = true
            };
        }
        
        //Interacting with an object in all 4 directions from player
        public void Interact(int x, int y, Dungeon dung, Player creature)
        {

            for (int i = -1; i < 2; i = i + 2)
            {
                if (dung.GiveInteraction(x + i, y) == Interaction.ClosedChest)
                {
                    creature.NewItem(dung.GiveItem(x + i, y));
                }
                else if (dung.GiveInteraction(x + i, y) == Interaction.ClosedDoor)
                {
                    dung.OpenInteraction(x + i, y);
                }


            }
            for (int j = -1; j < 2; j = j + 2)
            {
                if (dung.GiveInteraction(x, y + j) == Interaction.ClosedChest)
                {
                    creature.NewItem(dung.GiveItem(x, y + j));
                }
                else if (dung.GiveInteraction(x , y + j) == Interaction.ClosedDoor)
                {
                    dung.OpenInteraction(x , y + j);
                }
            }
        }
        public void Move(Dungeon dung)
        {
            PlMove(dung, this);
        }
        public bool IsAlive()
        {
            return Being.IsAlive;
        }
        public void DisplayItems()
        {
            for (int i = 0; i < temp; i++)
                Console.WriteLine("You have an item {0}", inventory[i]);
        }
        public void NewItem(string item)
        {
            if (temp < 5)
            {
                inventory[temp] = item;
                temp++;
            }
        }
    }
}
