Feature: ImportFromArchive
	In order to add news items to my coverage
	As a CCC user
	I want to be able to import from the archive

Scenario: Search all news - import from archive
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I search the archive for news with the keyword 'Mercedes' and a start date of 'Today minus 15 days' and an end date of 'Today'
	And I select the first news item
	And I click the Add To My Coverage button
	And I click Ok on the Add to My Coverage popup
	And I go to My Coverage Page
	Then I should see the news item