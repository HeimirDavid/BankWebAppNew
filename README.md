# Bank Admin Site

#### Hey! Welcome to my bank project! Here is the live link to it! https://skys-bank.azurewebsites.net/
#### Hosted on a very budget friendly plan on azure, so please give the server some time to wake up :)

This is mainly an admin and cashier project to handle a large database with everything from role based users to customers and their accounts. 
This project is built as a school purpose and this is my implementation of it.

### Style
Since this was built during an backend development course using .NET iv'e put more focus on the backend side of things. 
The layout is from StarAdmin and is a bootstrap template. After turning it in i might spend some more time on styling parts of it to make it feel more unique.


## Implemented Features
#### General
* System handles customers, their accounts with a balance and transactions between accounts.
* Statistics on the front page/dashboard. You can se the total amount of customers, accounts and the overall balance.
* Search for a customer and get their profile, either by Id, name, city or username.
* Sort the search result.
* Customer page, where you can se details about the customer, including their accounts.
* Account page, see current balance and previous transactions in descending order. Load 20 at a time and fetch more using JavaScript to call an API.
* Handle withdrawal and deposit into an account.


#### Identity
* Two users seeded from the start, one cashier and one admin.
* Seed user roles
* Role based authorisation - Cashier can handle customer actions such as transfers and edits, while admins have access to all of the system.
* CRUD USERS and add roles to them.

#### Countries
* Show statistics on the front page with group data based on countries. Number of customers, accounts and the balance.
* Open a country and show the top 10 customers with the highest balance. The view is **Response cached**

### API for customers 
* Create login for customers (new user and role created - Customer)
* Create JWT tokens for authorization and authentication
* Customer profile at /api/customer/profile
* Create account profile for admins and cashiers at /api/accounts/123 (Id)

### Console Application - Suspicious transsaction / money laundring
Console application checking every transaction between customers, sorting them by countries and catching all transactions over 15000 a day or 23000 over the course of 3 days.
Log these in text files to later be able to distribute out to the appropriate reciever.
Keep track of where you left of so you don't go through the same ones again. This is done by logging the date it was last run.

## Features - To be implemented
* Search through external service, Azure cognative search
* Add new (multiple) accounts to customers, and make the option to share an account (OWNER and DISPONANT).

Would also be fun to expand the API to have more features for the customers. Like transfering to others, withdraw or between his/hers own accounts.

## Technologies
#### This project was built using:
* .NET Razor Pages
* .NET MVC API
* .NET Console Application
* Enitity framework
* Git
* CSS
* Bootstrap - (Star Admin template)
* Javascript
* HTML (cshtml)

### Some extra thoughts
As mentioned previously, i would really like to spend som more time on styling this project and also make it nicer on a mobile phone. Right now the "free" responsiveness
from bootstrap is all there is.
  
Im very happy about the structure, where I feel like I have made some good improvments. Using a shared library for all the database stuff and models. I would like to
take it further. Maybe seperate up the **Interfaces** for the services to the shared library and then make seperate implementations of those services for the projects that needs it.
Getting more comfortable with **dependency injection** here has been good, and I will put more focus now on learning it further,
using services properly and getting confident in it.
All in all, great fun!



