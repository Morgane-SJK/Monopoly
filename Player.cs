using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Player
    {
        //Attributes
        private string namePlayer;
        private int idPlayer;
        private Location actualLocation;
        private int money;
        private bool inJail;
        private int nbDoubleDices;
        private bool loser;
        private List<Observer> addEventObservers;
        //private Observer_Ex3 testobserver;


        //Constructor
        public Player(string name, int id)
        {
            this.namePlayer = name;
            this.idPlayer = id;
            actualLocation = new Location_Property(0);
            money = 1500;
            inJail = false;
            nbDoubleDices = 0;
            loser = false;
            addEventObservers = new List<Observer>();
        }

        public void addAddEventObserver(Observer o)
        {
            addEventObservers.Add(o);
            //testobserver = o;
        }

        public void notifyAllObservers()
        {
            foreach (Observer o in addEventObservers)
            {
                o.update();
            }
            //testobserver.update();
        }

        //Get and Set methods
        public string NamePlayer
        {
            get { return namePlayer; }
        }

        public int IdPlayer
        {
            get { return idPlayer; }
        }

        public int Position
        {
            get { return actualLocation.IdLocation; }
        }

        public Location ActualLocation
        {
            get { return actualLocation; }
            set { actualLocation = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public bool InJail
        {
            get { return inJail; }
            set { inJail = value; }
        }

        public int NbDoubleDices
        {
            get { return nbDoubleDices; }
            set { nbDoubleDices = value; }
        }

        public bool Loser
        {
            get { return loser; }
            set { loser = value; notifyAllObservers(); }
        }

        public void DisplayPlayer()
        {
            Console.WriteLine("Name : " + namePlayer + "   Id : " + idPlayer + "   Money : " + money + "   Actual location : ");
            actualLocation.DisplayLocation();
        }
    }
}
