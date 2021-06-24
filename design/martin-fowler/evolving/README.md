### The Event-Driven Approach

When we build software, our main focus is, quite rightly, aimed at solving some
real-world problem. It might be a new web page, a report of sales features, an
analytics program searching for fraudulent behavior, or an almost infinite set of
options that provide clear and physical benefits to our users. These are all very
tangible goals—goals that serve our business today.
But when we build software we also consider the future—not by staring into a
crystal ball in some vain attempt to predict what our company will need next
year, but rather by facing up to the fact that whatever does happen, our software
will need to change. We do this without really thinking about it. We carefully
modularize our code so it is comprehensible and reusable. We write tests, run
continuous integration, and maybe even do continuous deployment. These
things take effort, yet they bear little resemblance to anything a user might ask
for directly. We do these things because they make our code last, and that doesn’t
mean sitting on some filesystem the far side of git push. It means providing for
a codebase that is changed, evolved, refactored, and repurposed. Aging in soft‐
ware isn’t a function of time; it is a function of how we choose to change it.
But when we design systems, we are less likely to think about how they wil

 how do we design systems that age well
at company scales and keep our businesses nimble?

There is a very good reason for this too.
When we design systems at company scales, those systems become far more
about people than they are about software.


organization, software, and data 

To complicate matters further, what really differentiates the good systems from the bad is their ability to manage these three
factors as they evolve, independently, over time.

Microservices are run
by different teams, have different deployment cycles, don’t share code, and don’t
share databases.

Measuring changes when they need to be applied all at once in various services
https://www.infoq.com/news/2017/04/tornhill-prioritise-tech-debt/

Encapsulation encourages us to hide data, but data systems have little to do with
encapsulation. In fact, quite the opposite: databases do everything they can to
expose the data they hold (Figure 8-2). They come with wonderfully powerful,
declarative interfaces that can contort the data they hold into pretty much any
shape you might desire. That’s exactly what a data scientist needs for an explora‐
tory investigation, but it’s not so great for managing the spiral of interservice
dependencies in a burgeoning service estate.