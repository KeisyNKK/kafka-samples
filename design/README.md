[13:09, 6/22/2021] Keisy: “Organizing into services taught teams not to trust
each other in most of the same ways they’re not supposed to trust external devel‐
opers.”
[13:09, 6/22/2021] Keisy: A shared source of truth turns out to be a surprisingly useful thing. Microservi‐
ces, for example, don’t share their databases with one another (referred to as the
IntegrationDatabase antipattern). There is a good reason for this: databases have
very rich APIs that are wonderfully useful on their own, but when widely shared
they make it hard to work out if and how one application is going to affect oth‐
ers, be it data couplings, contention, or load. But the business facts that services
do choose to share are the most important facts of all. They are the truth that the
rest of the business is built on. Pat Helland called out this distinction back in
2006, denoting it “data on the outside.”
[13:26, 6/22/2021] Keisy: So there are some clear advantages to the event-driven approach (and there are
of course advantages for the REST/RPC models too). But this is, in fact, only half
the story. Streaming isn’t simply an alternative to RPCs that happens to work
better for highly connected use cases; it’s a far more fundamental change in
mindset that involves rethinking your business as an evolving stream of data, and
your services as functions that transform these streams of data into something
new
[13:27, 6/22/2021] Keisy: For nearly half a century databases have played a central
role in system design, shaping—more than any other tool—the way we write
(and think about) programs. This has been, in some ways, unfortunate.
[13:56, 6/22/2021] Keisy: So when it comes to data, we should be unequivocal about the shared facts of our
system. They are the very essence of our business, after all. Facts may be evolved
over time, applied in different ways, or even recast to different contexts, but they
should always tie back to a single thread of irrevocable truth, one from which all
others are derived—a central nervous system that underlies and drives every
modern digital business.
[14:57, 6/22/2021] Keisy: Is Kafka What You Think It Is?
[14:57, 6/22/2021] Keisy: There is an old parable about an elephant and a group of blind men. None of the
men had come across an elephant before. One blind man approaches the leg and
declares, “It’s like a tree.” Another man approaches the tail and declares, “It’s like
a rope.” A third approaches the trunk and declares, “It’s like a snake.” So each
blind man senses the elephant from his particular point of view, and comes to a
subtly different conclusion as to what an elephant is. Of course the elephant is
like all these things, but it is really just an elephant!
[15:25, 6/22/2021] Keisy: What Is Kafka Really? A Streaming Platform
As Figure 3-1 illustrates, Kafka is a streaming platform. At its core sits a cluster of
Kafka brokers (discussed in detail in Chapter 4). You can interact with the cluster
through a wide range of client APIs in Go, Scala, Python, REST, and more.
[15:27, 6/22/2021] Keisy: Originally built to distribute the datasets created by large social networks, Kafka
was predominantly shaped by a need to operate at scale, in the face of failure.
Accordingly, its architecture inherits more from storage systems like HDFS,
HBase, or Cassandra than it does from traditional messaging systems that imple‐
ment JMS (Java Message Service) or AMQP (Advanced Message Queuing Proto‐
col).
[15:48, 6/22/2021] Keisy: Scalability opens other opportunities too. Single clusters can grow to company
scales, without the risk of workloads overpowering the infrastructure. For exam‐
ple, New Relic relies on a single cluster of around 100 nodes, spanning three
datacenters, and processing 30 GB/s. In other, less data-intensive domains, 5- to
10-node clusters commonly support whole-company workloads. But it should be
noted that not all companies take the “one big cluster” route. Netflix, for exam‐
ple, advises using several smaller clusters to reduce the operational overheads of
running very large installations, but their largest installation is still around the
200-node mark.
[15:49, 6/22/2021] Keisy: s, Kafka includes a throughput control feature, called quotas
[15:49, 6/22/2021] Keisy: https://docs.confluent.io/platform/current/kafka/post-deployment.html#quotas
[15:50, 6/22/2021] Keisy: To help with this, Kafka includes a throughput control feature, called quotas, that
allows a defined amount of bandwidth to be allocated to specific services, ensur‐
ing that they operate within strictly enforced service-level agreements, or SLAs
(see Figure 4-2). Greedy services are aggressively throttled, so a single cluster can
be shared by any number of services without the fear of unexpected network
contention. This feature can be applied to either individual service instances or
load-balanced groups.
[15:52, 6/22/2021] Keisy: (Kafka provides ordering guarantees only within a
partition.) This is managed for you: you supply the same key for all messages that
require a relative order. So a stream of customer information updates would use
the CustomerId as their partitioning key. All messages for the same customer
would then be routed to the same partition, and hence be strongly ordered (see
Figure 4-3).
[15:57, 6/22/2021] Keisy: To
maintain global ordering, use a single partition topic. Throughput will be limited
to that of a single machine, but this is typically sufficient for use cases of this
type.
[15:58, 6/22/2021] Keisy: https://docs.confluent.io/platform/current/clients/producer.html#configuration
[16:07, 6/22/2021] Keisy: So services inherit both high availability and load balancing, meaning they can
scale out, handle unplanned outages, or perform rolling restarts without service
downtime. In fact, Kafka releases are always backward-compatible with the pre‐
vious version, so you are guaranteed to be able to release a new version without
taking your system offline.

