@Post
@Regression
Feature: Pet Creation
	Story:

@Smoke
Scenario: 1. Validate Pet creation with provided model
	Given I have free API with swagger
	When I send the pet creation request with provided model
	Then I see created pet in the get response
