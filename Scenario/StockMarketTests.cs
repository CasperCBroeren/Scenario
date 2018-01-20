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
            s.Add(new StockScenarioEvent(100, true), DateTime.Now);
            s.Add(new StockScenarioEvent(80, false), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(90);
        }

        [Fact]
        public void OrderBookMatch()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, true), DateTime.Now);
            s.Add(new StockScenarioEvent(80, false), DateTime.Now);
            s.Add(new StockScenarioEvent(101, false), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(80);
        }

        [Fact]
        public void DumpStockPriceDrop()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, true), DateTime.Now);
            s.Add(new DumpStockScenarioEvent(20, 10), DateTime.Now);
            s.Add(new StockScenarioEvent(101, true), DateTime.Now);

            s.GetState(DateTime.Now).StockPrice.ShouldBe(60.5m);
        }
    }
}
