using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Program
    {
        //Test Class Player
        static void TestClassPlayer()
        {
            Player player1 = new Player("momo", 1); //Test Constructor
            player1.DisplayPlayer(); //Test DisplayPlayer()
            Console.WriteLine("After updating the money of player1 :"); //Tests Get/Set Methods
            player1.Money += 1000;
            player1.DisplayPlayer();
        }

        //Test Class Location
        static void TestClassLocation()
        {
            Player player1 = new Player("momo", 1);

            Location_Factory factory = new Location_Factory();

            Board board = Board.getInstance();

            //Test Location_Property
            Console.WriteLine("--------TEST Location_Property--------");
            Location location1 = factory.createLocation(1);
            Console.WriteLine("Display of the location before playing : ");
            location1.DisplayLocationAndOwner();

            Console.WriteLine();
            location1.displayRules(player1, board);

            Console.WriteLine("\nDisplay of the location after playing : ");
            location1.DisplayLocationAndOwner();


            //Test LocationGoToJail
            Console.WriteLine("\n--------TEST Location_Property--------");
            Location location2 = factory.createLocation(30);
            Console.WriteLine("Display of the location before playing : ");
            location2.DisplayLocationAndOwner();

            Console.WriteLine();
            location2.displayRules(player1, board);

            Console.WriteLine("\nDisplay of the player after playing : ");
            player1.DisplayPlayer();


            //Test LocationChanceOrCommunity
            Console.WriteLine("\n--------TEST LocationChanceOrCommunity--------");
            Location location3 = factory.createLocation(2);
            Console.WriteLine("Display of the location before playing : ");
            location3.DisplayLocationAndOwner();

            Console.WriteLine();
            location3.displayRules(player1, board);

            Console.WriteLine("\nDisplay of the player after playing : ");
            player1.DisplayPlayer();
        }

        //Test Class Board
        static void TestClassBoard()
        {
            Board board1 = Board.getInstance();
            Board board2 = Board.getInstance();

            Console.WriteLine(board1 == board2); //returns True so our Singleton Class works
            board1.displayBoard();

            //Test access to a particular location on the board
            board1.getLocation(0).DisplayLocation();
        }

        //Test Class Dices
        static void TestClassDices()
        {
            Dices dices = new Dices();
            dices.DisplayDices();
            dices.RollDices();
            dices.DisplayDices();
        }

        //Test Class Monopoly_Game
        static void TestClassMonopoly()
        {
            Console.WriteLine("--------------WELCOME TO OUR ONLINE MONOPOLY !!!--------------");

            Monopoly_Game monop = Monopoly_Game.getInstance();

            monop.displayPlayers();
            while (monop.Round <= monop.RoundMax)
            {
                monop.displayGame();
            }
            monop.EndGame();
        }



        static void Main(string[] args)
        {

            //TestClassPlayer();

            //TestClassLocation();

            //TestClassBoard();

            //TestClassDices();

            TestClassMonopoly();



            Console.ReadKey();
        }
    }
}
