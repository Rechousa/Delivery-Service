# Delivery-Service

### RoadMap (subject to change in the future):
* :heavy_check_mark: Create the basic models that will support the application
* :heavy_check_mark: Create the repositories
* :heavy_check_mark: Implement the WebApi
* :heavy_check_mark: Create Unit Tests to test the WebApi
* :heavy_check_mark: Add Entity Framework Core
* :heavy_check_mark: Populate with some example data
* :heavy_check_mark: Use a docker container for SQL Server
* :heavy_check_mark: Add Logging and global exception handling
* :heavy_check_mark: Implement a security mechanism based on JWT
* :heavy_check_mark: Use Redis as a cache server
* :heavy_check_mark: Install Swagger OpenAPI documentation generator
* :heavy_check_mark: Connect to the graph database Neo4j and query the database
* :heavy_check_mark: Create a basic UI to query the routes withing a origin and destination points


### What would be nice to have:
* :black_circle: Replace user management with Identity Server as a centralized access control manager
* :black_circle: Improve authorization with OpenID Connect
* :black_circle: Improve Neo4j database synchronization


# Pre-requisites:
Before running this exercise please make sure you have installed on your computer:
* Microsoft Visual Studio 2017
* Powershell
* Docker

Three docker images / containers are required to run this demonstration. They are SQL Server 2017, Redis and Neo4j.


# Instructions to run this demo:
1. Clone or download to your computer this project
2. Start a new powershell terminal window and run the following command to download the most recent docker image of Microsoft SQL Server 2017:
    ```
    docker pull mcr.microsoft.com/mssql/server:2017-latest
    ```
    After download finishes, let's start a new container, named DeliveryServiceExerciseDB, with SQL Server 2017

    ```
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=+YourStrong!Passw0rd+" -p 14330:1433 --name DeliveryServiceExerciseDB -d mcr.microsoft.com/mssql/server:2017-latest
    ```
    * Please notice that your local port **14330** will be mapped to the 1433 port of the container

3. Run the following command to download the most recent docker image of REDIS:
    ```
    docker pull redis
    ```
    After download finishes, let's start a new container, named DeliveryServiceExerciseREDIS, with the REDIS In-Memory NoSQL database

    ```
    docker run --name DeliveryServiceExerciseREDIS -d redis
    ```
    * Please notice that your local port **6379** will be mapped to the 6379 port of the container

4. Run the following command to download and run the most recent docker image of Neo4j:
    ```
    docker run --publish=7474:7474 --publish=7687:7687 neo4j:3.0
    ```
    * Please notice that your local port **7474** (for HTTP) will be mapped to the 7474 port of the container. Also notice that your local the port **7687** (for Bolt) will be mapped to the 7687 port of the container.

5. Open a web browser and navigate to http://localhost:7474/browser/. This will open the Neo4j manager and it requires immediately to change the password. Go ahed and change the password to 'teste' (without single quotes). Since the solution is already configured with this password, the interaction with Neo4j should work with no problems.
6. Start a new instance of Microsoft Visual Studio and open the solution file 'DeliveryServiceExercise.sln'
7. Ensure the DeliveryServiceExercise.API project is selected and press Execute. Solution is configured wiht multi startup projects, so both the WebAPI project and the Web UI should start running.
8. If prompted from your browser to accept or install any certificate, please do it. That's fine, it's just a security confirmation.
9. In case of the application is not working or you find these instructions incomplete, please feel free to contact me. I'll be very happy to help.


# Other informations:

###  Local tools used in this exercise:
* Microsoft Visual Studio 2017
* Microsoft Visual Studio Code
* Microsoft Management Studio
* Powershell
* Docker
* Postman


### Online tools/resources used in this exercise:
* Generate guids: https://www.guidgenerator.com/
* Mastering MarkDown: https://guides.github.com/features/mastering-markdown/
* Complete list of github markdown emoji markup: https://gist.github.com/rxaviers/7360908
* Cities of Porto District: https://codigopostal.ciberforma.pt/cidades/distrito-do-porto/
* Cost and travel duration: https://www.viamichelin.pt/
* Javascript Web Token Decoder: https://jwt.io/
* Microsoft REST API Guidelines: https://github.com/Microsoft/api-guidelines/blob/master/Guidelines.md


### Design Patterns used in this exercise:
* Factory Method Pattern
* Repository Pattern
* Model-View-Controller Pattern


### Technical decisions:
* Why use ViewModels for user data submission? Because database models can be extended in the future and we don't want the API callers to submit malicious data.


### URLS's:
#### HTTPS:
* WebApi: https://localhost:44308/
* Demo (ASP.Net MVC Web Application): https://localhost:44333/
* WebApi Documentation (Swagger): https://localhost:44308/swagger

#### HTTP:
* WebApi: http://localhost:58820/
* Demo (ASP.Net MVC Web Application): http://localhost:53947/

### Configuration:

Some settings are configurable and you easily find them on the appsettings.json of the WebAPI project.

It includes the connectionstring to SQL Server, Redis and Neo4j, Javascript Web Token settings (issuer, secret, audience, number of days to expire), for example.


# Final comments:

### What have I learned with this exercise?
1. Docker
   
   Although I already knew what is Docker and never tried it before. So, I gave it a change and it worked like a charm. This solution uses three different services running separately on docker containers and the magic is that I didn't needed to change anything in my host. 

2. Graph database Neo4j

    This was completely new to me. When I've read this exercise it was very clear that a graph database would be perfect to solve this problem. And it did. I was able to populate data on the database, connect nodes by creating relashionships between them, set custom properties to nodes and relationships.

Thank you.

It was very fun to complete this exercise.
