Feature: SaveSearch
	In order to receive coverage
	As a Standard CCC user
	I want to be able to save a search

Scenario: Save a search
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I search for news with a start date of 'Today minus 7 days'
	And I click the Save Search Button
	And I save the search
	Then I should see the search on My Coverage Page