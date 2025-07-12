Feature: Adding a note

  Scenario Outline: Successfully adding multiple notes
    Given note data is prepared with title "<title>" and content "<content>"
    When the note is posted to the API
    Then the response should be successful
    And the response should contain the created note with title "<title>" and content "<content>"
  Examples:
      | title     | content         | response |
      | Note A    | Alpha content   | Success  |
      | Note B    | Beta content    | Success  |
      
  Scenario Outline: Failing to add a note with invalid title
    Given note data is prepared with title "<title>" and content "<content"
    When the note is posted to the API
    Then the response should be a Bad Request
  Examples:
      | title        | content            | response    |
      | empty string | Valid content      | Bad Request |
      | null         | Also valid content | Bad Request |