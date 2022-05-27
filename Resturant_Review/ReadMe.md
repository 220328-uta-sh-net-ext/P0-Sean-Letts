# Resturant Reviewer

## Project Description

Resturant Reviwer is an API where users can create user accounts. Anyone can see all of the resturants and reviews. Admins can see all users. Any user can put a new resturant to be reviewed or write a review.

## Technologies Used

* C#
* Xunit or NUnit
* SQLServer DB
* ADO.Net
* Asp.Net Core Web Api
* Azure App Service
* Serilog 

## Features

List of features ready and TODOs for future development
* Add a new user
* Ability to search user as admin
* Display details of a restaurant for user
* Add reviews to a restaurant as a user
* View details of restaurants as a user
* View reviews of restaurants as a user
* Search restaurant (by name or zip code)

Todo:
* Calculate reviews’ average rating for each restaurant

## Getting Started

git clone https://github.com/220328-uta-sh-net-ext/Sean-Letts.git
Open your Visual Stuido
Select build at the top
Observe the program running

or

Go to https://resturantapitest.azurewebsites.net/swagger/index.html
From here you can test the diffent functionalities.
If you want to post something to the database outside of logging in, you will need to use Postman to do so.
Get the authentication token after performing a successful login
Put the token into the bearer token section in Postman
Post a new resturant or review with the proper information attached. 

