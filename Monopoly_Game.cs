using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Monopoly_Game : Observer
    {
        //Attributes
        private static Monopoly_Game instance;
        private int round;
        private int roundMax;
        private int nbPlayers;
        private Player[] players;
        private Player currentPlayer;
        private Board board;
        private Dices dices;

        //Constructor : private because we are using the Singleton pattern
        private Monopoly_Game()
        {
            round = 1;
            roundMax = ChooseNbRounds();
            nbPlayers = ChooseNbPlayers();
            players = ChoosePlayers(nbPlayers);
            foreach (Player p in players)
            {
                p.addAddEventObserver(this);
            }
            currentPlayer = players[0];
            board = Board.getInstance();
            dices = new Dices();
        }

        //GetInstance
        public static Monopoly_Game getInstance()
        {
            if (instance == null)
            {
                instance = new Monopoly_Game();
            }
            return instance;
        }


        //Get and Set Methods
        public int Round
        {
            get { return round; }
            set { round = value; }
        }

        public int RoundMax
        {
            get { return roundMax; }
        }


        //Choose parameters of the game : number of rounds, players and their names
        public int ChooseNbRounds()
        {
            int nbRounds = 0;

            while (nbRounds == 0)
            {
                try
                {
                    Console.Write("\nHow many maximum rounds do you want to play? (1 round : every player rolls the dices once) : ");
                    nbRounds = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    nbRounds = 0;
                    Console.WriteLine("You need to answer with a positive integer ! ");
                }
            }
            return nbRounds;
        }

        public int ChooseNbPlayers()
        {
            int nbPlayers = 0;
            while (nbPlayers == 0)
            {
                try
                {
                    Console.Write("\nHow many players want to play ? ");
                    nbPlayers = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    nbPlayers = 0;
                    Console.WriteLine("You need to answer with a positive integer ! ");
                }
            }
            return nbPlayers;
        }

        public Player[] ChoosePlayers(int nbPlayers)
        {
            Console.WriteLine("\nYou chose to play with " + nbPlayers + " players.");
            Player[] players = new Player[nbPlayers];
            for (int i = 0; i < nbPlayers; i++)
            {
                Console.Write("What is the name of player " + (i + 1) + " ?  ");
                string namePlayer = Console.ReadLine();
                players[i] = new Player(namePlayer, i + 1);
            }
            return players;
        }


        //Observer function
        public override void update()
        {
            Player loser = players[0];
            for (int i = 0; i < nbPlayers; i++)
            {
                if (players[i].Loser == true)
                    loser = players[i];
            }
            Console.WriteLine("\nThe loser of the game is : " + loser.NamePlayer + "\nThank you for playing !!");
            Console.WriteLine("\nPress keyboard to exit game.");
            Console.ReadKey();
            Environment.Exit(0);
        }


        //Display function
        public void displayPlayers()
        {
            Console.WriteLine();
            Console.WriteLine("The players of the game are : ");
            for (int i = 0; i < nbPlayers; i++)
            {
                players[i].DisplayPlayer();
            }
        }

        public void displayGame()
        {
            Console.WriteLine("\n-----------------------------------\n ROUND : " + round + "/" + RoundMax + "   |   TURN OF : " + currentPlayer.NamePlayer + "\n-----------------------------------");
            Console.WriteLine("Informations about the current player : ");
            currentPlayer.DisplayPlayer();

            //Roll the dices
            Console.WriteLine("Press keyboard to roll the dices.");
            Console.ReadKey();
            int result = dices.RollDices();
            dices.DisplayDices();
            Console.WriteLine("Sum of the 2 dices : " + result.ToString());

            //2 possibilities : inJail or not ?

            //**In Jail**
            if (currentPlayer.InJail == true)
            {
                inJailStrategy(result);
            }

            //**Not in Jail**
            else //if the player is not in jail
            {
                NotInJailStrategy(result);
            }

            //Change player and tour
            changePlayerAndRound();

        }


        //2 ways of playing : if a player is in jail or if he is not
        public void inJailStrategy(int result)
        {
            //Check : do we have a double dices ?
            if (dices.DoubleDices == false)
                currentPlayer.NbDoubleDices -= 1;

            //We can leave if we made a double dices or if we failed 3 times with making a double dices
            if (currentPlayer.NbDoubleDices == 0)
            {
                Console.WriteLine("You can leave the jail in the next round!");
                currentPlayer.InJail = false;
            }
            else if (dices.DoubleDices == true)
            {
                currentPlayer.NbDoubleDices = 0;
                Console.WriteLine("You can leave the jail directly !");
                currentPlayer.InJail = false;

                //Change position of the player
                int newPosition = changePosition(result);
                currentPlayer.ActualLocation = board.getLocation(newPosition);

                //Display the rules of the new location
                board.getLocation(newPosition).displayRules(currentPlayer, board);

            }
            else
            {
                Console.WriteLine("Sorry, you can't leave the jail.");
            }
        }

        public void NotInJailStrategy(int result)
        {
            //Check : do we have a double dices ?
            if (dices.DoubleDices == true)
                currentPlayer.NbDoubleDices += 1;

            //Check if 3 times DoubleDices or not
            if (currentPlayer.NbDoubleDices == 3)
            {
                Console.WriteLine("You made a double dices 3 times in a row ! You have to go to jail !!");
                currentPlayer.InJail = true;
                currentPlayer.ActualLocation = board.getLocation(10); //the player goes to jail
            }

            else
            {
                //Change position of the player
                int newPosition = changePosition(result);
                currentPlayer.ActualLocation = board.getLocation(newPosition);

                //Display the rules of the new location
                board.getLocation(newPosition).displayRules(currentPlayer, board);
            }

        }


        public int changePosition(int result)
        {
            int actualPosition = currentPlayer.Position;
            int newPosition = actualPosition + result;
            if (newPosition > 39)
            {
                newPosition = newPosition - 40;
                if (newPosition != 0) //if newPosition=0, the player receives 400 euros, this is written in the rules of the DEPART location
                {
                    Console.WriteLine("You earn 200 euros as you passed by DEPART location!");
                    currentPlayer.Money += 200;
                }
            }
            Console.WriteLine("You have to go on : " + newPosition.ToString());
            return newPosition;
        }

        public void changePlayerAndRound()
        {
            //if not in jail and double dices
            if (currentPlayer.InJail == false && dices.DoubleDices == true && (currentPlayer.NbDoubleDices == 1 || currentPlayer.NbDoubleDices == 2))
            {
                Console.WriteLine("You got both dices with the same value so you can play again !");
            }

            else
            {
                if (currentPlayer == players[nbPlayers - 1])
                {
                    currentPlayer = players[0];
                    round++; //we change round
                }
                else
                {
                    currentPlayer = players[currentPlayer.IdPlayer]; //we stay in the same tour but we change player
                }
            }

        }


        //End of the game
        public void EndGame()
        {
            Console.WriteLine("\nThis is the end of the game ! You played " + roundMax + " rounds. ");
            Player winner = players[0];
            for (int i=0; i<nbPlayers;i++)
            {
                if (players[i].Money>winner.Money)
                {
                    winner = players[i];
                }
            }
            Console.WriteLine("The winner is " + winner.NamePlayer+ "!!!!! CONGRATULATIONS");
            Console.WriteLine("\nThe players of the game were : ");
            displayPlayers();
            Console.WriteLine("\nPress keyboard to exit game.");
        }

    }
}
