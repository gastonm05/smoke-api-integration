Feature: AnalyzeCoverageItems
	In order to analyze coverage items
	As a CCC standard user
	I want to view analytics for coverage items

Scenario: View Analytics for coverage items
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I select all coverage items
	And I click the Analyze button
	Then I should see the Analytics page