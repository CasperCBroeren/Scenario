namespace ScenarioCore
{
    public interface IScenarioEvent<ScenarioState> where ScenarioState : IState
    {
        ScenarioState Execute(ScenarioState state);
 
    }
}
