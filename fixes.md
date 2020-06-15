### Improvements: Refactoring to the supplier and product based discounts
- Added a new constructor parameter in CartItem to take in productRepository and added a check in the setter to see if ID exists
  (This means no longer require null checks in the discount calculators to see if the cart item exists in the productRepository) 
- Added a new test to cover scenarios where an item that doesn't exist in the productRepository is added to cart
- Added a new boolean HasAssociatedCategories expression in Product to return whether a Product contains associated categories
- Refactored and removed duplication in Product and Supplier based discount calculators by 
	- pulling up GetDiscount in DiscountCalculatorDecorator
	- converting the foreach statement to a LINQ statement 
	- creating a new boolean abstract method in DiscountCalculatorDecorator which requires child classes to implement the discount condition
 