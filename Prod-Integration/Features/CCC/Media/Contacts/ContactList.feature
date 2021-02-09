Feature: ContactList
	In order to reach out to similar contacts
	As a standard CCC user
	I want to create lists

Scenario: Create new contact list
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I navigate to contact search results with the key 'name' and the value 'test'
	And I select the first contact
	And I create a new list 'QATestList'
	And I navigate to the list page
	Then I should see contacts on the list