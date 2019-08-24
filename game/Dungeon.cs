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
        private int currentLevel = 1;
        private bool _isWorking = true;

        private bool _isPaused = false;
        
        public void Start(Player player) // Initializing
        {
            _player = player;
            
            Generation(this);
            PlayerSpawn(_player);
            while (_isWorking) // game loop
            {
                while (_isPaused)
                {

                }
                Print();
                _player.Move(this); //player movement
                EnemyMove();
                Console.Clear();
                
            }
        }

        private void Print() // printing string array full of symbols
        {
            MakingMap();
            Console.WriteLine(stringMap);
            stringMap = "";

            Console.WriteLine("{0}'s hp: {1}", _player.Name, _player.Hp);
            for (int i = 0; i < enemies.GetLength(1); i++)
            {
                if (enemies[currentLevel-1, i].Hp > 0)
                    Console.WriteLine("{0}'s hp: {1}", enemies[currentLevel-1, i].Name, enemies[currentLevel-1, i].Hp);
            }
            _player.DisplayItems();
        }


        public SolidTiles CheckTile(int x, int y)
        {
            return _mapTiles[x, y];
        }

        public void Change(XY being) //changing the alive status of the XY cell 
        {
            // постараться что-то сделать со сменой при перемещении, ибо имеется проблема с первым перемещением после спавна в новосгенерированном данже
            if (_mapcoordinates[being.X, being.Y].IsAlive == true)
                _mapcoordinates[being.X, being.Y].IsAlive = false;
            else if (_mapcoordinates[being.X, being.Y].IsAlive == false)
                _mapcoordinates[being.X, being.Y].IsAlive = true;
            else Console.WriteLine("Error in changing");
        }

        private void MakingMap() // Adding all char's to the string array
        {
            for (int i = 0; i < _mapTiles.GetLength(0); i++)
            {
                for (int j = 0; j < _mapTiles.GetLength(1); j++)
                {

                    if (i == _player.X && j == _player.Y) stringMap += '@';


                    else if (_mapcoordinates[i, j].IsAlive == true)
                        for(int k = 0; k<enemies.GetLength(1);k++)
                        {
                            if(enemies[currentLevel-1,k].X==i&&enemies[currentLevel-1,k].Y==j)
                                stringMap += enemies[currentLevel-1, k].Image;
                        }
                    else ObjPrint(i, j);

                }
                stringMap += "\n";

            }
        }
        public Enemy GetEnemyAtCoordinates(int x, int y)
        {
            for (int i =0; i <enemies.GetLength(1);i++)
            {
                if (enemies[currentLevel-1, i].X == x && enemies[currentLevel-1, i].Y == y) return enemies[currentLevel-1, i];
            }
            return null;
        }

        public void LoadMap(int x)
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
            CheckIfFirst();
            _mapcoordinates[_player.X, _player.Y].IsAlive = true;
            for (int i = 0; i < enemies.GetLength(1); i++)
            {
                _mapcoordinates[enemies[currentLevel - 1, i].X, enemies[currentLevel - 1, i].Y].IsAlive = true;
            }
        }
        public void OpenInventory()
        {
            _isPaused = true;
        }
        
        private void InventoryDraw()
        {
            Console.Clear();

        }
        public int GetLevel()
        {
            return currentLevel;
        }
       
        public void SaveMap()
        {
            _mapcoordinates[_player.X, _player.Y].IsAlive = false;
            for (int i=0;i<enemies.GetLength(1); i++)
            {
                _mapcoordinates[enemies[currentLevel-1,i].X, enemies[currentLevel-1,i].Y].IsAlive = false;
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
              
        public void OpenInteraction(int x, int y)
        {
            _mapcoordinates[x, y].OpenInteraction(); 
        }

        public void CheckIfFirst() //checking if the level is first-time entered and needs to be generated
        {
            if (_mapcoordinates[0, 0] == null)
            {
                Generation(this);
            }

        }
        public Item GiveItem(int x, int y)
        {
            _mapcoordinates[x, y].OpenInteraction();
            return chestContainer[rand.Next(5)];
        }

        private void EnemyMove() //loop for all enemies to make a move
        {
            for (int i = 0; i < enemies.GetLength(1); i++)
            {
                if (!enemies[currentLevel-1,i].IsDead())
                    enemies[currentLevel-1, i].Ai(this, _player);
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