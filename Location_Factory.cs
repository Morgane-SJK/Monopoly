using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX3_Monopoly
{
    class Location_Factory
    {
        private int[] chanceOrCommunity = new int[] { 2, 7, 17, 22, 33, 36 };

        public Location createLocation(int id)
        {

            if (id == 10)
            {
                return new Location_Jail(id);
            }
            else if (chanceOrCommunity.Contains(id))
            {
                return new Location_Chance_Community(id);
            }
            else
            {
                return new Location_Property(id);
            }
        }
    }
}
