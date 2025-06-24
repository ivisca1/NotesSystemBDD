using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using System.Threading.Tasks;

namespace Notes.Tests.Api;

[Label("notes-api")]
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
            new { Title = "Note A", Content = "Alpha content" },
            new { Title = "Note B", Content = "Beta content" }
        };

        foreach (var testCase in testCases)
        {
            await Runner.RunScenarioAsync(
                _ => Given_note_data_is_prepared(testCase.Title, testCase.Content),
                _ => When_the_note_is_posted_to_the_api(),
                _ => Then_the_response_should_be_successful(),
                _ => And_the_response_should_contain_created_note(testCase.Title, testCase.Content)
            );
        }
    }


    [Scenario]
    public async Task Failing_to_add_note_with_invalid_titles()
    {
        var invalidInputs = new[]
        {
        new { Title = "", Content = "Valid content" },
        new { Title = null as string, Content = "Also valid content" }
    };

        foreach (var input in invalidInputs)
        {
            await Runner.RunScenarioAsync(
                _ => Given_note_data_is_prepared(input.Title, input.Content),
                _ => When_the_note_is_posted_to_the_api(),
                _ => Then_the_response_should_be_bad_request()
            );
        }
    }


    [Scenario]
    public async Task Getting_list_of_notes()
    {
        string title = "Note for list";
        string content = "This note should be in the list";

        await Runner.RunScenarioAsync(
            _ => Given_note_data_is_prepared_and_posted(title, content),
            _ => When_requesting_list_of_notes(),
            _ => Then_the_response_should_include_the_created_note(title)
        );
    }

}

