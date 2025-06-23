using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace Notes.Tests.Api;

[Label("notes-api")]
[FeatureDescription(
    @"In order to manage my personal notes
      As a user
      I want to add and view notes through the API")]
public partial class NotesFeature : FeatureFixture
{
    [Scenario]
    public async void Successfully_adding_a_note()
    {
        await Runner.RunScenarioAsync(
            Given_valid_note_data_is_prepared,
            When_the_note_is_posted_to_the_api,
            Then_the_response_should_be_successful,
            And_the_response_should_contain_created_note
        );
    }

    [Scenario]
    public async void Failing_to_add_note_with_empty_title()
    {
        await Runner.RunScenarioAsync(
            Given_note_data_with_empty_title_is_prepared,
            When_the_note_is_posted_to_the_api,
            Then_the_response_should_be_bad_request
        );
    }

    [Scenario]
    public async void Getting_list_of_notes()
    {
        await Runner.RunScenarioAsync(
            Given_note_data_is_prepared_and_posted,
            When_requesting_list_of_notes,
            Then_the_response_should_include_the_created_note
        );
    }
}

