using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Location_Jail : Location
    {
        public Location_Jail(int id)
        {
            idLocation = id;
            nameLocation = "JAIL";
        }

        public override void displayRules(Player currentPlayer, Board board)
        {
            if (currentPlayer.InJail == true)
                Console.WriteLine("You are in JAIL !!");
            else
                Console.WriteLine("You are visiting the jail.");
        }
    }
}
