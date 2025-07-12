Feature: Viewing the notes

  Scenario Outline: Getting the list of notes after adding a note
    Given note data is prepared with title "<title>" and content "<content>"
    And the note is posted to the API
    When requesting the list of notes
    Then the response should include a note with title "<title>"
  Examples:
      | title         | content                    | response                |
      | Note for list | This should be in the list | Includes the added note |