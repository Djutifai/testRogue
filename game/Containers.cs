using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Containers  // Class for creating enemies
    {
        //For enemies
        private readonly string[] _enemyName = new string[] { "rat", "spider", "zombie", "rogue" };
        private readonly int[] _enemyHp = new int[] { 2, 3, 6, 8 };
        private readonly int[] _enemyAtk = new int[] { 2, 3, 2, 3 };
        private readonly int[] _enemyArm = new int[] { 0, 1, 3, 2 };
        private readonly char[] _enemyImage = new char[] { 'r', 's', 'z', 'R' };

        protected Enemy CreateEnemy(int number)
        {
            return new Enemy(_enemyName[number], _enemyHp[number], _enemyAtk[number], _enemyArm[number], _enemyImage[number]);
        }

        //For items

        private readonly string[] _itemName = new string[] { "wooden sword", "chest armor", "apple", "healing scroll", "magic necklace", "iron sword" };
        private readonly int[] _buffToHp = new int[] { 0, 4, 2, 8, 1, 0 };
        private readonly int[] _buffToAtk = new int[] { 2, 0, 0, 0, 1, 5 };
        private readonly int[] _buffToArm = new int[] { 0, 5, 0, 0, 1, 0 };
        private readonly bool[] _isConsumable = new bool[] { false,false,true,true,false,false};

        protected Item CreateItem(int number)
        {
            return new Item(_itemName[number], _buffToHp[number], _buffToArm[number], _buffToAtk[number], _isConsumable[number]);
        }
    }
}
