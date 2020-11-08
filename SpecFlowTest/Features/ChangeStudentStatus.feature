Feature: Change Student status
	As student I want people to know that I do not want to contact any roommate
	Because I have one

@mytag
Scenario: Student want to change her/his status
	Given the student login to the platform with id 1
	And the student enter to Profile section
	When the student select the Finish Searching option
	Then the student will se her/his status changed