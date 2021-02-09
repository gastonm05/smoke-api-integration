Feature: SocialInfluencersSearch
	In order to reach out to social influencers
	As a standard CCC user
	I want to get social influencers with the given criteria

@Ignore("NotNeeded") @Ignore
Scenario: Verify Social Influencer search return results
	Given I go to CCC
	When I login as 'CisionCloudQAUser'
	And I navigate to the Social Influencers search page
	And I perform a search by subject : 'Sports'
	Then I should see relevant results returned
	And transparency text should have 'Sports' in the content
