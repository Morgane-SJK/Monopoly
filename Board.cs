using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Board
    {
        //Attributes
        private static Board instance;
        private static Location[] board;

        //Constructor : private because we are using the Singleton Pattern
        private Board()
        {
            board = new Location[40];
            Location_Factory locationFactory = new Location_Factory();

            for (int i = 0; i < 40; i++)
            {
                board[i] = locationFactory.createLocation(i);
            }
        }

        //GetInstance
        public static Board getInstance()
        {
            if (instance == null)
            {
                instance = new Board();
            }
            return instance;
        }

        //Good display of the board
        public void displayBoard()
        {
            for (int i = 0; i < 40; i++)
            {
                board[i].DisplayLocation();
            }
        }

        public Location getLocation(int index)
        {
            return board[index];
        }
    }
}
