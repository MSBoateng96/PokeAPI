# PokeAPI
My own fun ‘Pokedex’ in the form of a REST API that returns Pokemon information.

## Usage

Run project using IDE of choice e.g. Visual Studio Code

2 Options:
1) When project has started and localhost is in running in browser, append /swagger to URL to access APIs via Swagger
2) Call API using Postman and localhost URL

## API Endpoints
Note: Localhost Address may be different for your own machine

Basic Pokemon Information
Endpoint: http://localhost:5270/pokemon/{name}

Example: http://localhost:5270/pokemon/mewtwo

Translated Pokemon Information
Endpoint: http://localhost:5270/pokemon/{name}

Example: http://localhost:5270/pokemon/translated/mewtwo

## List of Improvements for Production API or if I had more time

• Create a decoupled, generic method to call any fun translation API (which includes a generic class to match all translation responses)

• Create a new exception class e.g. PokedexServiceException, to return a more bespoke error type to the user

• Create a memory cache to store pokemon data for a period of time after it has been retrieved from the PokeAPI

• Deploy Web App to Azure using a Docker Container and host API via Azure API Management Service

• Add extra layers of security to the API via Azure APIM e.g. IP restrictions, rate of service inbound policies & OAuth2.0 Token

## Link to Project Management Kanban Board
https://sarpps.notion.site/6aee8cffa3ae42edaf8709492ad11982?v=1a31430ed28649d88860269d3cb679e2