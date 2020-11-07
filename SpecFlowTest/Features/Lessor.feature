Feature:  Lessor
	Create, Read and Update a lessor

@lessorTest
Scenario: Search a lessor by id
	Given I know the lessor's id
    When I supplied 1 as lessor id
	Then lessor details should be
	| id | firstName | lastName  | dni      | phone     | gender | address      | birthdate  | password | mail             | premium |
	| 1   | Renato    | Arredondo | 78510254 | 984568145 | male   | lasorquideas | 2001-05-17 | 132465   | arr_av@gmail.com | true    |


#Scenario: read all lessor 
#   Given I want to get the list of lessors
#	When I select all lessor
#   Then All lessor list should return
#	|id | name	| quantity	|
#	|1	|milk	| 2			|
#	|2	|banana	| 6			|
#	|3	|apple	| 4			|

#Scenario: Add new Lessor 
#        Given I want to add a new lessor
#        When  lessor attributes provided
#	|Id | Name	| Quantity	|
#	|1	|Mango	|4			|
#        Then lessor will be able to enter to the platform as lessor