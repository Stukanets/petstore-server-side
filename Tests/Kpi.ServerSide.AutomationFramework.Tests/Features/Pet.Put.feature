@Put
@Regression
Feature: Update Pet by Id
	Story:

@Smoke
Scenario: 1. Validate Update Pet by Id with valid data
	Given I have free API with swagger
	When I create Pet by post request
	And I send the Pet update request with same Id and new body
	Then I see updated Pet