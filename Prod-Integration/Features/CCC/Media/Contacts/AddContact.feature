Feature: AddContact
	In order to keep contact information
	As a CCC user
	I want to be able to add contacts

Scenario: Add private contact
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I select 'Add New' from the 'Influencers' navigation menu
	And I Create a Private Contact with the twitter handle 'CisionTester'
	And I navigate to contact search page
	And I search for the contact
	Then the contact profile should display with a twitter stream