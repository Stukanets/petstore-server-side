@Get
@Regression
Feature: Get Pet by Id
	Story:

@Smoke
Scenario: 1. Validate Get Pet by id with valid data
	Given I have free API with swagger
	When I create Pet by post request
	And I receive get Pet by id response
	Then I see returned Pet details which are equal with created