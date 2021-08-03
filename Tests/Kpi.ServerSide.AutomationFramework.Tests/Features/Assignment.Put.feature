@Put
@Regression
Feature: Update Assignment by Id
	Story:

@Smoke
Scenario: 1. Validate Update Assignment by Id with valid data
	Given I have logged user
	When I create Assignment by post request
	And I send the Assignment update request with new description
	Then I see updated Assignment

Scenario: 1. Validate Update Assignment by Id with invalid data
	Given I have logged user
	When I send the Assignment update request with invalid Assignment id
	Then I see BadRequest status code 