using Hse.Platform.Workflow;
using Shouldly;
using Xunit;

namespace Hse.Platform.Workflow;

public class WorkflowDefinition_Tests
{
    [Fact]
    public void Should_Allow_Configured_Transition()
    {
        var definition = new WorkflowDefinition(System.Guid.NewGuid(), "health.medical-examination");
        definition.AddState("draft", "Draft", isInitial: true);
        definition.AddState("submitted", "Submitted");
        definition.AddTransition("draft", "submitted");

        definition.GetInitialState().ShouldBe("draft");
        definition.CanTransition("draft", "submitted").ShouldBeTrue();
        definition.CanTransition("submitted", "draft").ShouldBeFalse();
    }
}
