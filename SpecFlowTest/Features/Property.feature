Feature:  Property
	Create, Read a Property

@propertyTest
Scenario: Search a property
	Given I want to see the description of a property
    When I supplied 1 as property id
	Then property details should be
	| id | address      | description | 
	| 1  | lasorquideas | casa grande | 


Scenario: Add new Property
        Given I want to add a new property
        When  property attributes provided
		| address      | description | lessor_id |
		| lasorquideas | casa grande | 1         |
        Then property was created