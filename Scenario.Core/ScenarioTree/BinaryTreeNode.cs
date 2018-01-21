using System;

namespace ScenarioCore.ScenarioTree
{
    public class BinaryTreeNode<ScenarioEvent> 
    {
       
        public DateTime At { get; set; }
        public ScenarioEvent Item { get; set; }
        public BinaryTreeNode<ScenarioEvent> Left { get; internal set; }
        public BinaryTreeNode<ScenarioEvent> Right { get; internal set; }

        public BinaryTreeNode(DateTime at, ScenarioEvent item)
        {
            At = at;
            Item = item; 
        }

        internal void Add(DateTime at, ScenarioEvent scenarioEvent)
        { 
            if (At < at)
            {
                if (Right == null)
                {
                    Right = new BinaryTreeNode<ScenarioEvent>(at, scenarioEvent);
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
                    Left = new BinaryTreeNode<ScenarioEvent>(at, scenarioEvent);
                }
                else
                {
                    Left.Add(at, scenarioEvent);
                }
            }
        }
    }
}
