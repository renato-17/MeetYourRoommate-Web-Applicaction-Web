Feature:  Lessor
	Create, Read and Update a lessor

@lessorTest

Scenario: Add new Lessor 
    Given I want to add a new lessor
    When  lessor attributes provided
	| firstName | lastName  | dni      | phone     | gender | address      | birthdate  | password | mail             | premium |
	| Renato    | Arredondo | 78510254 | 984568145 | male   | lasorquideas | 2001-05-17 | 132465   | arr_av@gmail.com | true    |
    Then lessor will be able to enter to the platform as lessor

Scenario: Search a lessor by id
	Given I know the lessor's id
    When I supplied 2 as lessor id
	Then lessor details should be
	| id | firstName | lastName  | dni      | phone     | gender | address      | birthdate  | password | mail             | premium |
	| 2   | Renato    | Arredondo | 78510254 | 984568145 | male   | lasorquideas | 2001-05-17 | 132465   | arr_av@gmail.com | true    |


