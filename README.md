# Overview
This is a web api for printing message to the server console at the given time.

# Technologies & Tools
+ .Net Core 3.1
+ Redis
+ Docker
+ Swagger
+ Automapper

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
