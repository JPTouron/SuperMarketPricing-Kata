## Source:

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

three for a dollar (so what's the price if I buy 4, or 5?)

$1.99/pound (so what does 4 ounces cost?)

buy two, get one free (so does the third item have a price?)

This kata involves no coding. The exercise is to experiment with various models for representing money and prices that are flexible enough to deal with these (and other) pricing schemes, and at the same time are generally usable (at the checkout, for stock management, order entry, and so on). Spend time considering issues such as:

does fractional money exist?

when (if ever) does rounding take place?

how do you keep an audit trail of pricing decisions (and do you need to)?

are costs and prices the same class of thing?

if a shelf of 100 cans is priced using 'buy two, get one free', how do you value the stock?

This is an ideal shower-time kata, but be careful. Some of the problems are more subtle than they first appear. I suggest that it might take a couple of weeks worth of showers to exhaust the main alternatives.

## Objectives status

 - [x] Have a representation for money and currency
 - [x] Implement mantainable discountable items
 - [x] Implement Stock management
 - [x] Have your groceries calculated by this thing
 - [x] Implement making a purchase at a register
 - [x] Implement List totalling considering -> three for a dollar (so what's the price if I buy 4, or 5?)
 - [ ] Implement discounts over a period of time
 - [ ] Implement data loading off some external repo


 ## Another guys' solutions:
 https://github.com/raddanesh/Kata01
 https://github.com/andymeek/kata01-supermarket-pricing

 ## NOTE: Check this repo's branches for more approaches