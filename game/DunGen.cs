using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class DunGen : Containers  //Everything about generation and dirty stuff is over here ;)
    {
        // Цели:
        //      ᛫ Сделать адекватный алгоритм деления подземелия + тут же усовершенствовать проверку на дверь, если дверь встречается - стена рисуется по соседству
        //      ᛫ Напихать сундуки, врагов (спавн врагов также должен производиться НЕ В СТЕНКЕ)
        //      ᛫ Реализовать переход на следующий уровень, с сохранением предыдущего в его current state (массив солидтайлов, скорее всего)
        
        protected static Random rand = new Random();

        private int i, temp;
        
        private bool isHorizontal = true;
        protected string stringMap = "";
        protected string inventoryString = "";

        protected SolidTiles[,] _mapTiles = new SolidTiles[50, 50];

        protected SolidTiles[,] _mapSaver1 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver2 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver3 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver4 = new SolidTiles[50, 50];

        protected XY[,] _mapcoordinates = new XY[50, 50];

        protected XY[,] _mapCoordinatesSaver1 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver2 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver3 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver4 = new XY[50, 50];

        protected Enemy[,] enemies = new Enemy[4,3];
        protected Item[] chestContainer = new Item[5];

        protected void Generation(Dungeon dung) // algorythm for the procedural generation
        {        
            for (int i = 0; i < 50; i++) // here is where the main dungeon "box" is made
            {               
                for (int j = 0; j < 50; j++)
                {
                    if (i == 0 || i == 50 - 1)
                    {
                        _mapTiles[i, j] = SolidTiles.Wall;
                    }
                    else if (i != 0 && i != 50 - 1)
                    {
                        if (j != 0 && j != 50 - 1) { _mapTiles[i, j] = SolidTiles.Floor; }
                        else  _mapTiles[i, j] = SolidTiles.Wall; 
                    }
                    else Console.WriteLine("Error");
                    _mapcoordinates[i, j] = new XY();
                }
            }
            //x = _mapTiles.GetLength(0) / 2 - rand.Next(-5, 5);
            int[] horizontal = new int[4]; // arrays for the
            int[] vertical = new int[4];   // cut points
            int arrayindex1 = 0; 
            int arrayindex2 = 0;
            horizontal[arrayindex1] = (_mapTiles.GetLength(0) / 2) - rand.Next(-2, 2); // creating first
            vertical[arrayindex2] = (_mapTiles.GetLength(1) / 2) + rand.Next(-1, 1);   // cut points
            
            for (int r = 0; r < 6; r++) // now here is where room starts to show up
            {
                i = 1;
                if (isHorizontal) // checking if it should be the horizontal or vertical cut of the dungeon
                {
                    while (dung.CheckTile(horizontal[arrayindex1], i) != SolidTiles.Wall)
                    {
                        if (dung.GiveInteraction(horizontal[arrayindex1], i) != Interaction.ClosedDoor)
                        {
                            _mapTiles[horizontal[arrayindex1], i] = SolidTiles.Wall;
                            i++;
                        }
                    }
                    temp = rand.Next(5, i);
                    _mapTiles[horizontal[arrayindex1], temp] = SolidTiles.Floor;
                    _mapcoordinates[horizontal[arrayindex1], temp].AddInteraction(Interaction.ClosedDoor);
                    arrayindex1++;
                    horizontal[arrayindex1] = horizontal[arrayindex1-1]/2;
                    isHorizontal = false;
                }
                else if (!isHorizontal)
                {
                    while (dung.CheckTile(i,vertical[arrayindex2])!=SolidTiles.Wall)
                    {
                        if (dung.GiveInteraction(i, vertical[arrayindex2]) != Interaction.ClosedDoor)
                        {
                            _mapTiles[i, vertical[arrayindex2]] = SolidTiles.Wall;
                            i++;
                        }
                    }
                    temp = rand.Next(5, i);
                    _mapTiles[temp, vertical[arrayindex2]] = SolidTiles.Floor;
                    _mapcoordinates[temp, vertical[arrayindex2]].AddInteraction(Interaction.ClosedDoor);
                    arrayindex2++;
                    vertical[arrayindex2] = vertical[arrayindex2-1]/2;
                    isHorizontal = true;
                }
            }
            EnemyGen(dung.GetLevel());
            InteractionGen(dung.GetLevel());
            ItemGen();
        }

        private void InteractionGen(int currentLevel)
        {
            temp = rand.Next(7, 35);
            if (CheckForDoors(temp, 20))
                _mapcoordinates[temp, 20].AddInteraction(Interaction.ClosedChest);
            temp = rand.Next(5, 10);
            if (CheckForDoors(temp, 7))
                _mapcoordinates[temp, 7].AddInteraction(Interaction.ClosedChest);
            if (currentLevel != 4) 
            {
                temp = rand.Next(2, 40);
                if (CheckForDoors(temp, 4))
                    if (_mapTiles[temp, 4] == SolidTiles.Floor)
                        _mapcoordinates[temp, 4].AddInteraction(Interaction.UpLadder);
                    else _mapcoordinates[temp, 5].AddInteraction(Interaction.UpLadder);
            }
            if (currentLevel != 1)
            {
                temp = rand.Next(5, 30);
                if (CheckForDoors(6, temp))
                    if (_mapTiles[6, temp] == SolidTiles.Floor)
                        _mapcoordinates[6, temp].AddInteraction(Interaction.DownLadder);
                    else _mapcoordinates[7, temp].AddInteraction(Interaction.DownLadder);
            }
        }

        private bool CheckForDoors(int x, int y)
        {
            if (_mapcoordinates[x + 1, y].GetInteraction() == Interaction.None
                && _mapcoordinates[x - 1, y].GetInteraction() == Interaction.None
                && _mapcoordinates[x, y + 1].GetInteraction() == Interaction.None
                && _mapcoordinates[x, y - 1].GetInteraction() == Interaction.None
                && _mapcoordinates[x, y].GetInteraction() == Interaction.None)
                return true;
            return false;
        }       

        private void EnemyGen(int currentLevel) // generating random amount of random enemies (right now there are 4 type of an enemy)
        {
            for (int i = 0; i < enemies.GetLength(1); i++)
            {
                temp = rand.Next(4);
                enemies[currentLevel-1, i] = CreateEnemy(temp);
                enemies[currentLevel-1, i].Being.SetCoordinate(45 - rand.Next(20), 20 + rand.Next(10));
                _mapcoordinates[enemies[currentLevel-1, i].X, enemies[currentLevel-1, i].Y].IsAlive = true;
            }
        }
        
        private void ItemGen()
        {
            for (int i = 0; i < chestContainer.GetLength(0); i++)
                chestContainer[i] = CreateItem(rand.Next(6));
            
        }
        private void InventoryGen() // trying inventory-menu system
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j =0;j<20;j++)
                {
                    if (i == 0 || i == 19 || j == 0 || j == 19) inventoryString += '~';
                //    else if (i==3&&j==3) for (int x = 0; x<)
                }
            }
        }
        protected void PlayerSpawn(Player player)
        {
            _mapcoordinates[player.X, player.Y].IsAlive = true;
        }

        protected void ObjPrint(int x, int y) // printing Interactive Objects
        {
            if (_mapcoordinates[x,y].GetInteraction()==Interaction.None)
            {
                if (_mapTiles[x, y] == SolidTiles.Floor) stringMap += '.';
                else if (_mapTiles[x, y] == SolidTiles.Wall) stringMap += '#';
            }
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.ClosedChest)
                 stringMap += '~';
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.OpenedChest)
                 stringMap += '-';
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.OpenedDoor)
                 stringMap += '`';
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.ClosedDoor)
                 stringMap += '+';
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.UpLadder)
                 stringMap += '<';
            else if (_mapcoordinates[x, y].GetInteraction() == Interaction.DownLadder)
                 stringMap += '>';
            else Console.WriteLine("Error");
        }
    }
}
