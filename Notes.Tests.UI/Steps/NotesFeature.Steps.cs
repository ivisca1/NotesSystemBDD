using Notes.Tests.UI.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.UI;

public partial class NotesFeature : IDisposable
{
    private IWebDriver _driver;
    private NotesPage _notesPage;

    public NotesFeature()
    {
        var options = new ChromeOptions();
        options.AddArgument("--window-size=1920,1080");
        _driver = new ChromeDriver(options);
        _notesPage = new NotesPage(_driver);
    }

    public void Dispose()
    {
        _driver?.Quit();
    }

    Task Given_browser_is_on_notes_page()
    {
        _notesPage.Navigate();
        return Task.CompletedTask;
    }

    Task When_user_enters_note_data(string title, string content)
    {
        _notesPage.EnterNote(title, content);
        return Task.CompletedTask;
    }

    Task And_user_submits_the_form()
    {
        _notesPage.SubmitNote();
        return Task.CompletedTask;
    }

    Task Then_the_note_should_appear_in_the_list(string expectedTitle, string expectedContent)
    {
        Assert.True(_notesPage.NoteIsVisible(expectedTitle, expectedContent));
        return Task.CompletedTask;
    }

    Task Then_validation_message_should_be_displayed(string expectedMessage)
    {
        var message = _notesPage.GetValidationMessage();
        Assert.Contains(expectedMessage, message);
        return Task.CompletedTask;
    }
}
