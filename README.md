# Overview
This is a web api for printing message to the server console at the given time.

# Technologies & Tools
+ .Net Core 3.1
+ Redis
+ Docker
+ Swagger
+ Automapper

## API
[Swagger](https://swagger.io/) tool is used for api documentation. It provides UI where developers can see all available web methods, their description and even run queries against api.

For local environment, you can access it by the following link: http://localhost:8089/swagger/index.html

Example:
Print at: 2021-02-05T00:00:00
Message: "any text"

**Note: specify time in server time zone which is displayed when server is started**


# How to run on local environment
Prerequisites:
+ Docker

To run application locally, all you need is to run docker-compose file.

It will build images for the following parts:
- **Backend** (build api image and run it in docker container)
- **Database** (run redis instance using image from Docker Hub)

Steps:
+ Clone git repository to your local environemt
+ Go to **devops** folder and run the following command:
```
docker-compose -f docker-compose.yml up
```
