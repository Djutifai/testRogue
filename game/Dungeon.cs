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
        private SolidTiles[,] _map;
        Movement movement = new Movement();
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
            _map = new SolidTiles[_height, _width];
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

                        _map[i, j] = SolidTiles.Wall;
                    }
                    else if (i != 0 && i != _height - 1)
                    {
                        if (j != 0 && j != _width - 1) { _map[i, j] = SolidTiles.Floor; }
                        else { _map[i, j] = SolidTiles.Wall; }

                    }
                    else Console.WriteLine("Error");
                    _mapcoordinates[i, j] = new XY(' ');
                    _mapcoordinates[i, j].Creature(0);
                    
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
                    if (i == _player.X && j == _player.Y) Console.Write(_player.Image);
                    else if (i == _rat.X && j == _rat.Y) Console.Write(_rat.Image);
                    else if (j != _width - 1)
                    {
                        if (_map[i, j] == SolidTiles.Wall) Console.Write('#');
                        else if (_map[i, j] == SolidTiles.Floor) Console.Write(' ');
                    }

                    else if (j == _width - 1)
                    {
                        Console.WriteLine('#');
                    }
             Console.WriteLine("Your hp: {0}", _player.Hp);
             Console.WriteLine("Rat hp: {0}", _rat.Hp);
        }
        private void PlayerMove()
        {
            _player.Move(this);         
        }
        public SolidTiles CheckTile(int x, int y)
        {
            return _map[x, y];
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
            _mapcoordinates[_player.X, _player.Y].Creature(_player,1);
            _rat = new Enemy(_player.X - 1, _player.Y , "big ass rat", 7, 5, 2, 'r');
            _mapcoordinates[_rat.X, _rat.Y] = _rat.GetCoordinates();
            _mapcoordinates[_rat.X, _rat.Y].Creature(_rat,1);
            _mapcoordinates[_rat.X, _rat.Y].Tile=_rat.Image;
            
        }
       public void Change(int x,int y)
        {
            if (_mapcoordinates[x, y].IsAlive == true) _mapcoordinates[x, y].IsAlive = false;
            else if (_mapcoordinates[x, y].IsAlive == false) _mapcoordinates[x, y].IsAlive = true;
            else Console.WriteLine("Error in changing");
            

        }     
        public bool CreatureCheck(int x,int y)
        { 
            if (_mapcoordinates[x, y].IsAlive)
                return true;
            else return false;
        }
        public bool GetStatus(int x,int y)
        {
            return _mapcoordinates[x, y].IsAlive;
        }
        public void GameOver()
        {
            _isWorking = false;
        }
    }
}
