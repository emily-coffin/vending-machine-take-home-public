# Vending Machine App

The purpose of this app is to create a vending machine. The vending machine will accept money, give change, maintain inventory and dispense products.

This app is written in C# with XUnit tests.

## Project Features

This project contains methods within the Machine class to perform the following actions:

1. Select a Product
1. Pay for a Product
1. Accepts Coins
1. Returns Change (over payment of coins)

Currently, are three options you can pick for products:

1. Cola -  $1.00
2. Chips - $0.50
3. Candy - $0.65

This machine only accepts nickels, dimes, and quarters. Pennies will be returned to the customer.

When money is added to the machine it will append the coins to the total. Once the coins have all been entered and item has been selected the machine will return the item and provide any change back to the customer.

Included is a console app that works as an interactive orchestrator to tie together the Vending Machine's methods. The console app will display the products available, take coins, return change and the product.

Note: The isolation of the core machine methods from the console will help with the ability to swap out the interactive orchestrator depending on future business needs.

## Development

### Prerequisites

Make sure you have [Docker](https://docs.docker.com/get-docker/) installed.

To start Docker environment run the following command:

```sh
docker build -t vendingmachine . && docker run --name vendingmachine --rm -it -v $PWD:/project -w /project vendingmachine bash
```

### Run Tests

❗ Make sure you have started your Docker container first. ❗

To run test suite use the build and run-tests script within the Scripts directory within your Docker container.

```sh
./scripts/build.sh && ./scripts/run_tests.sh
```

### Run Console App

❗ Make sure you have started your Docker container first. ❗

To run test suite use the build and run script within the Scripts directory within your Docker container.

```sh
./scripts/build.sh && ./scripts/run.sh
```
