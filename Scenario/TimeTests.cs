using ScenarioCore;
using System;
using Xunit;

namespace ScenarioTests
{
    public class TimeTests
    {
        [Fact]
        public void BasicTest()
        {           
            DateTime start = DateTime.Now;
            var timeLine = new Time(60, start);
            while (timeLine.Now < start.AddMinutes(10))
            {
                timeLine.Tick();
                System.Threading.Thread.Sleep(100);
            }

        }
    }
}
