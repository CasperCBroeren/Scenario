using ScenarioCore;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioTests
{
    public class StockState : IState
    {
        public decimal StockPrice
        {
            get
            {
                return AskPrices.Count > 0 && BidPrices.Count > 0 ? (AskPrices.First() + BidPrices.First()) / 2:
                        AskPrices.Count >0 && BidPrices.Count == 0 ? AskPrices.First() :
                        BidPrices.Count > 0 && AskPrices.Count == 0 ? BidPrices.First() : 0;
            }
        }
        /// <summary>
        /// TODO: Change the sortedlist to something more elegant
        /// </summary>
        public SortedSet<decimal> AskPrices { get; private set; }
        public SortedSet<decimal> BidPrices { get; private set; }

        public StockState(SortedSet<decimal> askPrices, SortedSet<decimal> bidPrices)
        {
            AskPrices = askPrices;
            BidPrices = bidPrices;
        }
    }
}
