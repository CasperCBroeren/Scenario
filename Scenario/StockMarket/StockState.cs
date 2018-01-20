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
                return AskPrices.Count > 0 && BidPrices.Count > 0 ? (AskPrices.First() + BidPrices.Last()) / 2:
                        AskPrices.Count >0 && BidPrices.Count == 0 ? AskPrices.First() :
                        BidPrices.Count > 0 && AskPrices.Count == 0 ? BidPrices.Last() : 0;
            }
        }

        public List<decimal> AskPrices { get; private set; }
        public List<decimal> BidPrices { get; private set; }

        public StockState(List<decimal> askPrices, List<decimal> bidPrices)
        {
            AskPrices = askPrices;
            BidPrices = bidPrices;
        }
    }
}
