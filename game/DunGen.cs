using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class DunGen//starting simple procedural dungeon generation
    {
        // Цели:
        //      ᛫ Сделать адекватный алгоритм деления подземелия + тут же усовершенствовать проверку на дверь, если дверь встречается - стена рисуется по соседству
        //      ᛫ Напихать сундуки, врагов (спавн врагов также должен производиться НЕ В СТЕНКЕ)
        //      ᛫ Реализовать переход на следующий уровень, с сохранением предыдущего в его current state (массив солидтайлов, скорее всего)
        protected SolidTiles[,] _map = new SolidTiles[50,50];
        protected Random rand = new Random();
        protected XY[,] _mapcoordinates = new XY[50, 50];
        private int room, i, temp;
        private bool isHorizontal = true;
        protected SolidTiles[,] _mapSaver1 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver2 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver3 = new SolidTiles[50, 50];
        protected SolidTiles[,] _mapSaver4 = new SolidTiles[50, 50];
        protected XY[,] _mapCoordinatesSaver1 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver2 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver3 = new XY[50, 50];
        protected XY[,] _mapCoordinatesSaver4 = new XY[50, 50];
        protected void Generation(Dungeon dung) // algorythm for the procedural generation
        {
            room = rand.Next(10, 15);
            for (int i = 0; i < 50; i++) // here is where the main dungeon "box" is made
            {
                
                for (int j = 0; j < 50; j++)
                {

                    if (i == 0 || i == 50 - 1)
                    {

                        _map[i, j] = SolidTiles.Wall;
                    }
                    else if (i != 0 && i != 50 - 1)
                    {
                        if (j != 0 && j != 50 - 1) { _map[i, j] = SolidTiles.Floor; }
                        else { _map[i, j] = SolidTiles.Wall; }

                    }
                    else Console.WriteLine("Error");
                    _mapcoordinates[i, j] = new XY();
                }
            }
            //x = _map.GetLength(0) / 2 - rand.Next(-5, 5);
            Console.Write("Enter desired amount of rooms: ");
            room = Convert.ToInt32(Console.ReadLine());
            int[] horizontal = new int[room/2]; // arrays for cut points
            int[] vertical = new int[room/2];   // 
            int arrayhelper1 = 0;
            int arrayhelper2 = 0;
            horizontal[arrayhelper1] = (_map.GetLength(0) / 2) - rand.Next(-2, 2); 
            vertical[arrayhelper2] = (_map.GetLength(1) / 2) + rand.Next(-1, 1);
            for (int r = 0; r < room; r++) // now here is where room starts to show up
            {
                i = 1;
                if (isHorizontal) // checking if it should be the horizontal or vertical cut of the dungeon
                {
                    while (dung.CheckTile(horizontal[arrayhelper1], i) != SolidTiles.Wall)
                    {
                        if (dung.GiveInteraction(horizontal[arrayhelper1], i) != Interaction.ClosedDoor)
                        {
                            _map[horizontal[arrayhelper1], i] = SolidTiles.Wall;
                            i++;
                        }
                    }
                    temp = rand.Next(5, i);
                    _map[horizontal[arrayhelper1], temp] = SolidTiles.Floor;
                    _mapcoordinates[horizontal[arrayhelper1], temp].InteractionDoor();
                    arrayhelper1++;
                    horizontal[arrayhelper1] = horizontal[arrayhelper1-1]/2;
                    isHorizontal = false;
                }
                else if (!isHorizontal)
                {
                    while (dung.CheckTile(i,vertical[arrayhelper2])!=SolidTiles.Wall)
                    {
                        if (dung.GiveInteraction(i, vertical[arrayhelper2]) != Interaction.ClosedDoor)
                        {
                            _map[i, vertical[arrayhelper2]] = SolidTiles.Wall;
                            i++;
                        }
                    }
                    temp = rand.Next(5, i);
                    _map[temp, vertical[arrayhelper2]] = SolidTiles.Floor;
                    _mapcoordinates[temp, vertical[arrayhelper2]].InteractionDoor();
                    arrayhelper2++;
                    vertical[arrayhelper2] = vertical[arrayhelper2-1]/2;
                    isHorizontal = true;
                }
            }
        }
    }
}
