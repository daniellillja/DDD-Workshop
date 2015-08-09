Feature: List of submitted applications
	In order keep track of the applications I have previously submitted
	As a user calling the api
	I want to be given the list of applications in JSON

@mytag
Scenario: List a single application
	Given I have previously submitted a valid application with the data
	| FirstName | LastName |
	| Daniel    | Lillja   |
	When I call the api '/api/applications/daniel-lillja'
	Then the result should show the previously submitted application
