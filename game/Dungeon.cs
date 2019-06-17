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
        private int _height, _width;
        private XY _wall = new XY ();
        private XY _empty = new XY();
        private bool _isWorking = true;
        private XY[,] _mapcoordinates;
        private SolidTiles[,] _map;
        //Movement movement = new Movement();
        private Random rand = new Random();
        private byte temp;
        private Enemy[] enemies;
        private readonly string[] enemyname = new string[] { "rat", "spider", "zombie" };
        private readonly int[] enemyhp = new int[] { 3, 4, 6 };
        private readonly int[] enemyatk = new int[] { 4, 3, 2 };
        private readonly int[] enemyarm = new int[] { 1, 1, 3 };
        private readonly char[] enemyimage = new char[] { 'r', 's', 'z' };

        public void Start() // Initializing
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

        private void RoomGen() // generation of a room
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
                    _mapcoordinates[i, j] = new XY();
                }
            }
           PlayerSpawn();
           EnemyGen(3);

            while (_isWorking) // game loop
            {
                Print();
                PlayerMove();
                EnemyMove();
                Console.Clear();
            }
        }

        private void EnemyGen(int x) // generating random amount of enemies (right now there are 3 type of an enemy)
        {
            enemies = new Enemy[x];
            
            for (int j = 0; j < x; j++)
            {
                temp = (byte)rand.Next(3);
                enemies[j] = new Enemy(_player.X + (j - 1), _player.Y + (j +1), enemyname[temp], enemyhp[temp], enemyatk[temp], enemyarm[temp], enemyimage[temp]);
                _mapcoordinates[enemies[j].X, enemies[j].Y] = enemies[j].GetCoordinates();
                _mapcoordinates[enemies[j].X, enemies[j].Y].Creature(enemies[j], 1);
            }  
            
            
        }
      
        private void Print() // printing the dungeon
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    
                    if (i == _player.X && j == _player.Y) Console.Write(_player.Image); 


                    else if (_mapcoordinates[i, j].IsAlive==true)
                        foreach(Enemy enemy in enemies)
                        {
                            if (i == enemy.X && j == enemy.Y) Console.Write(enemy.Image);   
                        }
                        
                    else if (j != _width - 1)
                    {
                        if (_map[i, j] == SolidTiles.Wall) Console.Write('#');
                        else if (_map[i, j] == SolidTiles.Floor) Console.Write(' ');
                    }

                    else if (j == _width - 1)
                    {
                        Console.WriteLine('#');
                    }
                    
                }
            }
            Console.WriteLine("{0}'s hp: {1}",_player.Name, _player.Hp);
            for (int i = 0; i<enemies.Length; i++)
            {
                if (enemies[i].Hp>0) 
                Console.WriteLine("{0}'s hp: {1}", enemies[i].Name, enemies[i].Hp);
            }
            
        }

        private void PlayerMove() // movement
        {
            _player.Move(this);         
        }

        public SolidTiles CheckTile(int x, int y)
        {
            return _map[x, y];
        }

        public LivingObject GiveObject(int x,int y)
        {
            return _mapcoordinates[x, y].GetCreature();
        }

        private void PlayerSpawn()// spawn a player at the center of a map
        {
            _player = new Player((_height / 2)-1, (_width / 2)-1);
            _mapcoordinates[(_height / 2)-1, (_width / 2)-1] = _player.GetCoordinates();
            _mapcoordinates[_player.X, _player.Y].Creature(_player,1);
            
            
        }

        public void Change(XY being)
        {
            if (_mapcoordinates[being.X, being.Y].IsAlive == true) _mapcoordinates[being.X, being.Y].IsAlive = false;
            else if (_mapcoordinates[being.X, being.Y].IsAlive == false) { _mapcoordinates[being.X, being.Y].IsAlive = true; _mapcoordinates[being.X, being.Y] = being; }
            else Console.WriteLine("Error in changing");
            

        }  
        
        public bool CreatureCheck(int x,int y)
        { 
            if (_mapcoordinates[x, y].IsAlive)
                return true;
            else return false;
        }


        private void EnemyMove()
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.Ai(this,_player);
                _mapcoordinates[enemy.X, enemy.Y] = enemy.GetCoordinates();
            }
        }
        public void GameOver()
        {
            if (_player.IsAlive())
            {
                Console.Clear();
                Console.WriteLine("Congratulations, you have won!");
            }
            else if (!_player.IsAlive())
            {
                Console.Clear();
                Console.WriteLine("Sorry, you have died :(");
            }
            Console.ReadLine();
            _isWorking = false;
        }
    }
}
