using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    abstract class Location
    {
        //Attributes
        protected int idLocation; //between 0 and 39
        protected string nameLocation;
        protected Player owner = null; //if someone bought the property

        //Get and Set Methods
        public int IdLocation
        {
            get { return idLocation; }
            set { idLocation = value; }
        }

        //Display
        public void DisplayLocation()
        {
            Console.WriteLine("|" + this.idLocation + "  " + nameLocation + "|");
        }

        public void DisplayLocationAndOwner()
        {
            Console.WriteLine("|" + this.idLocation + "  " + nameLocation + "|");
            if (owner != null)
            {
                Console.Write("Owner : " + owner.NamePlayer);
            }
        }

        public abstract void displayRules(Player currentPlayer, Board board);
    }
}
