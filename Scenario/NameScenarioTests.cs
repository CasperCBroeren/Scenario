using ScenarioCore;
using ScenarioTests.Name;
using Shouldly;
using System;
using Xunit;

namespace ScenarioTests
{
    public class NameScenarioTests
    {
        [Fact]
        public void BasicTest()
        {
            var s = new Scenario<NameState>();
            s.Add(new NameScenarioEvent("Kat"), new DateTime(2014, 12,4));
            s.Add(new NameScenarioEvent("Poekie"), new DateTime(2015, 3, 20));
            s.Add(new NameScenarioEvent("Necko"), new DateTime(2015, 4, 24));
            s.Add(new NameScenarioEvent("Pom"), new DateTime(2015, 12, 1));
            var state = s.GetState(new DateTime(2014, 1, 1), DateTime.Now) as NameState;
            state.Name.ShouldBe("Pom");
        }
    }
}
