using ScenarioCore;
using Shouldly;
using System;
using Xunit;

namespace ScenarioTests
{
    public class StockMarketTests
    { 
        [Fact]
        public void BasicTest()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Bid), DateTime.Now);
            s.Add(new StockScenarioEvent(80, OrderType.Ask), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(0);
        }

        [Fact]
        public void BasicTestReverse()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Ask), DateTime.Now);
            s.Add(new StockScenarioEvent(80, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(90);
        }

        [Fact]
        public void OrderBookMatch()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Ask), DateTime.Now);
            s.Add(new StockScenarioEvent(80, OrderType.Bid), DateTime.Now);
            s.Add(new StockScenarioEvent(101, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(80);
        }

        [Fact]
        public void DumpStockPriceDrop()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Bid), DateTime.Now);
            s.Add(new DumpStockScenarioEvent(20, 10), DateTime.Now);
            s.Add(new StockScenarioEvent(101, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(20);
        }

      
    }
}
