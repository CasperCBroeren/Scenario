using System;
using System.Collections.Generic;

namespace ScenarioCore.ScenarioTree
{
    public class ScenarioTimeNode<ScenarioEvent>
    {
        public SortedList<DateTime, List<ScenarioEvent>> Items { get; internal set; }

        public ScenarioTimeNode()
        {
            Items = new SortedList<DateTime, List<ScenarioEvent>>();
        }

        internal void Add(DateTime at, ScenarioEvent scenarioEvent)
        {
            if (!Items.ContainsKey(at))
            {                
                Items.Add(at, new List<ScenarioEvent>());
            }            
            Items[at].Add(scenarioEvent);
        }
    }
}
