using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Dungeon
    {
        private Player _player; 
        private int _height;
        private int _width;
        private XY _wall = new XY ('#');
        private XY _empty = new XY(' ');
        private bool _isWorking = true;
        private XY[,] _mapcoordinates;
        Movement movement = new Movement();
        private XY _tempXY;
        private Enemy _rat;
        public void Start()
        {
            Console.Write("Choose the height of our dungeon: ");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Choose the width of our dungeon: ");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            _height = height+2;
            _width = width+2;
            _mapcoordinates = new XY[_height, _width];
            RoomGen();
        }
        private void RoomGen()
        {
            
            
            for (int i = 0;i<_height;i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    
                    if (i == 0 || i == _height - 1)
                    {
                          
                          _mapcoordinates[i, j] = new XY(i, j,_wall.Image);
                    }
                    else if (i != 0 && i != _height - 1)
                    {
                        if (j != 0 && j != _width - 1) { _mapcoordinates[i, j] = new XY(i, j, _empty.Image); }
                        else { _mapcoordinates[i, j] = new XY(i, j, _wall.Image); }

                    }
                    
                }
            }
            PlayerSpawn();
            
            while (_isWorking)
            {
                Print();
                PlayerMove();
                Console.Clear();
            }
        }
        private void Print()
        {
            for (int i = 0; i < _height; i++)
                for (int j = 0; j < _width; j++)
                     if (j != _width - 1)
                        Console.Write(_mapcoordinates[i, j].GetImage());
                    else if (j == _width - 1)
                        Console.WriteLine(_mapcoordinates[i, j].GetImage());
            Console.WriteLine("Your hp: {0}", _player.Atk);
            Console.WriteLine("Rat hp: {0}", _rat.Hp);
        }
        private void PlayerMove()
        {
            _tempXY = _player.GetCoordinates();
            _player.Move(this);
            
            
            
            
        }
        public char CheckImage(int x, int y)
        {
            return _mapcoordinates[x, y].Image;
        }
        public void Dead(int x,int y,int t)
        {
            if (t == 1)
                _mapcoordinates[x, y].IsAlive = true;
            else if (t == 2)
                _mapcoordinates[x, y].IsAlive = false;
        }
        public LivingObject GiveObject(int x,int y)
        {
            return _mapcoordinates[x, y].GetCreature();
        }
        private void PlayerSpawn()
        {
            _player = new Player((_height / 2)-1, (_width / 2)-1);
            _mapcoordinates[(_height / 2)-1, (_width / 2)-1] = _player.GetCoordinates();
            _mapcoordinates[_player.getX(), _player.getY()].Creature(_player);
            _rat = new Enemy(_player.getX() - 1, _player.getY() - 1, "big ass rat", 7, 3, 0, 'r');
            _mapcoordinates[_rat.getX(), _rat.getY()].SetImage(_rat.Image);
        }
        public void Change(int x,int y,char ch,int t)
        {
            _mapcoordinates[x,y].SetImage(ch);
            Dead(x, y, t);
            
        }     
        public bool GetStatus(int x,int y)
        {
            return _mapcoordinates[x, y].IsAlive;
        }
    }
}
