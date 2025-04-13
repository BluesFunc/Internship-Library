# Library-Api by BluesFunc

For startup application use command in solution root: 
docker compose up -d --build

Application hosted on localhost:8080

You can watch all routes and manipulate them at /swagger (OpenAPI) 

run tests via "dotnet test"

# Fixes log

## Excpetion handling
### Application layer
Instead of throwing expction in application layer error there is used Result pattern with error code. Each code represented error, that identified at controller and form a result.
### Domain layer
There i found only one domain error. If reservation time is lower than now, it's caused thorwing custom domain exception. Exception handled by middlware. Each exception might have unique handle logic. 
## Buissnes loigc checks
Each use case handle different errors, that occurs. 
## Each File now have only one class(Except Result and Result'<T>' ^-^)
## UnitOfWork replcaed by MediatrPipline
Now each command, that realise ITransactionRequest after handling use case save changes in database.


