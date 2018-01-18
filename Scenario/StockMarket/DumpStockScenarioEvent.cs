using ScenarioCore;
using System.Collections.Generic;

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
                newState = new StockState(new SortedSet<decimal>(), new SortedSet<decimal>());
            }
            else
            {
                newState = new StockState(prevState.AskPrices, prevState.BidPrices);
            }

            for (int i = 0; i < amount; i++)
            {
                newState.AskPrices.Add(value);
            }

            return newState;
        }
    }
}
