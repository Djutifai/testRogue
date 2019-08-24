using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testRogue
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(2, 2);
            Dungeon dung = new Dungeon();
            dung.Start(player);
        }
    }
}
