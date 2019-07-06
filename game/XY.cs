using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class XY
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAlive { get; set; }
        private LivingObject creature;
        private Interaction cell = new Interaction();
        public XY()  // non-creature constructor (using in the mapArray)
        {
            cell = Interaction.None;
            IsAlive = false;
        }
        public XY(int x, int y,char image) //creature constructor
        {
            cell = Interaction.None;
            X = x;
            Y = y;
            IsAlive = false;
        }
        public void InteractionDoor ()
        {
            cell = Interaction.ClosedDoor;
        }
        public void InteractionChest()
        {
            cell = Interaction.ClosedChest;
        }
        public void OpenInteraction()  // when player tries to open an interaction object that is closed
        {
            if (cell == Interaction.ClosedChest)
                cell = Interaction.OpenedChest;
            else if (cell == Interaction.ClosedDoor)
                cell = Interaction.OpenedDoor;
            else if (cell == Interaction.OpenedDoor)
                cell = Interaction.ClosedDoor;
            
        }
        public Interaction GetInteraction()
        {
            return cell;
        }
        public void SetCoordinate(int x,int y)
        {
            X = x;
            Y = y;
        }
        public void AddTo(int x,int y)
        {
            X += x;
            Y += y;
        }
        public void Creature(LivingObject creature)
        {
            this.creature = creature;
            IsAlive = true;
        }
        public LivingObject GetCreature()
        {
            return creature;
        }
        public void Die()
        {
            IsAlive = false;
            
        }

    }
}
