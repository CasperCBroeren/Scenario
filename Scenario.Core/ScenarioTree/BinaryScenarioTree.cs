using System;

namespace ScenarioCore.ScenarioTree
{
    public class BinaryScenarioTree<ScenarioState> : IScenarioTree<ScenarioState>  where ScenarioState : IState

    {
        private BinaryTreeNode<IScenarioEvent<ScenarioState>> root;


        public BinaryScenarioTree()
        {

        }

        public ScenarioState GetState(DateTime start, DateTime end)
        {
            return ExecuteNode(start, end, root, default(ScenarioState));
        }

        private ScenarioState ExecuteNode(DateTime start, DateTime end, BinaryTreeNode<IScenarioEvent<ScenarioState>> node, ScenarioState state)
        {
            if (node == null) throw new Exception("No data is added to the scenario");

            if (node.At > start && node.At < end)
            {
                if (node.Left != null)
                {
                    state = ExecuteNode(start, end, node.Left, state);
                }


                state = node.Item.Execute(state);

                if (node.Right != null)
                {
                    state = ExecuteNode(start, end, node.Right, state);
                }
            }

            return state;

        }


        public void Add(DateTime at, IScenarioEvent<ScenarioState> scenarioEvent)
        {
            if (root == null)
            {
                root = new BinaryTreeNode<IScenarioEvent<ScenarioState>>(at, scenarioEvent);
            }
            else
            {
                root.Add(at, scenarioEvent);
            }
        }


    }
}
