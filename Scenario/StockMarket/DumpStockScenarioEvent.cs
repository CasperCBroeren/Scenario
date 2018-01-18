﻿using ScenarioCore;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioTests
{
    public class DumpStockScenarioEvent : IScenarioEvent<StockState>
    {
        private bool bid;
        private decimal value;

        public DumpStockScenarioEvent(decimal value, bool bid)
        {
            this.value = value;
            this.bid = bid;
        }

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

            if (bid)
            {
                if (newState.AskPrices.Count > 0 && newState.AskPrices.First() < value)
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
                if (newState.BidPrices.Count > 0 && newState.BidPrices.First() > value)
                {
                    //orderbook match!
                    newState.BidPrices.Remove(newState.BidPrices.First());
                }
                else
                {
                    newState.AskPrices.Add(value);
                }
            }
            return newState;
        }
    }
}