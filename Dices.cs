using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    // We decided to create one class for the 2 dices because of the random function
    // We need to create one instance of random in order to generate 2 different numbers for the dices
    class Dices
    {
        //Attributes
        private int die1; //value of die1 between 1 and 6
        private int die2; //value of die2 between 1 and 6
        private bool doubleDices; //true if the 2 dices are equal

        //Constructor
        public Dices()
        {
            die1 = 0; //we didn't play yet
            die2 = 0;
            doubleDices = false;
        }

        public bool DoubleDices
        {
            get { return doubleDices; }
            set { doubleDices = value; }
        }

        //To roll the dices
        public int RollDices()
        {
            doubleDices = true;

            //Generate a random value between 1 and 6 for dices 1 and 2
            Random number = new Random();
            die1 = number.Next(1, 7);
            die2 = number.Next(1, 7);
            //die1 = 2;
            //die2 = 2;

            //Check if we have a double
            doubleDices = false;
            if (die1 == die2)
                doubleDices = true;

            //return the sum of the dices
            return die1 + die2;

        }

        //Display the dices
        public void DisplayDices()
        {
            Console.WriteLine("|" + die1 + "||" + die2 + "|");
        }
    }
}
