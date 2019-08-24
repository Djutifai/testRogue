using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Item
    {
        private string _name;
        private int _buffToHealth = 0;
        private int _buffToArmor = 0;
        private int _buffToAttack = 0;
        private bool _isConsumable = false;

        public Item(string name, int Hp, int Arm, int Atk, bool isConsumable)
        {
            _name = name;
            _buffToHealth = Hp;
            _buffToArmor = Arm;
            _buffToAttack = Atk;
            _isConsumable = isConsumable;
        }

        public void AddStats (Player player)
        {
            if (!_isConsumable)
            {
                player.Atk += _buffToAttack;
                player.Arm += _buffToArmor;
                player.Hp += _buffToHealth;
            }
        }

        public string GetName()
        {
            return _name;
        }
    }
}
