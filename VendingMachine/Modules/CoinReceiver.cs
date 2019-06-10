using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.models;

namespace VendingMachine.Modules
{
    /// <summary>
    /// Collects and counts inserted change.  Assumes that change will be added to change maker.
    /// </summary>
    class CoinReceiver
    {
        private decimal changeCollected = 0;

        public void InsertChange(Change change)
        {
            changeCollected += change.value;
        }

        public decimal TotalChangeInserted()
        {
            return changeCollected;
        }

        public void ClearChangeCounter()
        {
            changeCollected = 0;
        }
    }
}
