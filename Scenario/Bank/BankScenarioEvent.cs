using ScenarioCore;

namespace ScenarioTests.Bank
{
    public class BankScenarioEvent : IScenarioEvent<BankState>
    {
        public int Mutation { get; set; }

        public BankScenarioEvent(int mutation)
        {
            Mutation = mutation;
        }

        public BankState Execute(BankState state)
        {
            var change = Mutation;
            if (state != null)
            {
                change = state.Balance + Mutation;
            }

            return new BankState() { Balance = change };

        }
    }
}
