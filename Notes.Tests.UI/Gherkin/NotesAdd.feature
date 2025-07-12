Feature: Adding notes via UI

  Scenario Outline: Successfully adding multiple notes
    Given browser is on notes page
    When user enters note data "<title>" and "<content>"
    And user submits the form
    Then the note should appear in the list with title "<title>" and content "<content>"
  Examples:
      | title     | content         | expectedResult           |
      | UI Note A | First content   | Note appears in the list |
      | UI Note B | Second content  | Note appears in the list |

  Scenario Outline: Failing to add a note with empty title
    Given browser is on notes page
    When user enters note data "<title>" and "<content>"
    And user submits the form
    Then validation message should be displayed saying "Title is required"
  Examples:
      | title        | content      | expectedResult              |
      | empty string | Some content | The title field is required |