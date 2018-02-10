# simple REST API for TODO-manager
Technology stack: ASP.NET Core 2.0, sqLite.

Authorization: Bearer-token. 

After creating a new user you will receive a valid jwt-token which could be used for authorization.

### Resourses and methods
**/api/user**
- POST - create a new user. 
    
    valid input data: ` {   "login" : "your_login", "password" : "your_password" } `
    
    response: **JWT-token for Bearer Auth**

**/api/todo** -- needs authorization
- POST - create new TODO-item.
    
    valid input data: ` { "name": "your_todo_name", "isComplete" : "your_todo_status"}` 
    
    response: All created item data, including ID
    
- GET - get all user TODO-items

**/api/todo/{id}** -- needs authorization
- GET - get item with provided ID
response: All created item data, including ID

- DELETE -- remove item with provided ID
- PUT -- change item data 
