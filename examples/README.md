# First Steps

First of all we install Docker on our machines.

## Using terminal - kafka-cluster

Go to kafka-cluster directory and `docker-compose up -d` to host and run the six clusters that you can see on

`docker-compose ps` :

`kafka-cluster_kafka-1_1       /etc/confluent/docker/run   Up`           
`kafka-cluster_kafka-2_1       /etc/confluent/docker/run   Up  `         
`kafka-cluster_kafka-3_1       /etc/confluent/docker/run   Up  `         
`kafka-cluster_zookeeper-1_1   /etc/confluent/docker/run   Up  `         
`kafka-cluster_zookeeper-2_1   /etc/confluent/docker/run   Up  `         
`kafka-cluster_zookeeper-3_1   /etc/confluent/docker/run   Up`

so running `docker exec -it kafka-cluster_kafka-1_1 bash` for the frist cluster, we enter in the machine.

