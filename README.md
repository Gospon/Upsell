# Upsell (Current Status)
The objective of the project is to create an eCommerce application that initially adopts a modular monolith architecture and subsequently transitions to a microservices architecture for deployment.
## Technologies
- ASP NET Core
- Angular
- SQL Server
- RabbitMq
- Docker
## Practices
### Clean architecture
  ![image](https://github.com/Gospon/Upsell/assets/103904530/b33f6f35-6753-42f9-a5eb-cd205e656d6e)
  - Infrastructure and Persistence are separated:
   ![image](https://github.com/Gospon/Upsell/assets/103904530/495f4712-ea98-42c8-bc86-500ca3bce202)

### Modular monolith (Currently)
  - Each module is implemented as a Class Library, representing a single bounded context
  - The modules encapsulate specific business functionality and are isolated from each other.
  - Collaboration between bounded contexts is facilitated through a Shared Kernel class library.
  - RabbitMQ messaging system is utilized for communication between bounded contexts.
  - All services and dependencies are registered in the Program.cs file.
  - All data is stored in one database but each module has its own persistence
    
  ![image](https://github.com/Gospon/Upsell/assets/103904530/1649b6c0-08a7-4e6c-8836-1d7b0de2cfea)

### Communication between Bounded Contexts (RabbitMQ)
-  docker run -d  --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management

![image](https://github.com/Gospon/Upsell/assets/103904530/657800e0-064c-410a-b1c8-50351ee418a8)

### CQRS & Mediator
  ![image](https://github.com/Gospon/Upsell/assets/103904530/b6a0204d-b476-4642-9a08-1b78faf67f60)
  - Controllers are located in API layer
  - Repositories not implemented yet

### Authentication / Authorization 
  - JwtToken (Currenlty)
  
  <img src="https://github.com/Gospon/Upsell/assets/103904530/35ecb1b7-0fe7-41df-a34f-c9e60cae8157" width="45%"/>
  &nbsp; &nbsp; &nbsp; &nbsp;
  <img src="https://github.com/Gospon/Upsell/assets/103904530/97883bfb-7f34-480f-9e3e-4333a1eb369c" width="48%"/>
![image](https://github.com/Gospon/Upsell/assets/103904530/be873067-33e7-40b8-9b6e-ad6b98b62587)
