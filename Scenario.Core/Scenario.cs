using ScenarioCore.ScenarioTree;
using System;

namespace ScenarioCore
{
    public class Scenario<ScenarioState> where ScenarioState : IState
    { 
        private ScenarioTree<ScenarioState> Storage = new ScenarioTree<ScenarioState>();

        public void Add(IScenarioEvent<ScenarioState> theEvent, DateTime at)
        { 
            Storage.Add(at, theEvent);
        }

        public ScenarioState GetState(DateTime dateTime)
        {             
            return (ScenarioState)Storage.ExecuteUntil(dateTime);
        }

        
    }
}
