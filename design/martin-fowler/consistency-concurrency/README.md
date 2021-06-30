# Consitency and Concurrency


Consistence for https://www.julianbrowne.com/article/brewers-cap-theorem CAP theorem and for ACID transactions


Not always we want consistency to be ACID, but reather we may preffer them to be eventual.


## Eventual Consitency

We can easly think of consistency being applied to the same copy of data when every script runs sequentially.

Event driven system aren’t jus normally designed that way. Instead, they leverage asynchronous broadcast, deliberately removing the need for global state and avoiding synchronous execution.

 There are two consequences of eventual consistency:

- Timeliness:
    - If two services process the same event stream, they will process them at different rates, so one might lag behind the other. If a business operation consults both services for any reason, this could lead to an inconsistency
- Collisions:
    - If different services make changes to the same entity in the same event stream, if that data is later coalesced—say in a database—some changes might be lost.

One way to solve concurrency in distributed Systems is  **CRDT**: https://en.wikipedia.org/wiki/Conflict-free_replicated_data_type


## Single Writer Principle

A useful way to generify these ideas is to isolate consistency concerns into owning services using the single writer principle. Martin Thompson used this term in The Single Writer Principle response to the wide-scale use of locks in concurrent environments, and the subsequent efficiencies that we can often gain by consolidating writes to a single thread. The core idea closely relates to the Actor model.

So, instead of sharing a global consistency model (e.g., via a database), we use the single writer principle to create local points of consistency that are connected via the event stream. There are a couple of variants on this pattern, which we will discuss in the next two sections.


## Command Topic

A common variant on this pattern uses two topics per entity, often named Command and Entity.

- Command Topic 
    - can be written to by any process and is used only for the initiating event
- Event Topic
    - can be written to only by the owning service: the single writer.

## Atomicity with Transactions

Kafka provides a transactions feature with two guarantees:

• Messages sent to different topics, within a transaction, will either all be written or none at all.

• Messages sent to a single topic, in a transaction, will never be subject to duplicates, even on failure.

But transactions provide a very interesting and powerful feature to Kafka Streams: they join writes to state stores and writes to output topics together, atomically. 

## Identity and Concurrency Control

The notion of identity is hugely important in business systems, yet it is often overlooked. For example, to detect duplicates, messages need to be uniquely identified, as we discussed in Chapter 12. Identity is also important for handling the potential for updates to be made at the same time, by implementing optimistic concurrency control.

The version identifier can then be used to handle concurrency. Say a user named Bob opens his customer details in one browser window (reading version 1), then opens the same page in a second browser window (also version 1). He then changes his address and submits in the second window, so the server increments the version to 2. If Bob goes back to the first window and changes his phone number, the update should be rejected due to a version comparison check on the server.

The optimistic concurrency control technique can be implemented in synchronous or asynchronous systems equivalently.

## Summary

In this chapter we looked at why global consistency can be problematic and why eventual consistency can be useful. We adapted eventual consistency with the single writer principle, keeping its lack of timeliness but avoiding collisions. Finally, we looked at implementing identity and concurrency control in event-driven systems.
