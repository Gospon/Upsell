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
  ![image](https://github.com/Gospon/Upsell/assets/103904530/52c7b2d2-0c76-49db-94e9-2cc1aba64813)

  - Infrastructure and Persistence are separated:
   ![image](https://github.com/Gospon/Upsell/assets/103904530/3ad95714-dc2d-49df-a888-182a66bd438b)


### Modular monolith (Currently)
  - Each module is implemented as a Class Library, representing a single bounded context
  - The modules encapsulate specific business functionality and are isolated from each other.
  - Collaboration between bounded contexts is facilitated through a Shared Kernel class library.
  - RabbitMQ messaging system is utilized for communication between bounded contexts.
  - All services and dependencies are registered in the Program.cs file.
  - All data is stored in one database but each module has its own persistence
    
 ![image](https://github.com/Gospon/Upsell/assets/103904530/45cc896a-4668-4974-831a-56bf4ceb44c3)


### Communication between Bounded Contexts (RabbitMQ)
-  docker run -d  --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management

![image](https://github.com/Gospon/Upsell/assets/103904530/fe874b81-5d7f-4b27-900f-109b29796989)

### CQRS & Mediator
  ![image](https://github.com/Gospon/Upsell/assets/103904530/75775506-80b3-4df7-b176-7b8c8447f774)

  - Controllers are located in API layer
  - Repositories not implemented yet

### Authentication / Authorization 
  - JwtToken (Currenlty)
    
  ![image](https://github.com/Gospon/Upsell/assets/103904530/56497715-8eda-4cbe-af06-a118b8b4284e)

### Redux
![image](https://github.com/Gospon/Upsell/assets/103904530/6b84f8be-719e-4748-938d-621ae2397e58)

