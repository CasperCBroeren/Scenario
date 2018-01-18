using System;

namespace ScenarioCore.ScenarioTree
{
    public class ScenarioTree<ScenarioState> where ScenarioState : IState 
    {
        private ScenarioTreeNode<IScenarioEvent<ScenarioState>> root;
          
         
        public ScenarioTree()
        {
             
        }

        public IState ExecuteUntil(DateTime until)
        {
            return ExecuteNode(until, root, default(ScenarioState));
        }

        private ScenarioState ExecuteNode(DateTime until, ScenarioTreeNode<IScenarioEvent<ScenarioState>> node, ScenarioState state)
        {
            if (node == null) throw new Exception("No data is added to the scenario");
           
            if (node.At < until)
            {
                if (node.Left != null)
                {
                    state= ExecuteNode(until, node.Left, state);
                }

                if (!node.Dirty)
                {
                    state = (ScenarioState)node.CachedState;
                }
                else
                {
                    state = node.Item.Execute(state);
                    node.CachedState = state; 
                    node.Dirty = false;
                }
                if (node.Right != null)
                {
                    state=  ExecuteNode(until, node.Right, state);
                }
            }
            
            return state;
              
        }


        public void Add(DateTime at, IScenarioEvent<ScenarioState> scenarioEvent)
        {
            if (root == null)
            {
                root = new ScenarioTreeNode<IScenarioEvent<ScenarioState>>(at, scenarioEvent);
            }
            else
            {
                root.Add(at, scenarioEvent);
            }
        }

        
    }
}