Security
Kafka provides a number of enterprise-grade security features for both authenti‐
cation and authorization. Client authentication is provided through either Ker‐
beros or Transport Layer Security (TLS) client certificates, ensuring that the
Kafka cluster knows who is making each request. There is also a Unix-like per‐
missions system, which can be used to control which users can access which data.
Network communication can be encrypted, allowing messages to be securely sent
across untrusted networks. Finally, administrators

https://queue.acm.org/detail.cfm?id=3096459

# Events: A Basis for Collaboration

In this chapter we’re going to focus on the other side of the architecture coin:
composing services not through chains of commands and queries, but rather
through streams of events. This is an implementation pattern in its own right,
and has been used in industry for many years, but it also forms a baseline for the
more advanced patterns we’ll be discussing in Part III and Part V, where we
blend the ideas of event-driven processing with those seen in streaming plat‐
forms.

# Commands, Events, and Queries

## Commands

Commands are actions—requests for some operation to be performed by
another service, something that will change the state of the system. Com‐
mands execute synchronously and typically indicate completion, although
they may also include a result.1
• Example: processPayment(), returning whether the payment succeeded

A command has an explicit expectation that something
(a state change or side effect) will happen in the future. Events come with no such future expectation.
They are simply a statement that something happened.

https://www.enterpriseintegrationpatterns.com/patterns/messaging/ProcessManager.html

## Events

Events are both a fact and a notification. They represent something that hap‐
pened in the real world but include no expectation of any future action. They
travel in only one direction and expect no response (sometimes called “fire
and forget”), but one may be “synthesized” from a subsequent event.
• Example: OrderCreated{Widget}, CustomerDetailsUpdated{Customer}
• When to use: When loose coupling is important (e.g., in multiteam sys‐
tems), where the event stream is useful to more than one service, or
where data must be replicated from one application to another. Events
also lend themselves to concurrent execution.

## Queries

Queries are a request to look something up. Unlike events or commands,
queries are free of side effects; they leave the state of the system unchanged.
• Example: getOrder(ID=42) returns Order(42,…).
• When to use: For lightweight data retrieval across service boundaries, or
heavyweight data retrieval within service boundaries.

The beauty of events is they wear two hats: a notification hat that triggers services
into action, but also a replication hat that copies data from one service to
another. But from a services perspective, events lead to less coupling than com‐
mands and queries. Loose coupling is a desirable property where interactions
cross deployment boundaries, as services with fewer dependencies are easier to
change.

https://ieeexplore.ieee.org/document/5388187

Functional couplings are optional. Core data couplings are essential.

receiver-driven routing