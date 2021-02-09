Feature: CustomActivity
	In order to see my activities within CCC
	As a CCC user
	I want to create custom activities

Scenario: Create custom activity
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I navigate to the activities page
	And Create an activity		
	Then I should see the activity with a status of 'Sent'