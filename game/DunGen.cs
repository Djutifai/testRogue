using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class DunGen  //Everything about generation and dirty stuff is over here ;)
    {
        // Цели:
        //      ᛫ Сделать адекватный алгоритм деления подземелия + тут же усовершенствовать проверку на дверь, если дверь встречается - стена рисуется по соседству
        //      ᛫ Напихать сундуки, врагов (спавн врагов также должен производиться НЕ В СТЕНКЕ)
        //      ᛫ Реализовать переход на следующий уровень, с сохранением предыдущего в его current state (массив солидтайлов, скорее всего)
        
        protected static Random rand = new Random();
        
        private int i, temp;
        protected int currentLevel = 1;
        private bool isHorizontal = true;
        protected string stringMap = "";
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
        protected string[] ChestContainer = new string[] { "Sword", "Scroll", "Apple" };
        protected readonly string[] enemyName = new string[] { "rat", "spider", "zombie" };
        protected readonly int[] enemyHp = new int[] { 3, 4, 6 };
        protected readonly int[] enemyAtk = new int[] { 4, 3, 2 };
        protected readonly int[] enemyArm = new int[] { 1, 1, 3 };
        protected readonly char[] enemyImage = new char[] { 'r', 's', 'z' };

        protected void Generation(Dungeon dung) // algorythm for the procedural generation
        {
            EnemyGen(dung);
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
                        else { _mapTiles[i, j] = SolidTiles.Wall; }

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
                    while (dung.dung.CheckTile(horizontal[arrayindex1], i) != SolidTiles.Wall)
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
            InteractionGen();
        }

        private void Print() // printing string array full of symbols
        {
            Console.WriteLine(stringMap);
            stringMap = "";

            Console.WriteLine("{0}'s hp: {1}", _player.Name, _player.Hp);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[currentLevel,i].Hp > 0)
                    Console.WriteLine("{0}'s hp: {1}", enemies[currentLevel,i].Name, enemies[currentLevel,i].Hp);
            }
            _player.DisplayItems();
        }

        private void InteractionGen()
        {
            temp = (byte)rand.Next(7, 35);
            if (CheckForDoors(temp, 20))
                _mapcoordinates[temp, 20].AddInteraction(Interaction.ClosedChest);
            temp = (byte)rand.Next(5, 10);
            if (CheckForDoors(temp, 7))
                _mapcoordinates[temp, 7].AddInteraction(Interaction.ClosedChest);
            if (currentLevel != 4)
            {
                temp = (byte)rand.Next(2, 40);
                if (CheckForDoors(temp, 4))
                    if (_mapTiles[temp, 4] == SolidTiles.Floor)
                        _mapcoordinates[temp, 4].AddInteraction(Interaction.UpLadder);
                    else _mapcoordinates[temp, 5].AddInteraction(Interaction.UpLadder);
            }
            if (currentLevel != 1)
            {
                temp = (byte)rand.Next(5, 30);
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

        private void MakingMap() // Adding all char's to the string array
        {
            for (int i = 0; i < _mapTiles.GetLength(0); i++)
            {
                for (int j = 0; j < _mapTiles.GetLength(1); j++)
                {

                    if (i == _player.X && j == _player.Y) stringMap += _player.Image;


                    else if (_mapcoordinates[i, j].IsAlive == true)
                        foreach (Enemy enemy in enemies)
                        {
                            if (!enemy.IsDead())
                            {
                                if (i == enemy.X && j == enemy.Y) stringMap += enemy.Image;
                            }
                        }
                    else if (_mapcoordinates[i, j].GetInteraction() != Interaction.None)
                    {
                        ObjPrint(i, j);
                    }
                    else if (j != _mapTiles.GetLength(0) - 1)
                    {
                        if (_mapTiles[i, j] == SolidTiles.Wall) stringMap += '#';
                        else if (_mapTiles[i, j] == SolidTiles.Floor) stringMap += '.';
                    }

                    else if (j == _mapTiles.GetLength(0) - 1)
                    {
                         stringMap += '#';
                    }
                }
                 stringMap += "\n";

            }
        }

        private void EnemyGen(Dungeon dung) // generating random amount of random enemies (right now there are 3 type of an enemy)
        {
            enemies = new Enemy[currentLevel,enemyName.Length];

            for (int j = 0; j < enemyName.Length; j++)
            {
                temp = (byte)rand.Next(3);
                if (dung.CheckTile(_mapTiles.GetLength(0) / 2 + (j - temp), _mapTiles.GetLength(0) / 2 + (j + temp)) != SolidTiles.Wall)
                {
                    enemies[currentLevel,j] = new Enemy(_mapTiles.GetLength(0) / 2 + (j - temp), _mapTiles.GetLength(0) / 2 + (j + temp), enemyName[temp], enemyHp[temp], enemyAtk[temp], enemyArm[temp], enemyimage[temp]);
                    _mapcoordinates[enemies[currentLevel,j].X, enemies[currentLevel,j].Y].IsAlive = true;
                }
                else
                {
                    enemies[j] = new Enemy(_mapTiles.GetLength(0) / 2 + (j), _mapTiles.GetLength(0) / 2 + (j - temp), enemyName[temp], enemyHp[temp], enemyAtk[temp], enemyArm[temp], enemyImage[temp]);
                    _mapcoordinates[enemies[j].X, enemies[j].Y].IsAlive = true; ;
                }
            }
        }

        private void ObjPrint(int x, int y) // printing Interactive Objects
        {
            if (_mapcoordinates[x, y].GetInteraction() == Interaction.ClosedChest)
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
