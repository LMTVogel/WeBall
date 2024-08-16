
# Ball.com

Ball.com is a monorepo that includes multiple microservices that recreate the domain of a big webshop. Each microservice uses .NET 8 and maintains its own database, allowing them to operate independently. The system uses RabbitMQ for messaging and ensures eventual consistency over time, so the microservices work well together without being tightly coupled.

## Microservices
- Supplier Management
- Inventory Management
- Customer Account Management
- Customer Support Management
- Payment Management
- Order Management
- Logistics Management
- Notification Service

## Technologies
- **[.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)**
- **[RabbitMQ](https://www.rabbitmq.com/)**
- **[Docker](https://www.docker.com/)**
- **[MySQL](https://www.mysql.com/)**
- **[MongoDB](https://www.mongodb.com/)**

---

## Archimate Model
To visualize the application and all functional requirements, this Archimate model was created.

![Archimate Model](https://github.com/user-attachments/assets/28de524d-b5b1-4d3c-a831-bc4f0c5fe7a6)

## CQRS
An example of CQRS (Command Query Responsibility Segregation) using an order event.

<img src="https://github.com/user-attachments/assets/a7bbfba6-a9a9-417d-a418-9f4c6862c243" alt="CQRS" width=50%>
