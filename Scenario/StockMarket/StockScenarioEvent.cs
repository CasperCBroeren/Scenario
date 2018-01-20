﻿using ScenarioCore;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioTests
{
    public class StockScenarioEvent : IScenarioEvent<StockState>
    {
        private bool bid;
        private decimal value;

        public StockScenarioEvent(decimal value, bool bid)
        {
            this.value = value;
            this.bid = bid;
        }

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

            if (bid)
            {
                if (newState.AskPrices.Count > 0 && newState.AskPrices.First() >= value)
                {
                    //orderbook match!
                    newState.AskPrices.Remove(newState.AskPrices.First());
                }
                else
                {
                    newState.BidPrices.Add(value);
                }
            }
            else
            {
                if (newState.BidPrices.Count > 0 && newState.BidPrices.Last() <= value)
                {
                    //orderbook match!
                    newState.BidPrices.Remove(newState.BidPrices.Last());
                }
                else
                {
                    newState.AskPrices.Add(value);
                }
            }
            newState.AskPrices.Sort(); ;
            return newState;
        }
    }
}
