using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Dungeon : DunGen
    {
        //fix непон€тный фриз уммммал€ю)
        private Player _player;
        
        private bool _isWorking = true;
        private byte temp;

        
        public void Start() // Initializing
        {
            Generation(this);
            PlayerSpawn();
            while (_isWorking) // game loop
            {
                
                _player.Move(this); //player movement
                EnemyMove();
                Console.Clear();
            }
        }
        
           
        public SolidTiles CheckTile(int x, int y)
        {
            return _mapTiles[x, y];
        }

        public void Change(XY being) //changing the alive status of the XY cell 
        {
            // постаратьс€ что-то сделать со сменой при перемещении, ибо имеетс€ проблема с первым перемещением после спавна в новосгенерированном данже
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

        protected void LoadMap(int x)
        {
            if (x==1)
            {
                currentLevel = 1;
                _mapTiles = _mapSaver1;
                _mapcoordinates = _mapCoordinatesSaver1;
            }
            else if (x == 2)
            {
                currentLevel = 2;
                _mapTiles = _mapSaver2;
                _mapcoordinates = _mapCoordinatesSaver2;
            }
            else if (x == 3)
            {
                currentLevel = 3;
                _mapTiles = _mapSaver3;
                _mapcoordinates = _mapCoordinatesSaver3;
            }
            else if (x == 4)
            {
                currentLevel = 4;
                _mapTiles = _mapSaver4;
                _mapcoordinates = _mapCoordinatesSaver4;
            }
            
        }

        protected int GetLevel()
        {
            return currentLevel;
        }

        protected void SaveMap()
        {
            _mapcoordinates[_player.X, _player.Y].IsAlive = false;
            foreach(Enemy enemy in enemies)
            {
                _mapcoordinates[enemy.X, enemy.Y].IsAlive = false;
            }
            if (currentLevel == 1)
            {
                _mapSaver1 = _mapTiles;
                _mapCoordinatesSaver1 = _mapcoordinates;
            }
            else if (currentLevel == 2)
            {
                _mapSaver2 = _mapTiles;
                _mapCoordinatesSaver2 = _mapcoordinates;
            }
            else if (currentLevel == 3)
            {
                _mapSaver4 = _mapTiles;
                _mapCoordinatesSaver3 = _mapcoordinates;
            }
            else if (currentLevel == 4)
            {
                _mapTiles = _mapSaver4;
                _mapcoordinates = _mapCoordinatesSaver4;
            }
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
            _mapcoordinates[_player.X, _player.Y].IsAlive = true;
        }

        
        public void OpenInteraction(int x, int y)
        {
            _mapcoordinates[x, y].OpenInteraction(); 
        }
        public void CheckIfFirst() //checking if the level is first-time entered and needs to be generated
        {
            if (_mapcoordinates[0, 0] == null)
            {
                Generation(this);
                _mapcoordinates[0, 0].IsFirst = false;
                _mapcoordinates[_player.X, _player.Y].IsAlive = true;
                foreach (Enemy enemy in enemies)
                    _mapcoordinates[enemy.X, enemy.Y].IsAlive = true;
            }

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
            Console.ReadKey();
            _isWorking = false;
        }
    }
}