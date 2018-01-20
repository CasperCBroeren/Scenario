using ScenarioCore;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioTests
{
    public class DumpStockScenarioEvent : IScenarioEvent<StockState>
    { 
        private decimal value;

        public DumpStockScenarioEvent(decimal value, int amount)
        {
            this.value = value;
            this.amount = amount;
        }

        public int amount { get; }

        public StockState Execute(StockState prevState)
        {
            StockState newState = null;
            if (prevState == null)
            {
                newState = new StockState(new List<decimal>(), new List<decimal>());
            }
            else
            {
                newState = new StockState(prevState.AskPrices, prevState.BidPrices);
            }

            for (int i = 0; i < amount; i++)
            {
                if (newState.BidPrices.Count > 0 && newState.BidPrices.Last() >= value)
                {
                    //orderbook match!
                    newState.BidPrices.Remove(newState.BidPrices.Last());
                }
                else
                {
                    newState.AskPrices.Add(value);
                }
            }
            newState.AskPrices.Sort();
            newState.BidPrices.Sort();
            return newState;
        }
    }
}
