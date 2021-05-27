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

Tópicos: o tubo de entrada das mensagens
Partição:
Partições distribuídas ( apenas orenadas pela key - carteira)
Producers que podem ter não garantia, garantia parcial ou garantia total
A depender de não saber se gravou, saber que gravou mas poder cair, saber que gravou e replicou
Ack 1 Ack 0 Ack -1
At most once, at least once, excactly once
1 - Super performance, mas posso perder uma ou outra
2 - pode gerar duplicidade
3 - apenas uma vez
Imdepotência de produtores
ON não duplica, não eficiente
OFF duplica se rolar
Consumers
Producers cria, kafta gerencia, consumer lê
while true que sempre lê as mensagens
1 - Um consumer pode ler de várias partições de um mesmo tópico
2 - Há o conceito de grupo de consumidores
3 - Num tópico com n partições é ideal deixar o kafka distribuir a leitura sem sobrecarga de nenhuma por meio da criação de um número de consumidores o mais próximo de n possível. Mas apenas um consumer pode estar ouvindo uma partição, de modo que qualquer valor excedente a n produza consumers ociosos
4 - É possível consumers fazerem leitura de tópicos distintos.
Consumers rebalance:
Toda vez que muda o número de cosnumers o rebalanceamente da distribuição entre provedores e leitores é automático
Segurança: é possível que a mensage producer - kafka e broker - consumer seja, em ambos os casos, criptografadas
É possível haver tanto autenticação quanto autorização, por meio de credenciais
kafka connect: pegar informações de um lugar e passar para outro lugar
Kafka rest proxy : caso não queiramos usar driver padrão e queiramos, na real conectar via http.
today
Compatibilidade de dados:
Confluent Schema registry - pelo problema de conversibilidade de dados e contrato
Você com ele especifica o Schema/Model... O padrão no qual a mensagem estará
IDL ( Interface Description Language )
Apache AVRO
Protobuffer
Com eles é possível adequar as mensagens ao padrão de interesse
Kafka Streams
O Streams fazem parte do ecossistema do kafka, mas no fundo é apenas um biblioteca feita em java cujas finalidades são: Real-time data processing and transformation library
É possível, a partir dele fazer transformação em tempo real dos dados e conseguir adaptá-los, gerenciá-lo e, dentro disso, garantir a usabilidade e persistência deles, uma vez que serão gerenciados pelo kafka
Conflutent ksqDB


