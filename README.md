How to open

Visual Studio
tools -> command line => develop powershell

change folder to cd ex. cd C:\Users\Michal\source\repos\ToDoApplication\ToDoApplication.Api

type
docker-compose up --build

Database will be empty of records

database image: docker pull michaltulej/todoapplicationdb:latest
api image:      docker pull michaltulej/todoapplicationapi:latest
Database script: ToDoApplication\ToDoApplication.Infrastructure\Database\database.sql








