﻿Feature: Access to the Platform as Student
	As a person I want to subscribe to the platform as Student

@mytag
Scenario: Succesfully create an acount as Student
	Given the person enter to the platform
	And enter her/his information
	| FirstName | LastName | Dni      | Phone     | Gender | Address | Birthdate  | Description  | Hobbies | Password | Mail           | Smooker |
	| Juan      | Perez    | 74567898 | 456789123 | Male   | SJM     | 2001-05-17 | Happy person | Soccer  | 123456   | juan@gmail.com | false   |
    And specify her/his study center
	| Name |
	| UPC  |
    And specify her/his campus 
    | Name | Address    |
    | Villa  | Chorrillos |
	When he/she click on register
	Then he/she will be able to enter to the platform as student
