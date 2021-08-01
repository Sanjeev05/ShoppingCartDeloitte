Feature: Shopping Cart Test Scenarios

Scenario: Basic validation
Given The user is on the store page
When 1 items are added to the cart
And I navigate to Cart
And Cart is validated
And The total value of items 1 and payment $39.11 is verified 
And The delete button appears for the added item
And The clear button is clicked
Then The cart is cleared

Scenario: Advanced validation
Given The user is on the store page
When 2 items are added to the cart
And I navigate to Cart
And Cart is validated with both Item.
And The qty is increased to 3 for first item
And The total value of items 4 and payment $168.34 is verified 
And Reduce and delete button for both the items are verified
And Item 1 is reduced by 1 unit
And The total value of items 3 and payment $129.23 is verified 
And Item 2 is deleted and validated
Then The checkout functionality is verified
And The cart is cleared