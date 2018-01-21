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

            s.GetState(DateTime.Now.AddDays(-1), DateTime.Now).StockPrice.ShouldBe(0);
        }

        [Fact]
        public void BasicTestReverse()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Ask), DateTime.Now);
            s.Add(new StockScenarioEvent(80, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now.AddDays(-1), DateTime.Now).StockPrice.ShouldBe(90);
        }

        [Fact]
        public void OrderBookMatch()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Ask), DateTime.Now);
            s.Add(new StockScenarioEvent(80, OrderType.Bid), DateTime.Now);
            s.Add(new StockScenarioEvent(101, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now.AddDays(-1), DateTime.Now).StockPrice.ShouldBe(80);
        }

        [Fact]
        public void DumpStockPriceDrop()
        {
            var s = new Scenario<StockState>();
            s.Add(new StockScenarioEvent(100, OrderType.Bid), DateTime.Now);
            s.Add(new DumpStockScenarioEvent(20, 10), DateTime.Now);
            s.Add(new StockScenarioEvent(101, OrderType.Bid), DateTime.Now);

            s.GetState(DateTime.Now.AddDays(-1), DateTime.Now).StockPrice.ShouldBe(20);
        }

        [Fact]
        public void JustAlotofRandom()
        {
            var random = new Random(System.Environment.TickCount);
            var s = new Scenario<StockState>();
            for (var i = 0; i < 10000; i++)
            {
                if (random.Next(0, 1) == 1)
                {
                    s.Add(new StockScenarioEvent(random.Next(50, 65), OrderType.Bid), DateTime.Now.AddSeconds(random.Next(-100, 100)));
                }
                else
                {
                    s.Add(new StockScenarioEvent(random.Next(60, 75), OrderType.Ask), DateTime.Now.AddSeconds(random.Next(-100, 100)));
                }
            }
            var result = s.GetState(DateTime.Now.AddDays(-1), DateTime.Now.AddSeconds(120)).StockPrice;
            result.ShouldBeGreaterThan(50);
            result.ShouldBeLessThan(75);
            Console.WriteLine(result);
        }
    }
}
