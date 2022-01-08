# ValaisEat


## Introduction

Some restaurants in Valais would like to manage their food delivery in the region. They ask you to create this platform with the help of N-tier and a database.
This project aims to cover the subjects presented in following course:

- 623-1 : Implémentation du système d’information / Implementierung des Informationssystems


## Application layers requirements

This application consists of three layers:

- 1 database (SQL Server Server)

- 1 or more class library projects (depending on your needs) to access the data (DAL, BLL, …)

- 1 MVC application as a user interface


## Database

The database must be a SQL Server database and you must create your own tables and their relationships. The database must store data to manage:

- Dishes sold by restaurants
- Orders from customers
- Staff from VS Eat responsible for the delivery in cities
- Staff login
- Customers login

You can add test data for restaurants and dishes directly in the database. You can change/add table properties as you wish.


## Features

### Constraints

- Each order is identified by a number. This number in addition to firstname lastname will be used to cancel the order at least 3 hours before delivery.

### User stories

- (login) – A customer must create an account with his/her address before using the website

- (order) – A logged customer can choose dishes from a list given by each restaurant available on the website to form an order. He/she will add delivery time (every 15 min) for his/her order. At the end of the order the price that the customer has to pay to the courier will be displayed

- (delivery management) – The system will assign the delivery of an order to one courier who is available in the same city as the restaurant where the order is made. One courier cannot have more than 5 orders to deliver every 30 minutes.

- (delivery interface) – each courier can log in the system to see his/her upcoming deliveries. Once one delivery is made the delivery person will archive it by pressing a button on the delivery interface.

### Details

- MVC + N-tier must be used for this project
- Individual project


## Technical information

### Implementation / Development

- Programming language : C# with Visual Studio (version provided in the virtual machine)
- ASP.NET Core
- Database: Microsoft SQL Server Express (version provided in the virtual machine) during development and full SQL Server for deployment

### Application architecture
- N-tier architecture
- Presentation layer
- Data access layer (DAL), Business logic layer (BLL)
- Data layer (database)
