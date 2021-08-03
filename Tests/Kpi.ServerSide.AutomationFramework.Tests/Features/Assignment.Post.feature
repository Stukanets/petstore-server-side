@Post
@Regression
Feature: Assignment Creation
	Story:

@Smoke
Scenario: 1. Validate Assignment creation with provided model
	Given I have logged user
	And I have current Assignment count
	When I send the assignment creation request with provided model
	Then I see increased Assignment count