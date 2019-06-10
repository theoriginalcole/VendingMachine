using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.models
{
    /// <summary>
    /// Base class for various coins
    /// </summary>
    public abstract class Change
    {
        public decimal value { get; protected set; }
        public int quantity { get; set; }        
    }

    public class Quarter : Change
    {
        public Quarter()
        {
            value = .25M;
            quantity = 0;
        }
    }

    public class Dime : Change
    {
        public Dime()
        {
            value = .10M;
            quantity = 0;
        }
    }

    public class Nickel : Change
    {
        public Nickel()
        {
            value = .05M;
            quantity = 0;
        }
    }

    public class Penny : Change
    {
        public Penny()
        {
            value = .01M;
            quantity = 0;
        }
    }

    /// <summary>
    /// Holds the various types of change this vending machine holds.
    /// </summary>
    public class ChangeHolder
    {
        public ChangeHolder()
        {
            Quarter = new Quarter();
            Dime = new Dime();
            Nickel = new Nickel();
            Penny = new Penny();
        }

        public Quarter Quarter { get; set; }
        public Change Dime { get; set; }
        public Change Nickel { get; set; }
        public Change Penny { get; set; }
    }

}
