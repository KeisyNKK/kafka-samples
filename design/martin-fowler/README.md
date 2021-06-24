# Summary - <a href="https://www.youtube.com/watch?v=STKCRSUsyP0">The Many Meanings of Event-Driven Architecture • Martin Fowler • GOTO 2017<\a>

## Event driven design wasn`t always made the same way

When it comes to high throughput systems it's safe to say that event driven play an important role.

But, it's now clear that what can be called event driven runs around three called patterns:

### Event notification

Wherever something that is meant to be decoupled happens it notifies a queue or whatever and then the services that depends on it can know what is supposed to be done.

So, # events are used as a notification system mecanism.

The same way you use things like mobx to throw an event because you don`t want some text field to know everything about your code, you're here, reversing dependencies.

Everytime we use this mecanism we may generate a record of this event that can be used decopouled.

### Event or Commands

The interesting part is that, when using command, this user service may know what's supposed to be done with the change, when, with events, it's just telling, notifying, that some event happend.

It's important to notice that we can use event driven to notify both events and commands. 

Events notify that something happend, so it's better to use them in simple past, while commands want something to happen so we may write them in imperative.

#### The trade off

When you decouple what's gonna happen of what supposed to be done with the change, you'll have to be aware that you're hidding what happens through the chains of event. 

There's no sort of code that tells you that, procedurally anymore. So the debbuggin becomes a problem.

### Event-carried State Transfer

Event Notification often envolves aditional traffic because you need to know the new state that was notfied.

If we reduce the scope of search by adding information we reduce the numbers of look ups. 

The natural question is:

"Can I add enought information for the look up to never happen?"

The answer is Event-Carried State Transfer.

By keeping record of all necessary that into the consumers for it's scope, we reduce the load, we speed up the process, we decoupled even more. but the down side is that we loose data consistency.

Replicate data comes with a cost. (Eventual Consistency)

It's a less common pattern.

### Event Sourcing

It's basically when you preserve every change that happend in your system and you, now have the abbility not to worry about your appllication state and preserve, because, everytime it's shutted down you can confidently recover ir from the log system.

An famous event-source system is the version control.

Martin Fowler said:

"Event source is providing to your users a system that works on their data the way version controll works on your code".

It's for: Audit, Debuggin, Historic State, Alternative State, Memory Image

#### Downside of Event Sourcing

It's going to be hard to tell that the logic was done wrong if you do not store what you asked.

### CQRS - Command Query Responsability Segregation


## Event Colaboration

hat is special
about Event Collaboration is that no single service owns the whole process;
instead, each service owns a small part—some subset of state transitions—and
these plug together through a chain of events. So each service does its work, then
raises an event denoting what it did. If it processed a payment, it would raise a
Payment Processed event. If it validated an order, it would raise Order Validated,
and so on. These events trigger the next step in the chain (which could trigger
that service again, or alternatively trigger another service).


## Processing Events with Stateful Functions

Kafka
provides the equivalent of a pipe in Unix shell, and stream processors provide the
chained functions.

There is a well-held mantra that statelessness is good, and for good reason. State‐
less services start instantly (no data load required) and can be scaled out linearly,
cookie-cutter-style.
Web servers are a good example: to increase their capacity for generating
dynamic content, we can scale a web tier horizontally, simply by adding new
servers. So why would we want anything else? The rub is that most applications
aren’t really stateless. A web server needs to know what pages to render, what
sessions are active, and more. It solves these problems by keeping the state in a
database. So the database is stateful and the web server is stateless. The state
problem has just been pushed down a layer. But as traffic to the website increa‐
ses, it usually leads programmers to cache state locally, and local caching leads to
cache invalidation strategies, and a spiral of coherence issues typically ensues.

### The Event-Driven Approach

Listen to orders

Send confirmation

Using event driven it's triggered, make the look up and then send in line.

Problmes: The constant need to look things up, one message at a time.

The Pure (Stateless) Streaming Approach

A streaming system comes at this problem from a slightly different angle. The
streams are buffered until both events arrive, and can be joined together
(Figure 6-2).

Event Sourcing ensures every state change in a system is recorded,
much like a version control system. As the saying goes, “Accountants
don’t use erasers.”

## Making Events the Source of Truth

