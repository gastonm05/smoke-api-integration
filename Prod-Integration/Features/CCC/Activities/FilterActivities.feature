Feature: FilterActivities
	In order to find activities
	As a CCC user
	I want to be able to filter activities


Scenario: Filter Activities by Type
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I navigate to the activities page
	And I filter by Type 'Twitter'
	Then all activities displayed should be of Type 'Twitter'

Scenario: Filter Activities by Status
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I navigate to the activities page
	And I filter by Status 'Sent'
	Then all activities displayed should be of State 'Sent'