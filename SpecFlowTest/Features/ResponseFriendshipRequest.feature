Feature: Response Friendship request
	As a student I want to see all my Friendship requests
	To answer some 

Background: 
	Given the student log in to the platform
	
Scenario: The student accept friendship request
	Given the student select the Friendship Request section 
	When the student accept the first friendship request
	Then the student will be her/his request accepted