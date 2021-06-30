


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

https://docs.confluent.io/platform/current/clients/producer.html#configuration


https://queue.acm.org/detail.cfm?id=3096459