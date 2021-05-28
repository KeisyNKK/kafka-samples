# So let's get started with Aspnet Core

We first clone the confluentinc/cp-zookeeper from docker hub

`docker clone confluentinc/cp-zookeeper`

Then we create a network like, the one we're using for both zookeeper and kafka

`docker network create kafka`

Using the created network, we're going to create a new zookeeper container using docker commands from getting started guide (<a href="https://docs.docker.com/get-started/">read more</a>).

This time we're going to create the zookeeper container into kafka network assigning some variables

`docker run -d --network=kafka --name=zookeeper -e ZOOKEEPER_CLIENT_PORT=2181 -e ZOOKEEPER_TICKE_TIME=2000 -p 2181:2181 confluentinc/cp-zookeeper`

So, if we:

`docker logs zookeeper`

So now we're going to run on 9092 and tell KAFKA_ZOOKEEPER_CONNECT env variable that zookeeper is running on port 2181, with:
-e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 confluentic/cp-kafka

now we're running kafka container on port 9092

`docker run -d --network=kafka --name=kafka_aspnet -p 9092:9092 -e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 confluentinc/cp-kafka`


Once connected on the containers, we can stablish the connection using the aspnet framework as the librarys here will show. 

Reade README for the Kafka project which was created with: 

`dotnet create new webapi Kafka`

