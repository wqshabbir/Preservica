# Readme

## Table of Contents

1. [Project Structure](#project-structure)
2. [Desgin & Architecture](#design-and-architecture)

## Project Structure

---

This repo contains BFF in Backend folder and Frontend React app in frontend folder

Application stack is responsible of the management of customer details. It will allow users to add/amed or delete customers as required.
<br>


## Design and Architecture

---

### High Level Design

- **Backend/BFF** <br>
This is .net core app which has set of APIs to allows CRUD operations on customer. On a request to the app, validation is performed on the params provided. Once validated, request gets pass onto the service layer which is so far calling operations on the database layer as there is no logic currently being performed. Database layer is constructed using Dapper ORM with base repository that performs operation on the generic entity provided. In this case this is customer but can be used for other entities in the database.
Also there is global exception handler has been registered in order to catch any unexpected exceptions. In the presence  of exact business requirements, we could have our own business exception classes created to transform errors to more readable/user freindly UI response.

- **Backend/BFF Unit Tests** <br>
Also unit test project is added to cover the basic calls to the dependencies. For example controller tests covering possible return types with all usecases with hits to service layer when mocked. Similary, service layer calls to database layer when mocked to make sure we are hitting right method with correct expecations. 

- **Frontend** <br>
Frontend single page app setup using create-react-app with typescript template which has two main components rendered on the UI. First one is a container with form wrapped with four fields that allows you to add new customer details. And second component is a list item of all customers already exists and added to the system. Each customer can be amended or deleted from the listed customers component.

- **Configuration** <br>
Please setup ConnectionStrings:DefaultConnectionString key value to the database in the appsettings.json of the .net core project

    Please set the serverApiAddress in the App.tsx file to the relevant base address of the BFF.
<br>

