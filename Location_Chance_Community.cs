using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Location_Chance_Community : Location
    {
        public Location_Chance_Community(int id)
        {
            idLocation = id;
            nameLocation = "CHANCE-COMMUNITY";
        }

        public override void displayRules(Player currentPlayer, Board board)
        {
            Console.WriteLine("You are on a CHANCE-COMMUNITY location. Please press keyboard to draw a card.");
            Console.ReadKey();
            int cost = GenerateCost();
            if (cost < 0)
            {
                cost = -cost;
                Console.WriteLine("Your card is : YOU NEED TO PAY " + cost.ToString() + " EUROS.");
                if (currentPlayer.Money > cost)
                {
                    currentPlayer.Money -= cost;
                    Console.WriteLine("Thank you for paying, you still have " + currentPlayer.Money + " euros.");
                }
                else
                {
                    Console.WriteLine("You don't have enough money to pay... You lose the game.");
                    currentPlayer.Loser = true;
                }
            }
            else
            {
                Console.WriteLine("Your card is : LUCKY YOU ! YOU RECEIVE " + cost.ToString() + " EUROS.");
                currentPlayer.Money += cost;
                Console.WriteLine("You now have " + currentPlayer.Money + " euros.");
            }
        }

        public int GenerateCost()
        {
            int cost = 0;
            int earnOrLose = 0;

            int[] costs = new int[] { 100, 200, 500, 1000 };
            Random number = new Random();
            cost = costs[number.Next(4)]; //generate a cost between 100, 200, 500 and 1000
            earnOrLose = number.Next(2); //generate a random number between 0 and 1

            if (earnOrLose == 0)
                cost = -cost;

            return cost;
        }
    }
}
