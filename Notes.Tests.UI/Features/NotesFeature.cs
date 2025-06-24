using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using System.Threading.Tasks;

namespace Notes.Tests.UI;

[Label("notes-ui")]
[FeatureDescription(
    @"In order to manage my personal notes
      As a user
      I want to add and view notes")]
public partial class NotesFeature : FeatureFixture
{
    [Scenario]
    public async Task Successfully_adding_multiple_notes()
    {
        var testCases = new[]
        {
            new { Title = "UI Note A", Content = "First content" },
            new { Title = "UI Note B", Content = "Second content" }
        };

        foreach (var testCase in testCases)
        {
            await Runner.RunScenarioAsync(
                _ => Given_browser_is_on_notes_page(),
                _ => When_user_enters_note_data(testCase.Title, testCase.Content),
                _ => And_user_submits_the_form(),
                _ => Then_the_note_should_appear_in_the_list(testCase.Title, testCase.Content)
            );
        }
    }

    [Scenario]
    public async Task Failing_to_add_note_with_empty_title()
    {
        await Runner.RunScenarioAsync(
            _ => Given_browser_is_on_notes_page(),
            _ => When_user_enters_note_data("", "Some content"),
            _ => And_user_submits_the_form(),
            _ => Then_validation_message_should_be_displayed("The Title field is required.")
        );
    }
}
