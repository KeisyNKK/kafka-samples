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

## Create first topic

Now, inside we can run 

`$ kafka-topics --create --bootstrap-server localhost:29092 --replication-factor 3 --partitions 3 --topic mytopic`

So `kafka-topics --create --bootstrap-serve` requires a machine that can come from the kafka-cluser/docker-compose.yaml line 53 so you can use it.

With the `--replication-factor <NUMBER OF REPLICAS>` you can say how many replicas of this topic partitions you'll have accross the machines and `--partitions <NUMBER>` define how many of those are. Then you can specify the topic name with `--topic <NAME>`

So if it outpus

`Created topic mytopic`

it means that your topic was created. 

You can always use `$ kafka-topics` to see how to use it, but to see all the topics created you can use `$ kafka-topics --list --bootstrap-server localhost:29092`.

You may see your `mytopic` outputed.


***
# Connecting to the topic

We can use th `kafka-console-producer` for this job with:

`kafka-console-producer --broker-list localhost:29092 --topic mytopic`

Then, as an output you're seeing an `>`, meaning that everything you time goes into your topic.

Now, you can use another terminal to listen to the messages sent:

first: 

`docker exec -it kafka-cluster_kafka-1_1 bash`

then: 

`kafka-console-consumer --broker-list localhost:29092 --topic mytopic`

This way, all that is sent from the producer is outputed on the running consumer.

The nice thing here is that it's being stored. Meening that, even if you restart your application you can add the flag `--from-beginning` then all the previously outputed dated will be written again.

It's nice to notice that it's not in the same order.

# Exploring groups

If I use groups with 

`kafka-console-consumer --broker-list localhost:29092 --topic mytopic --group a`

in three different terminals, I'll be able to see the normalization of the consumers usage which produces the paralelism and better efficient resource consumming and performance.












