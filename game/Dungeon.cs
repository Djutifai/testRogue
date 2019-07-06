using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Dungeon : DunGen
    {
        private Player _player;
        private bool _isWorking = true;
        private byte temp;
        private Enemy[] enemies;
        private string[] ChestContainer= new string[] { "Sword", "Scroll", "Apple" };
        private readonly string[] enemyname= new string[] { "rat", "spider", "zombie" };
        private readonly int[] enemyhp = new int[] { 3, 4, 6 };
        private readonly int[] enemyatk = new int[] { 4, 3, 2 };
        private readonly int[] enemyarm= new int[] { 1, 1, 3 };
        private readonly char[] enemyimage = new char[] { 'r', 's', 'z' };

        public void Start() // Initializing
        {
            Generation(this);
            PlayerSpawn();
          //  EnemyGen(2);
            ChestGen();
            while (_isWorking) // game loop
            {
                Print();
                _player.Move(this); //player movement
                //EnemyMove();        
                Console.Clear();
            }
        }

        private void Print() // printing the dungeon including all objects in their current state 
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j < _map.GetLength(1) ; j++)
                {

                    if (i == _player.X && j == _player.Y) Console.Write(_player.Image);


                    else if (_mapcoordinates[i, j].IsAlive == true)
                        foreach (Enemy enemy in enemies)
                        {
                            if (!enemy.IsDead())
                            {
                                if (i == enemy.X && j == enemy.Y) Console.Write(enemy.Image);
                            }
                        }
                    else if (_mapcoordinates[i,j].GetInteraction()!=Interaction.None)
                    {
                        ObjPrint(i, j);
                    }
                    else if (j != _map.GetLength(0) - 1)
                    {
                        if (_map[i, j] == SolidTiles.Wall) Console.Write('#');
                        else if (_map[i, j] == SolidTiles.Floor) Console.Write('.');
                    }

                    else if (j == _map.GetLength(0) - 1)
                    {
                        Console.WriteLine('#');
                    }
                }
                
            }

            Console.WriteLine("{0}'s hp: {1}", _player.Name, _player.Hp);
          /*  for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Hp > 0)
                    Console.WriteLine("{0}'s hp: {1}", enemies[i].Name, enemies[i].Hp);
            }*/
            _player.DisplayItems();
        }
        
        public SolidTiles CheckTile(int x, int y)
        {
            return _map[x, y];
        }

        public LivingObject GiveObject(int x, int y)
        {
            return _mapcoordinates[x, y].GetCreature();
        }

        public void Change(XY being) //changing the alive status of the XY cell 
        {
            if (_mapcoordinates[being.X, being.Y].IsAlive == true)
                _mapcoordinates[being.X, being.Y].IsAlive = false;
            else if (_mapcoordinates[being.X, being.Y].IsAlive == false)
                _mapcoordinates[being.X, being.Y].IsAlive = true;
            else Console.WriteLine("Error in changing");
        }
        
        public Enemy GetEnemyAtCoordinates(int x, int y)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.X == x && enemy.Y == y) { return enemy; }
            }
            return null;
        }

        public Interaction GiveInteraction(int x, int y)
        {
            return _mapcoordinates[x, y].GetInteraction();
        }

        public bool InteractiveCheck(int x, int y) 
        {
            if (_mapcoordinates[x, y].GetInteraction() == Interaction.None||_mapcoordinates[x,y].GetInteraction()==Interaction.OpenedDoor)
                return false;
            else return true;
        }

        public bool CreatureCheck(int x, int y) 
        {
            if (_mapcoordinates[x, y].IsAlive)
                return true;
            else return false;
        }
        
        private void PlayerSpawn() //spawning player at the left top corner
        {
            _player = new Player(2, 2);
            _mapcoordinates[_player.X, _player.Y].Creature(_player);
        }

        private void EnemyGen(int x) // generating random amount of random enemies (right now there are 3 type of an enemy)
        {
            enemies = new Enemy[x];

            for (int j = 0; j < x; j++)
            {
                temp = (byte)rand.Next(3);
                enemies[j] = new Enemy(_map.GetLength(0)/2+ (j - temp), _map.GetLength(0)/2+ (j + temp), enemyname[temp], enemyhp[temp], enemyatk[temp], enemyarm[temp], enemyimage[temp]);
                _mapcoordinates[enemies[j].X, enemies[j].Y].Creature(enemies[j]);
            }
        }

        public void OpenInteraction(int x, int y)
        {
            _mapcoordinates[x, y].OpenInteraction();
        }

        public string GiveItem(int x, int y)
        {
            _mapcoordinates[x, y].OpenInteraction();
            return ChestContainer[rand.Next(3)];
        }

        private void EnemyMove() //loop for all enemies to make a move
        {
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.IsDead())
                {
                    enemy.Ai(this, _player);
                }
            }
        }

        private void ChestGen()
        {
            temp = (byte)rand.Next(7, 15);
            _mapcoordinates[temp, 10].InteractionChest();
            temp = (byte)rand.Next(5, 10);
            _mapcoordinates[temp, 7].InteractionChest();
        }

        private void ObjPrint(int x, int y) // printing Interactive Objects
        {
            if (_mapcoordinates[x, y].GetInteraction() == Interaction.ClosedChest)
                Console.Write('~');
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.OpenedChest)
                Console.Write('-');
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.OpenedDoor)
                Console.Write('`');
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.ClosedDoor)
                Console.Write('+');
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.UpLadder)
                Console.Write('<');
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.DownLadder)
                Console.Write('>');
            else Console.WriteLine("Error");
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