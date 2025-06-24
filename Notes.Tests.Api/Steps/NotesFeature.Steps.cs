using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Notes.WebApp.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Notes.Tests.Api;

public partial class NotesFeature
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private HttpResponseMessage _response;
    private CreateNoteDto _request;
    private Note _createdNote;

    Task Given_note_data_is_prepared(string title, string content)
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();

        _request = new CreateNoteDto
        {
            Title = title,
            Content = content
        };

        return Task.CompletedTask;
    }

    async Task When_the_note_is_posted_to_the_api()
    {
        _response = await _client.PostAsJsonAsync("/api/notes", _request);
    }

    async Task Then_the_response_should_be_successful()
    {
        _response.EnsureSuccessStatusCode();
    }

    async Task Then_the_response_should_be_bad_request()
    {
        Assert.Equal(HttpStatusCode.BadRequest, _response.StatusCode);
    }

    async Task And_the_response_should_contain_created_note(string expectedTitle, string expectedContent)
    {
        var result = await _response.Content.ReadFromJsonAsync<Note>();
        Assert.NotNull(result);
        Assert.Equal(expectedTitle, result.Title);
        Assert.Equal(expectedContent, result.Content);
        _createdNote = result;
    }

    async Task Given_note_data_is_prepared_and_posted(string title, string content)
    {
        await Given_note_data_is_prepared(title, content);
        await When_the_note_is_posted_to_the_api();
        await And_the_response_should_contain_created_note(title, content);
    }


    async Task When_requesting_list_of_notes()
    {
        _response = await _client.GetAsync("/api/notes");
    }

    async Task Then_the_response_should_include_the_created_note(string expectedTitle)
    {
        var list = await _response.Content.ReadFromJsonAsync<List<Note>>();
        Assert.Contains(list, n => n.Title == expectedTitle);
    }

}

