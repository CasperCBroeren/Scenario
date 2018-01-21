using ScenarioCore.ScenarioTree;
using System;

namespace ScenarioCore
{
    public class Scenario<ScenarioState> where ScenarioState : IState
    { 
        private IScenarioTree<ScenarioState> Storage = new ScenarioTimeTree<ScenarioState>();

        public void Add(IScenarioEvent<ScenarioState> theEvent, DateTime at)
        { 
            Storage.Add(at, theEvent);
        }

        public ScenarioState GetState(DateTime start, DateTime end)
        {             
            return (ScenarioState)Storage.GetState(start, end);
        }

        
    }
}
