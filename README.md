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


### Nice to have:
* :x: Use Identity Server as a centralized access control manager
* :x: Use OpenID Connect to manage authorization
* :x: Connect to the graph database Neo4j
* :x: Create a basic UI to query the routes withing a defined and destination points


### Pre-requisites:
Before running this exercise please make sure you have installed on your computer:
* Microsoft Visual Studio 2017
* Powershell
* Docker


## Instructions:
1. Clone or download to your computer this project
2. Start a new powershell terminal window and run the following command to download the most recent docker image of Microsoft SQL Server 2017:
    ```
    docker pull mcr.microsoft.com/mssql/server:2017-latest
    ```
    After download finishes, let's start a new container, named DeliveryServiceExerciseDB, with SQL Server 2017

    ```
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=+YourStrong!Passw0rd+" -p 14330:1433 --name DeliveryServiceExerciseDB -d mcr.microsoft.com/mssql/server:2017-latest
    ```
    * Please notice that your local post **14330** will be mapped to the 1433 port of the container

3. Run the following command to download the most recent docker image of REDIS:
    ```
    docker pull redis
    ```
    After download finishes, let's start a new container, named DeliveryServiceExerciseREDIS, with the REDIS In-Memory NoSQL database

    ```
    docker run --name DeliveryServiceExerciseREDIS -d redis
    ```
    * Please notice that your local post **14330** will be mapped to the 1433 port of the container

4. Start a new instance of Microsoft Visual Studio and open the solution file 'DeliveryServiceExercise.sln'
5. Ensure the DeliveryServiceExercise.API project is selected and press Execute



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


### URLS's:
* WebApi: https://localhost:44308/
* Identity Server Endpoint: https://localhost:44328/
* Demo (ASP.Net Core Web Application): https://localhost:44352/


### Identity Server:
* Discovery Endpoint: https://localhost:44328/.well-known/openid-configuration


