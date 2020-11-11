 Feature: Look for a Roommate
	As an student I want to see a list of a Students that want a roommate

Background: 
	Given the student log in the platform
	| FirstName | LastName  | Dni      | Phone     | Gender | Address | Birthdate  | Description | Hobbies | Password | Mail           | Smooker | StudyCenterId | CampusId |
	| Jose      | Fernandez | 74361398 | 452785126 | Male   | VES     | 2001-03-17 | Kind person | Read    | 123456   | jose@gmail.com | false   | 1             | 1        |
    
Scenario: Student want to see a list of possible roommates
	Given the student enter to the Roommate section
	Then the student will see the list of roommates

Scenario: Student want to see the posible roommate's information
	Given the student enter to the Roommate section
	When the student click on Ver más option of the roommate with id 1
	Then will appears the roommate's information in the Perfil section
	| Id | FirstName | LastName | Dni      | Phone     | Gender | Address | Password | Mail           | Birthdate  | Description  | Hobbies | Smooker |
	| 1  | Juan      | Perez    | 74567898 | 456789123 | Male   | SJM     | 123456   | juan@gmail.com | 2001-05-17 | Happy person | Soccer  | false   |
	

Scenario: Student want to meet her/his possible Roommate
	Given the student enter to the Roommate section
	When the student click on Ver más option of the roommate with id 1
	When the student click on Send Invitation button
	Then the student see a message "Wait for your possible roommate answer"