
![QnA](qna_logo.png)

# QuestionAnswerApp
A simple app for asking questions and posting answers, built using React and ASP.NET Core

# Requirements

- .NET Core SDK 2.2
- MySQL
- MySQL Connector
- Docker (optional)

# How to Setup ?

1. `cd` into the `api` project
    >`cd server\Server\Api`

2. restore nuget packages
    >`dotnet restore`

3. build the project
    >`dotnet build`

4. run the project
    >`dotnet run`

# Running with Docker

The code contains the `docker-compose.yml` file which contains the necessary configurations for the app to run inside docker.

Simply run the following command:

> `docker-compose up`

The above command will run the services inside docker containers at following URLs.

    - WebApp: http://localhost:3000
    - API: http://localhost:5000




