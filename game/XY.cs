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
        public bool IsFirst { get; set; }
        private Interaction cell = new Interaction();

        public XY()  // non-creature constructor (using in the mapArray)
        {
            cell = Interaction.None;
            IsAlive = false;
            IsFirst = true;
        }
        public XY(int x, int y,char image) //creature constructor
        {
            cell = Interaction.None;
            X = x;
            Y = y;
            IsAlive = false;
        }
        public void AddInteraction (Interaction interaction)
        {
            cell = interaction;
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

        public void Die()
        {
            IsAlive = false;
            
        }

    }
}
