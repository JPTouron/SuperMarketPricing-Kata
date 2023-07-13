# source:
http://codekata.com/kata/kata01-supermarket-pricing/

## Goal
The goal of this kata is to practice a looser style of experimental modelling. 
Look for as many different ways of handling the issues as possible. 
Consider the various tradeoffs of each. 
What techniques are best for exploring these models? 
For recording them? 
How can you validate a model is reasonable?


## Some Guidelines:
Some things in supermarkets have simple prices: this can of beans costs $0.65. Other things have more complex prices. For example:

three for a dollar (so what�s the price if I buy 4, or 5?)
$1.99/pound (so what does 4 ounces cost?)
buy two, get one free (so does the third item have a price?)
This kata involves no coding. The exercise is to experiment with various models for representing money and prices that are flexible enough to deal with these (and other) pricing schemes, and at the same time are generally usable (at the checkout, for stock management, order entry, and so on). Spend time considering issues such as:

does fractional money exist?
when (if ever) does rounding take place?
how do you keep an audit trail of pricing decisions (and do you need to)?
are costs and prices the same class of thing?
if a shelf of 100 cans is priced using �buy two, get one free�, how do you value the stock?
This is an ideal shower-time kata, but be careful. Some of the problems are more subtle than they first appear. I suggest that it might take a couple of weeks worth of showers to exhaust the main alternatives.

# Objectives & guidelines on this run

## important basic rules (first-run)

- every single item has to have a price
- a bill/ticket has to have at least one item
- all discounts have a start and end date
- an item can NEVER have a price lower or equal to zero (0.0, -1.0, etc)


## some playful business rules

- in order to sell an item, it has to be in stock (qty >=1)

- discounts can come in the following forms:
 - % off an item's price
 - fixed amount off an item's price
     - discounts types:
         - buy one, get one (BOGO, this could be an x qty of items for the price of y qty of items, ie: buy 1 get 1 free, buy 2 get 1 free, etc)
         - volume discount: the more you buy the more you get discounted, this is a % off based on the total amount, up to a maximum amount of items bought
             the diff with the above one is that the above one is rather a fixed % over a fixed qty of items
         - seasonal discounts (ie: christmas, black friday, etc)
         - loyalty discounts (ie: on your X purchase you get Y% off on Z item)

 - some discounts may be combined with others depending on the discount type (ie: loyalty + seasonal, but not volume + seasonal, etc)


 - discount applicability (creation/update) has to be audited, i should be able to trace when an item was discounted and by how much
 - price change (creation/update) has to be audited, i should be able to trace when an item has changed price and by how much
 - start and end dates must have 00:00 and 23:59 times respectively (ie: start: 03/03/2023 00:00, end: 03/08/2023 23:59)
 - a bill/ticket must have a specific format (TBD the format details)
     - should be able to be stored in some sort of storage (ie: database)
 - taxes
     - a tax is a percentage of the item's price that has been included in the price
     - different taxes can be applied to an item
         - there will be a base price which represents the raw price of the item
         - the tax applied to an item will be listed in the bill/ticket
             - if in a bill there are items wich have different taxes, then the bill will group the total price for the items with the same tax and indicate what's the tax percentage applied and base price for the group summarized

## Figma:
- https://www.figma.com/file/OjKWVBkQ3aUIdVMXIuYUus/Event-Storming-(Community)?type=design&node-id=105-4542&mode=design&t=15UHd8UOWYAJNQxK-0

## Objectives status

 - [ ] Have a representation for money and currency
 - [ ] Implement mantainable discountable items
 - [ ] Implement Stock management
 - [ ] Have your groceries calculated by this thing
 - [ ] Implement making a purchase at a register
 - [ ] Implement List totalling considering -> three for a dollar (so what's the price if I buy 4, or 5?)
 - [ ] Implement discounts over a period of time
 - [ ] Implement data loading off some external repo


 ## Another guys' solutions:
 https://github.com/raddanesh/Kata01
 https://github.com/andymeek/kata01-supermarket-pricing

 ## NOTE: Check this repo's branches for more approaches
