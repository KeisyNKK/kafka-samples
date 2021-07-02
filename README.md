# Kafka infra

Kafka was originally developed at LinkedIn in 2011 and has improved a lot since then. Nowadays, it’s a whole platform, allowing you to redundantly store absurd amounts of data, have a message bus with huge throughput (millions/sec), and use real-time stream processing on the data that goes through it all at once.

This is all well and great, but stripped down to its core, Kafka is a distributed, horizontally scalable, fault-tolerant commit log.

A Kafka server, a Kafka broker and a Kafka node all refer to the same concept and are synonyms (see the scaladoc of KafkaServer).

---

A Kafka broker receives messages from producers and stores them on disk keyed by unique offset.

A Kafka broker allows consumers to fetch messages by topic, partition and offset.

Kafka brokers can create a Kafka cluster by sharing information between each other directly or indirectly using Zookeeper.

A Kafka cluster has exactly one broker that acts as the Controller.

You can start a single Kafka broker using kafka-server-start.sh script.

Distributed systems are designed in such a way to accommodate failures in a configurable way. In a 5-node Kafka cluster, you can have it continue working even if two of the nodes are down. It’s worth noting that fault tolerance is at a direct trade-off with performance, as in the more fault-tolerant your system is, the less performant it is.

<a href="https://betterprogramming.pub/thorough-introduction-to-apache-kafka-6fbf2989bbc1">More here </a>


Kafka combines three key capabilities so you can implement your use cases for event streaming end-to-end with a single battle-tested solution:

* To publish (write) and subscribe to (read) streams of events, including continuous import/export of your data from other systems.
* To store streams of events durably and reliably for as long as you want.
* To process streams of events as they occur or retrospectively.

## Event, produces, topics, consumers

Every time something happen it's possible to trigger an event, so you can use Kafka to manage it. 

### Event

The occurrency that is going to be handled

### Producer

Producers are those client applications that publish (write) events to Kafka. They can ask for guarantee: Ack 1 Ack 0 Ack -1.x

### Consumers

Consumers are those that subscribe to (read and process) these events.

# Topics

Events are organized and durably stored in topics. Events in a topic can be read as often as needed—unlike traditional messaging systems, events are not deleted after consumption.

# Partitions

The key point to out modelation is that : Events with the same event key (e.g., a customer or vehicle ID) are written to the same partition, and Kafka guarantees that any consumer of a given topic-partition will always read that partition's events in exactly the same order as they were written.

## Replication

To make your data fault-tolerant and highly-available, every topic can be replicated, even across geo-regions or datacenters, so that there are always multiple brokers that have a copy of the data just in case things go wrong, you want to do maintenance on the brokers, and so on. A common production setting is a replication factor of 3, i.e., there will always be three copies of your data. This replication is performed at the level of topic-partitions.


# Start up

Topics: the message entry tube 
Partition: Distributed partitions (only keyed by the key - portfolio)
Producers that may have no guarantee, partial guarantee or full guarantee Depending on not knowing if you recorded, knowing that you recorded but being able to fall, knowing that you recorded and replied 
Ack 1 Ack 0 Ack -1 
At most once, 1 - Super performance, but I can lose one or the other
at least once, 2 - can generate duplication
excactly once  3 - only once Imdepotency of producers ON does not duplicate, not efficient OFF doubles if scrolls Consumers

Producers creates, kafta manages, consumer reads while true that always reads messages 1 - A consumer can read from multiple partitions on the same topic 2 - 

There is the concept of consumer group 3 - On a topic with n partitions is ideal let kafka distribute the reading without overloading any by creating as many consumers as possible. But only one consumer can be listening to a partition, so that any value in excess of n produces idle consumers 

4 - It is possible for consumers to read different topics. Consumers rebalance: Whenever the number of cosnumers changes, the rebalancing of the distribution between providers and readers is automatic 

Security: it is possible that the message producer - kafka and broker - consumer is, in both cases, encrypted It is possible to have both authentication and authorization , using kafka connect credentials: get information from one place and move to another place Kafka rest proxy: in case we don't want to use a standard driver and we want to, in real connect via http. today Data compatibility: Confluent Schema registry - due to the problem of data and contract convertibility 

You specify the Schema / Model with it ... 

The standard in which the message will be IDL (Interface Description Language) 
Apache AVRO 
Protobuffer 

With them it is possible to adapt the messages to the pattern of interest 

Kafka Streams Streams are part of the kafka ecosystem, but in the end it is just a library made in java whose purposes are: 
  Real-time data processing and transformation library It is possible, from it to make real-time transformation of data and be able to adapt them, manage them and, within that, guarantee their usability and persistence, since they will be managed by kafka Conflutent ksqDB

## What you'll see in the folders

- code: has all necessary implementation code to put kafka up and running into four aws EC2
- zookeeper-explained: Explain all you need to know about zookeeper for this repo
- kafka-explained: Explain all you need to know about kafka for this repo