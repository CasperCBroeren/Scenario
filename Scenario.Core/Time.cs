
using System;

namespace ScenarioCore
{
    public class Time
    {
        public DateTime Now { get; private set; }
        public int ProgressPerSecond { get; }
        private DateTime lastTick;

        public Time(int progressPerSecond, DateTime start)
        {
            ProgressPerSecond = progressPerSecond;
            lastTick = DateTime.Now;
            Now = start;
        }

        public void JumpTo(DateTime moment)
        {
            Now = moment;
        }

        public void Tick()
        {
            var workingMoment = DateTime.Now;
            var delta = workingMoment.Subtract(lastTick).TotalSeconds;
            var add = delta * ProgressPerSecond;
            Now = Now.AddSeconds(add);
            lastTick = workingMoment;
        }
    }
}
