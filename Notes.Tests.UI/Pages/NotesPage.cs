using OpenQA.Selenium;
using System.Linq;

namespace Notes.Tests.UI.Pages;

public class NotesPage
{
    private readonly IWebDriver _driver;

    public NotesPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl("https://localhost:7006/Notes");
    }

    public void EnterNote(string title, string content)
    {
        _driver.FindElement(By.Id("NewNote_Title")).Clear();
        _driver.FindElement(By.Id("NewNote_Title")).SendKeys(title);
        _driver.FindElement(By.Id("NewNote_Content")).Clear();
        _driver.FindElement(By.Id("NewNote_Content")).SendKeys(content);
    }

    public void SubmitNote()
    {
        _driver.FindElement(By.CssSelector("form#addNoteForm button[type=submit]")).Click();
    }

    public bool NoteIsVisible(string expectedTitle, string expectedContent)
    {
        var listItems = _driver.FindElements(By.CssSelector("ul li"));
        var match = listItems.FirstOrDefault(item =>
            item.Text.Contains(expectedTitle) && item.Text.Contains(expectedContent));

        if (match != null)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", match);
            return true;
        }

        return false;
    }

    public string GetValidationMessage()
    {
        return _driver.FindElement(By.CssSelector(".text-danger")).Text;
    }
}
