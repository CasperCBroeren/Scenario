using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioCore.ScenarioTree
{
    public interface IScenarioTree<ScenarioState> where ScenarioState : IState
    {
        ScenarioState GetState(DateTime start, DateTime end);
        void Add(DateTime at, IScenarioEvent<ScenarioState> theEvent);
    }
}
