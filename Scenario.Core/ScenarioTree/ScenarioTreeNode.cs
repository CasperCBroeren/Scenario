using System;

namespace ScenarioCore.ScenarioTree
{
    public class ScenarioTreeNode<ScenarioEvent> 
    {
        public bool Dirty { get; set; }

        public DateTime At { get; set; }
        public ScenarioEvent Item { get; set; }
        public ScenarioTreeNode<ScenarioEvent> Left { get; internal set; }
        public ScenarioTreeNode<ScenarioEvent> Right { get; internal set; }
        public IState CachedState { get; internal set; }

        public ScenarioTreeNode(DateTime at, ScenarioEvent item)
        {
            At = at;
            Item = item;
            Dirty = true;
        }

        internal void Add(DateTime at, ScenarioEvent scenarioEvent)
        {
            Dirty = true;
            if (At < at)
            {
                if (Right == null)
                {
                    Right = new ScenarioTreeNode<ScenarioEvent>(at, scenarioEvent);
                }
                else
                {
                    Right.Add(at, scenarioEvent);
                }

            }
            else
            {
                if (Left == null)
                {
                    Left = new ScenarioTreeNode<ScenarioEvent>(at, scenarioEvent);
                }
                else
                {
                    Left.Add(at, scenarioEvent);
                }
            }
        }
    }
}
