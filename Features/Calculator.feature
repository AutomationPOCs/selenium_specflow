@Calculator
Feature: Calculator
	Scenario: Add two numbers
		Given the first number is 50
		And the second number is 70
		When the two numbers are added
		Then the result should be 120

	Scenario Outline: Add two numbers permutations
		Given the first number is <First number>
		And the second number is <Second number>
		When the two numbers are added
		Then the result should be <Expected result>
		Examples:
			| First number | Second number | Expected result |
			| 0            | 0             | 0               |
			| -1           | 10            | 9               |
			| 6            | 9             | 15              |