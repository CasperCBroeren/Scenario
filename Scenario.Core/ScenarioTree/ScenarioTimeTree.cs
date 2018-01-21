using System;
using System.Collections.Generic;
using System.Linq;

namespace ScenarioCore.ScenarioTree
{
    public enum Partition
    {
        Second,
        Minute,
        Hour,
        Day,
        // No month and day because they have ireggularties  
    }

    public class ScenarioTimeTree<ScenarioState> : IScenarioTree<ScenarioState> where ScenarioState : IState
    {
        private Dictionary<long, ScenarioTimeNode<IScenarioEvent<ScenarioState>>> Partions { get; set; }
        public Partition PartionBy { get; set; }
        public ScenarioTimeTree()
        {
            Partions = new Dictionary<long, ScenarioTimeNode<IScenarioEvent<ScenarioState>>>();
        }

        public void Add(DateTime at, IScenarioEvent<ScenarioState> theEvent)
        {
            var tick = GetTimePos(at);
            if (!Partions.ContainsKey(tick))
            {
                Partions.Add(tick, new ScenarioTimeNode<IScenarioEvent<ScenarioState>>());
            }
            Partions[tick].Add(at, theEvent);
        }

        public ScenarioState GetState(DateTime start, DateTime end)
        {
            var state = default(ScenarioState);
            var sortedPartitions = Partions.OrderBy(x => x.Key);
            foreach (var part in sortedPartitions)
            {
                if (part.Key > GetTimePos(start) && part.Key <= GetTimePos(end))
                {
                    foreach (var moment in part.Value.Items)
                    {
                        foreach (var item in moment.Value)
                            state = item.Execute(state);
                    }
                }

            }
            if (state == null)
            {
                throw new Exception("There is no state!");
            }
            return state;
        }

        private long TicksPerPartition()
        {
            switch (PartionBy)
            {
                case Partition.Second:
                    return TimeSpan.TicksPerSecond;
                case Partition.Minute:
                    return TimeSpan.TicksPerMinute;
                case Partition.Hour:
                    return TimeSpan.TicksPerHour;
                case Partition.Day:
                    return TimeSpan.TicksPerDay;
            }
            return 0;
        }

        private long GetTimePos(DateTime at)
        {
            switch (PartionBy)
            {
                case Partition.Second:
                    return new DateTime(at.Year, at.Month, at.Day, at.Hour, at.Minute, at.Second).Ticks;
                case Partition.Minute:
                    return new DateTime(at.Year, at.Month, at.Day, at.Hour, at.Minute, 0).Ticks;
                case Partition.Hour:
                    return new DateTime(at.Year, at.Month, at.Day, at.Hour, 0, 0).Ticks;
                case Partition.Day:
                    return new DateTime(at.Year, at.Month, at.Day, 0, 0, 0).Ticks;
            }
            return 0;
        }


    }
}
