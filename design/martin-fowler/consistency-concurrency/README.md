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
