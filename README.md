# aspnetcore-parcel-delivery
See the overall picture of  **implementations on microservices with .net tools**  on  **parcel delivery microservices**  project;

![110304529-c5b70180-800c-11eb-832b-a2751b5bda76](https://user-images.githubusercontent.com/13931992/192162932-6992dba4-f7aa-4073-a9c0-e94d0f6378ae.png)


There is a couple of microservices which implemented  parcel delivery  modules over  **Order** , **PriceList**  and  **Delivery** microservices with  NoSQL (MongoDB, Redis)  and  Relational database PostgreSQL  with communicating over  RabbitMQ Event Driven Communication  and using  Ocelot API Gateway.

**Whats Including In This Repository**

## We have implemented below features over the [aspnetcore-parcel-delivery](https://github.com/rybayramov/aspnetcore-parcel-delivery/tree/development)

**microservices repository**.

**Order microservice which includes;**

- ASP.NET Web API application
- REST API principles, CRUD operations
- **Redis database**  connection and containerization
- Consume PriceList  **Grpc Service**  for inter-service sync communication to calculate parcel price
- Publish OrderCheckout Queue with using  **MassTransit and RabbitMQ**

**PriceList microservice which includes;**

- ASP.NET  **Grpc Server**  application
- Build a Highly Performant  **inter-service gRPC Communication**  with Order Microservice
- Exposing Grpc Services with creating  **Protobuf messages**
- **MongoDB database**  connection and containerization

**Microservices Communication**

- Sync inter-service  **gRPC Communication**
- Async Microservices Communication with  **RabbitMQ Message-Broker Service**
- Using  **RabbitMQ Publish/Subscribe Topic**  Exchange Model
- Using  **MassTransit**  for abstraction over RabbitMQ Message-Broker system
- Publishing OrderCheckout event queue from Order microservices and Subscribing this event from Delivery microservices
- Create  **RabbitMQ EventBus.Messages library**  and add references Microservices

**Delivery Microservice**

- Implementing  **DDD, CQRS, and Clean Architecture**  with using Best Practices
- Developing  **CQRS with using MediatR, FluentValidation and AutoMapper packages**
- Consuming  **RabbitMQ**  OrderCheckout event queue with using  **MassTransit-RabbitMQ**  Configuration
- **Postgre database**  connection and containerization

**API Gateway Ocelot JWT Authentication Microservice**

- Implement  **API Gateways with Ocelot**
- Sample microservices/containers to reroute through the API Gateways
- Run multiple different  **API Gateway/BFF**  container types
- Role based Authorization in Ocelot API Gateway with tranformation Claims to request headers 
- Authentication Web Api with user login method which returns rol based JWT access token 

**Microservices Cross-Cutting Implementations**

- Implementing **Centralized Distributed Logging with Elastic Stack (ELK); Elasticsearch, Logstash, Kibana and SeriLog** for Microservices
- Use the  **HealthChecks**  feature in back-end ASP.NET microservices
- Using  **Watchdog**  in separate service that can watch health and load across services, and report health about the microservices by querying with the HealthChecks

**Ancillary Containers**

- Use  **Portainer**  for Container lightweight management UI which allows you to easily manage your different Docker environments
- **pgAdmin PostgreSQL Tools**  feature rich Open Source administration and development platform for PostgreSQL

**Docker Compose establishment with all microservices on docker;**

- Containerization of microservices
- Containerization of databases
- Override Environment variables

**Run The Project**

You will need the following tools:

- Visual Studio 2022
- [.Net Core 6](https://dotnet.microsoft.com/download/dotnet-core/5)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

**Installing**

Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

1. Clone the repository
2. At the root directory which include  **docker-compose.yml**  files, run below command:

docker-compose -f docker-compose.yml -f docker-compose.override.yml up –d

1. Wait for docker compose all microservices. That's it! (some microservices need extra time to work so please wait if not worked in first shut)
2. You can  **launch microservices**  as below urls:

- **Order API** -\>  [**http://host.docker.internal:8001/swagger/index.html**](http://host.docker.internal:8001/swagger/index.html)
- **PriceList API** -\>  [**http://host.docker.internal:8002/swagger/index.html**](http://host.docker.internal:8002/swagger/index.html)
- **Delivery API** -\>  [**http://host.docker.internal:8003/swagger/index.html**](http://host.docker.internal:8003/swagger/index.html)
- **API Gateway** -\> [**http://host.docker.internal:8010**](http://host.docker.internal:8010/)
- **Rabbit Management Dashboard** -\>  [**http://host.docker.internal:15672**](http://host.docker.internal:15672/) -- guest/guest
- **Portainer** -\> [**http://host.docker.internal:9000**](http://host.docker.internal:9000/) -- admin/admin1234
- **pgAdmin PostgreSQL** -\>  [**http://host.docker.internal:5050**](http://host.docker.internal:5050/) -- [admin@aspnetrun.com](mailto:admin@aspnetrun.com)/admin1234
- **Elasticsearch** -\>  [**http://host.docker.internal:9200**](http://host.docker.internal:9200/)
- **Kibana** -\>  [**http://host.docker.internal:5601**](http://host.docker.internal:5601/)
- **Web Status** -\>  [**http://host.docker.internal:8007**](http://host.docker.internal:8007/)

1. Launch [http://host.docker.internal:8007](http://host.docker.internal:8007/) in your browser to view the Web Status. Make sure that every microservices are healthy.
