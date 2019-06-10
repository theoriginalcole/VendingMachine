using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.models;

namespace VendingMachine.Modules
{
    class ChangeMaker
    {
        private ChangeHolder ChangeHolder;

        public ChangeMaker()
        {
            ChangeHolder = new ChangeHolder();
        }

        /// <summary>
        /// Allows for adding change to the existing change in the vending machine.
        /// </summary>
        /// <param name="change"></param>
        public void LoadChange(ChangeHolder change)
        {
            ChangeHolder.Quarter.quantity += change.Quarter.quantity;
            ChangeHolder.Dime.quantity += change.Dime.quantity;
            ChangeHolder.Nickel.quantity += change.Nickel.quantity;
            ChangeHolder.Penny.quantity += change.Penny.quantity;
        }

        /// <summary>
        /// Allows us to empty out the change holder if desired
        /// </summary>
        public void EmptyChange()
        {
            ChangeHolder.Quarter.quantity = 0;
            ChangeHolder.Dime.quantity = 0;
            ChangeHolder.Nickel.quantity = 0;
            ChangeHolder.Penny.quantity = 0;
        }

        /// <summary>
        /// Calculates the amount of change to dispense.  If a certain coin has run out, it moves on the next one.
        /// </summary>
        /// <param name="AmountInserted"></param>
        /// <param name="ItemPrice"></param>
        /// <returns></returns>
        public ChangeHolder MakeChange(decimal AmountInserted, decimal ItemPrice)
        {
            ChangeHolder CoinsGiven = new ChangeHolder();
            decimal AmountOfChangeToMake = AmountInserted - ItemPrice;
            if (AmountOfChangeToMake < 0)
                throw new Exception("Not enough money inserted to purchase item!");
            if (AmountOfChangeToMake == 0)
                return CoinsGiven;
            while (AmountOfChangeToMake != 0)
            {
                if (ChangeHolder.Quarter.quantity > 0 && AmountOfChangeToMake % ChangeHolder.Quarter.value == 0)
                {
                    CoinsGiven.Quarter.quantity++;
                    AmountOfChangeToMake -= ChangeHolder.Quarter.value;
                    ChangeHolder.Quarter.quantity--;
                }
                else if (ChangeHolder.Dime.quantity > 0 && AmountOfChangeToMake % ChangeHolder.Dime.value == 0)
                {
                    CoinsGiven.Dime.quantity++;
                    AmountOfChangeToMake -= ChangeHolder.Dime.value;
                    ChangeHolder.Dime.quantity--;
                }
                else if (ChangeHolder.Nickel.quantity > 0 && AmountOfChangeToMake % ChangeHolder.Nickel.value == 0)
                {
                    CoinsGiven.Nickel.quantity++;
                    AmountOfChangeToMake -= ChangeHolder.Nickel.value;
                    ChangeHolder.Nickel.quantity--;
                }
                else if (ChangeHolder.Penny.quantity > 0 && AmountOfChangeToMake % ChangeHolder.Penny.value == 0)
                {
                    CoinsGiven.Penny.quantity++;
                    AmountOfChangeToMake -= ChangeHolder.Penny.value;
                    ChangeHolder.Penny.quantity--;
                }
                else
                {
                    throw new Exception("Unable to make enough change!");
                }
            }

            return CoinsGiven;
        }

        /// <summary>
        /// Check to see if there is enough change before vending item.
        /// </summary>
        /// <param name="AmountInserted"></param>
        /// <param name="ItemPrice"></param>
        /// <returns></returns>
        public bool EnoughChange(decimal AmountInserted, decimal ItemPrice)
        {
            ChangeHolder tempChangeHolder = new ChangeHolder();
            tempChangeHolder.Quarter.quantity = ChangeHolder.Quarter.quantity;
            tempChangeHolder.Dime.quantity = ChangeHolder.Dime.quantity;
            tempChangeHolder.Nickel.quantity = ChangeHolder.Nickel.quantity;
            tempChangeHolder.Penny.quantity = ChangeHolder.Penny.quantity;
            decimal AmountOfChangeToMake = AmountInserted - ItemPrice;
            if (AmountOfChangeToMake < 0)
                throw new Exception("Not enough money inserted to purchase item!");
            if (AmountOfChangeToMake == 0)
                return true;
            while (AmountOfChangeToMake != 0)
            {
                if (tempChangeHolder.Quarter.quantity > 0 && AmountOfChangeToMake % tempChangeHolder.Quarter.value == 0)
                {
                    tempChangeHolder.Quarter.quantity--;
                    AmountOfChangeToMake -= ChangeHolder.Quarter.value;
                }
                else if (tempChangeHolder.Dime.quantity > 0 && AmountOfChangeToMake % tempChangeHolder.Dime.value == 0)
                {
                    tempChangeHolder.Dime.quantity--;
                    AmountOfChangeToMake -= ChangeHolder.Dime.value;
                }
                else if (tempChangeHolder.Nickel.quantity > 0 && AmountOfChangeToMake % tempChangeHolder.Nickel.value == 0)
                {
                    tempChangeHolder.Nickel.quantity--;
                    AmountOfChangeToMake -= ChangeHolder.Nickel.value;
                }
                else if (tempChangeHolder.Penny.quantity > 0 && AmountOfChangeToMake % tempChangeHolder.Penny.value == 0)
                {
                    tempChangeHolder.Penny.quantity--;
                    AmountOfChangeToMake -= ChangeHolder.Penny.value;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
