# Shopping Cart API - Akshay Koregaonkar

The following document contains the outline of the solution, design decisions and answers to questions asked in Readme.md

### Assumptions made:
- We must always have a shopping cart containing items passed to the API
- We must always have a product repository passed to the API
- Discount calculations are independent of shipping costs unless its a shipping based discount

### Design
- The easiest way to follow the code would be by following the ShoppingCartEngine.cs class. This class can be initialised by a client using the builder pattern via ShoppingCartEngineBuilder. 
- The builder must be provided with a shopping cart and a product repository as a minimum as without these, any calculations are unnecessary.
- The builder allows custom factories to be initialised by the client and the client may therefore choose to implement their own factories for the following interfaces:
	- IBasketCalculatorFactory
	- IShippingCalculatorFactory
	- IDiscountCalculatorFactory
- The default implementations for these will be used if a custom one is not provided
- These default factory implementations help the API decide which Calculator to use for which task. There are three main categories for this:
	- BasketCalculator
	- ShippingCalculator
	- DiscountCalculator
- A majority of the calculators implement the abstract class ShoppingCart.Core.Calculators.ShoppingCalculatorBase.cs
- The exception to this are the null object pattern calculators and the shipping related calculators (including the shipping based discount calculator for free shipping).

###### BasketCalculator
This is the the original Calculator.cs class refactored. It was renamed to "Basket" for clarity as we will use to sum up products in a basket. The basket calculator verifies whether the product repository is provided.

###### ShippingCalculator
The shipping calculator in the original code was tightly coupled to the main Calculator class. Refactoring was performed to loosely couple this away from the main basket calculator. It originally implemented 
the IShippingCalculator interface which required its inherited classes to pass a "shippingTotal" to the CalculateShipping method. Instead, the total is now passed to the shipping calculator via constructor injection 
via the shipping calculator factory. This allows us to use the shipping calculator in isolation and also means that if there is a different implementation required for shipping, it can be easily implemented.

The shipping calculator should never have a null product total passed to it but just in case a new IShippingCalculatorFactory implementation passes a null object, an ArgumentNullException() will be throw to warn 
incorrect usage of the iterface via the Null object pattern class "UnknownShippingCalculator.cs"

###### DiscountCalculators
In order to facilitate discounts to the Shopping Cart API, the following changes to the model were made: 
- New Supplier enum was added for HP, Dell, Apple and All (default)
- New ProductCategory enum was added for Electronic, Accessory, Audio or None (default)
- In an ideal world these two entities would come via the database but enums were used for simplicity
- Another enum added was for DiscountType which can either be ProductBased, SupplierBased or ShippingBased
- The Product entity was updated to include an IEnumerable of associated categories and supplier

The supplier and product based calculators are similar types of discounts in that they both apply discounts based on the items in the cart. These therefore implement abstract CalculatorBase class which provides them 
with the cart items. The shipping discount (i.e. free shipping) does not follow this rule and therefore does not implement the base class but instead implements ICalculator directly.

One additional aspect to mention is the UnknownDiscountCalculator class. Like the Shipping calculator, this too uses the Null object pattern but is likely to be called much more often. To counter this, a singleton object 
is created here and its calculate method will always return a value of 0m. When a discount is invalid, a value of 0m seems most relevant and this means the rest of the shopping cart calculations are not affected. 
The unknown shipping calculator does not pass 0 as we always need to calculate the shipping costs.

### Additonal notes including changes made to original submission 
- The passing unit tests asked in the Readme.md file have been added under ShoppingCart.Tests.Core.Calculators.AssignmentTests
- Original tests to check calculations kept but modified slightly to fit new design. See ShoppingCart.Tests.Core.Calculators.PreDiscountCalculatorTests.cs
- Removal of WholesalePrice.cs as this is not required by the Shopping cart API.
- The type decimal is used for currency as double has relative precision
- IRepository remains unchanged, however an abstract Repository is implemented to make use of dictionary for faster lookup
- The abstract Repository implements the Get and Add methods and only expects its child repositories to implement a GetKey method to specify what Id needs to be used to store as key in the dictionary integer key value from 
the Coupon or Product object. Future consideration maybe to use something other than int for this ID - possibly GUID or UUID.

### Bonus Question:
A refund would ideally be handled by a different API as we don't persist data within the Shopping Cart API and its only calculating the shopping cart total. To cater for refunds, we will need to know shopping history 
per user. This would ideally be persisted after a successful purchase has been completed. We will then need to validate whether the user is eligible for a refund for a specific item - which could be a refund payment or 
voucher. If it's a voucher, the shopping cart API can be extended to cater for this by adding a new DiscountType and DiscountTypeCalculator and modifying the DiscountCalculatorFactory. The calculator can be implemented in 
two ways:
- one where we deduct a specific product's value from the total amount. We would prefer taking the value for this from the original payment as product values are not always consitent with time 
- another where we deduct a specific value from the total amount. In this case, we may need to persist the refund voucher details if this voucher is re-usable and the refund amount is more than the shopping total
