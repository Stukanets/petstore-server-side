@Delete
@Regression
Feature: Delete Pet by Id
	Story:

@Smoke
Scenario: 1. Validate Delete Pet by Id with valid data
	Given I have free API with swagger
	When I create pet by post request
	And I send delete request with created pet id
	Then I see NotFound on get request

Scenario: 2. Validate Delete Pet by Id with invalid data
	Given I have free API with swagger
	When I send delete request with invalid pet id
	Then I see NotFound status code
