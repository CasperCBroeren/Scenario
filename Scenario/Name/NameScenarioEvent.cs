using ScenarioCore;
namespace ScenarioTests.Name
{
    public class NameScenarioEvent : IScenarioEvent<NameState>
    {
        public string NewName { get; }

        public NameScenarioEvent(string newName)
        {
            this.NewName = newName;
        }

        public NameState Execute(NameState state)
        { 
            if (state == null)
            {
                return new NameState() { Name = NewName, Changed = 0 };
            }
            if (!state.Equals(NewName))
            {
                return new NameState() { Name = NewName, Changed = state.Changed + 1 };
            }

            return state;
        }
    }
}
