using System;
using ScenarioCore;
using ScenarioTests.Bank;
using Xunit;
using Shouldly;

namespace ScenarioTests

{
    public class BankScenarioTests
    {
        [Fact]
        public void BasicTest()
        {
            var s = new Scenario<BankState>();
            var t0 = new BankScenarioEvent(0);
            var t1 = new BankScenarioEvent(100);
            var t2 = new BankScenarioEvent(-90);
            var t3 = new BankScenarioEvent(20);
            s.Add(t0, DateTime.Now);
            s.Add(t1, DateTime.Now.AddMinutes(3));
            s.Add(t2, DateTime.Now.AddHours(2));
            s.Add(t3, DateTime.Now.AddDays(4));
            var last = s.GetState(DateTime.Now.AddHours(5)) as BankState;
            last.Balance.ShouldBe(10);
            last = s.GetState(DateTime.Now.AddDays(10)) as BankState;
            last.Balance.ShouldBe(30);
        }

        [Fact]
        public void NoZeroStartTest()
        {
            var s = new Scenario<BankState>();
            var t0 = new BankScenarioEvent(2);
            var t1 = new BankScenarioEvent(100);
            s.Add(t0, DateTime.Now);
            s.Add(t1, DateTime.Now.AddMinutes(3));
            var last  = s.GetState(DateTime.Now.AddDays(10)) as BankState;
            last.Balance.ShouldBe(102);
        }


        [Fact]
        public void NoEvents()
        {
            var s = new Scenario<BankState>();
            Should.Throw<Exception>(() =>
            {
                var last = s.GetState(DateTime.Now.AddHours(5)) as BankState;
            });
        }

        [Fact]
        public void EventOnSametime()
        {
            var s = new Scenario<BankState>();
            var t0 = new BankScenarioEvent(0);
            var t1 = new BankScenarioEvent(100);
            var t2 = new BankScenarioEvent(-90);
            var t3 = new BankScenarioEvent(20);
            s.Add(t0, DateTime.Now);
            DateTime next3Minutes = DateTime.Now.AddMinutes(3);
            s.Add(t1, next3Minutes);
            s.Add(t2, next3Minutes);
            s.Add(t3, next3Minutes);
            var last = s.GetState(DateTime.Now.AddHours(5)) as BankState; 
            last.Balance.ShouldBe(30);
        }

        [Fact]
        public void EventsInWrongOrder()
        {
            var s = new Scenario<BankState>();
            var t0 = new BankScenarioEvent(0);
            var t1 = new BankScenarioEvent(100);
            var t2 = new BankScenarioEvent(-90);
            var t3 = new BankScenarioEvent(20);
            s.Add(t0, DateTime.Now.AddMinutes(5)); 
            s.Add(t1, DateTime.Now.AddMinutes(4));
            s.Add(t2, DateTime.Now.AddMinutes(2));
            s.Add(t3, DateTime.Now.AddMinutes(1));
            var last = s.GetState(DateTime.Now.AddHours(5)) as BankState;
            last.Balance.ShouldBe(30);
        }

        [Fact]
        public void ALotOfItems()
        {
            var s = new Scenario<BankState>();
            for (var i = 0; i < 1000; i++)
            {
                var t1 = new BankScenarioEvent(2);
                DateTime next3Minutes = DateTime.Now.AddMinutes(i);
                s.Add(t1, next3Minutes);
            }
            for (var i = 0; i < 100; i++)
            {
                var t1 = new BankScenarioEvent(-2);
                DateTime next3Minutes = DateTime.Now.AddHours(3);
                s.Add(t1, next3Minutes);
            }
            var last = s.GetState(DateTime.Now.AddYears(5)) as BankState;
            last.Balance.ShouldBe(1800);
        }
    }
}
