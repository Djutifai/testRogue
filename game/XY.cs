using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class XY 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Image { get; set; }
        public bool IsAlive { get; set; }
        private LivingObject creature;
        public XY(char image)
        {
            Image = image;
            IsAlive = false;
        }
        public XY(int x, int y,char image)
        {
            X = x;
            Y = y;
            Image = image;
            IsAlive = false;
        }
        public char GetImage()
        {
            return Image;   
        }
        public void SetImage(char img)
        {
            Image = img;
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
            Image = this.creature.Image;
        }
        public LivingObject GetCreature()
        {
            return creature;
        }

    }
}
