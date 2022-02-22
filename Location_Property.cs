using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Location_Property : Location
    {
        private int price;

        public Location_Property(int id)
        {
            idLocation = id;
            price = id * 50;
            if (idLocation == 0)
            {
                nameLocation = "DEPART";
            }

            else if (idLocation == 30)
                nameLocation = "GO TO JAIL";
            else
                nameLocation = "PROPERTY";
        }

        public int Price
        {
            get { return price; }
        }

        public override void displayRules(Player currentPlayer, Board board)
        {
            if (idLocation == 0)
            {
                Console.WriteLine("You earn 400 euros instead of 200 euros because you arrived exactly on the DEPART location.");
                currentPlayer.Money += 400;
            }
            else if (idLocation == 30)
                GoToJail(currentPlayer, board);
            else
                BuyOrNot(currentPlayer);
        }

        public void BuyOrNot(Player currentPlayer)
        {
            if (owner == null)
            {
                Console.WriteLine("You are on the property " + idLocation + ", which has no owner yet. Do you want to buy it for " + Price.ToString() + " euros ? (y/n) ");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "Y")
                {
                    if (currentPlayer.Money < Price)
                        Console.WriteLine("Sorry you don't have enough money for this.");
                    else
                    {
                        currentPlayer.Money = currentPlayer.Money - Price;
                        owner = currentPlayer;
                        Console.WriteLine("You are now the owner of property " + idLocation + " and you still have " + currentPlayer.Money + " euros.");
                    }
                }
                else if (answer == "n" || answer == "N")
                    Console.WriteLine("Ok. Maybe you can buy it next time.");
                else
                {
                    Console.WriteLine("You need to answer with y or n!");
                    BuyOrNot(currentPlayer);
                }
            }
            else
            {
                if (owner == currentPlayer)
                {
                    Console.WriteLine("You are on your own property ! ");
                }
                else //the player is on the property of someone else
                {
                    Console.WriteLine("You are on property " + idLocation + ", which is the property of " + owner.NamePlayer);
                    int priceToPay = Price / 10;
                    Console.WriteLine("You need to pay " + priceToPay + " euros to " + owner.NamePlayer);
                    if (currentPlayer.Money < priceToPay)
                    {
                        Console.WriteLine("You don't have enough money to pay... You lose the game.");
                        currentPlayer.Loser = true;
                    }
                    else
                    {
                        currentPlayer.Money -= priceToPay;
                        owner.Money += priceToPay;
                        Console.WriteLine("Thank you for paying. You still have " + currentPlayer.Money + " euros.");
                        Console.WriteLine("The owner of this property has now " + owner.Money + " euros.");
                    }
                }

            }
        }

        public void GoToJail(Player currentPlayer, Board board)
        {
            Console.WriteLine("You have to go to jail !");
            currentPlayer.ActualLocation = board.getLocation(10);
            currentPlayer.InJail = true;
            currentPlayer.NbDoubleDices = 3;
        }

    }
}
