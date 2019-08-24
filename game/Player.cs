using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Player : Entity
    {
        private string[] inventory1 = new string[5];
        private int temp = 0;
        private Item[] inventory = new Item[10];

        public Player (int x, int y) : base("Boy",10,5,1,'@')
        {
            Being = new XY(x, y)
            {
                IsAlive = true
            };
        }
        //Interacting with an object in all 4 directions from player
        public void Interact(int x, int y, Dungeon dung, Player creature)
        {

            for (int i = -1; i < 2; i += 2)
            {
                if (dung.GiveInteraction(x + i, y) == Interaction.ClosedChest)
                {
                    creature.NewItem(dung.GiveItem(x + i, y));
                }
                else if (dung.GiveInteraction(x + i, y) == Interaction.ClosedDoor||dung.GiveInteraction(x+i,y)==Interaction.OpenedDoor)
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
                else if (dung.GiveInteraction(x , y + j) == Interaction.ClosedDoor||dung.GiveInteraction(x,y+j)==Interaction.OpenedDoor)
                {
                    dung.OpenInteraction(x , y + j);
                }
            }
        }

        public void NextLvl(Dungeon dung, int x)
        {
            dung.SaveMap();
            dung.LoadMap(dung.GetLevel()+x);
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
            foreach (Item item in inventory)
                if (item!=null)Console.WriteLine("You have an item: {0}", item.GetName());
        }

        public void NewItem(Item item)
        {
            if (temp < 10)
            {
                inventory[temp] = item;
                inventory[temp].AddStats(this);
                temp++;
            }
        }
    }
}
