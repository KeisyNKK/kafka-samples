# Monolitic world, and the streaming world

All we do in software development is to model data flow, but all we have is some data stored.

With Kafka we try to combine those two and use some integrated way to think about data withou sharing schemas and databases acceses in sort of a improductive way.

# Streaming Platform 

This is what Kafka is and where we're starting this new trend.

## How we're storing data?

Kafka is way closer to a distributed data base than it is to a messaging system. Specially because it's abilitty to be fault tolerant.

### Logs - the fundamental data-structure for kafka

With kafka the fundamental way to store data is by using logs. As a matter of fact we need to know three basic data structures as Engeniers: b-trees, hash maps and logs!

Logs as basically massages written from the beggining till the end of the file and imutable as it's core.



## Pub / Sub pattern

All the messages in the Kafka env have three things: the key, value and headers.  But the way it's consumed is important because no message disaper after read.

Excactly as physical book reader uses book mark to know where he stopped in the last time reading, kafka stores where some given consumer has stopped reading when it's back to the messages stores.

