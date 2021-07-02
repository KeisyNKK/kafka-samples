# Kafka Cluster Setup
#### Multi Servers deployment and configuration discussions
******

## Kafka Basics
- Brokers holds topics partitions
- Brokers recieve and serve data
- Brokers are the unit of parallelism of a Kafka cluster
- Brokers are the essence of "distributed" aspect of Kafka
    - The same way we set multiple zookeepers servers to form a **Quorum** we'll set multiple **kafka brokers** to form a **cluster**. Some [cluesters](https://engineering.linkedin.com/blog/2019/apache-kafka-trillion-messages) like Linkedin, and Netflix, have 100 brokers to serve 100K messages per second. So it's really highly scalable.

# Kafka Cluster Sizing

Now, you're maybe wondering 

> How big should a cluster be
> How many brokers should I have

When we talked about Zookeepers the answer was three or five, maximum, but for kafka... Let's talk about size:

![alt text](../../../assets/event-streaming/1-broker.png "text")

- In the case of only 1 Broker:
    - If the broker is restarted, the Kafka cluster is down
    - The maximum replication factor for topics is 1
    - All producer and consumer requests go to the same unique broker
        - Only one point of failure
    - You can only scale vertically ( by increasing the instance size and restarting)
- It's very high risk, and only useful for development purposes

![alt text](../../../assets/event-streaming/3-brokers.png "text")

- In the case of 3 brokers:
    - N-1 brokers can be down, if N is your default [topic replication factor](https://jaceklaskowski.gitbooks.io/apache-kafka/content/kafka-topic-replication.html)
    - Ex: If N=3, then two brokers can be down
    - Producer and consumer requests are spread out between your different machines
    - Data is spread out between brokers, which means less disk space is used per broker.
    - You can have a cluster (this is our target for our course) - that's how we'll set up our cluster

## What about the extreme big number of 100 brokers?

![alt text](../../../assets/event-streaming/100-brokers.png "text")

We probably have five **Zookeepers** but let's ilustrate with three. Then 100 brokers. So what happens is:

- In the case of 100
    - Your cluster is fully distributed and can handle tremendous volumes, even using commodity (very basic) hardware.
    - Zookeeper may be under pressure because of many open connections, so you need to increase the Zookeeper instance performance.
    - **Cluster management is a full time job** (make sure no broker acts weirdly) - all big companies have an entire team dedicated to those tasks: managing kafka clusters.
     > "If you wanna propose 100 brokers make sure you have work for the rest of your life, without sleep"
    - It is not recommended to have a replication factor of 4 or more, and this would incur network communications within the brokers. Leave it as 3.
    - Scale horizontally only when a bottleneck is reached (network, i/o, cpu, ram)



****
# Kafka Configuration

> Configuring Kafka in production is an ART.
>

Is important to take it into account, because, although you need a few to get started there are 140 parameters to care about, so you never get it right the first time and, no matter what, your configuration may evolve over time.

It requires really good understanding of:

- Operating Systems Architecture
- Servers Architecture
- Distributed Computing
- CPU operations
- Network performance
- Disk I/O
- RAM and Heap size
- Page cache
- Kafka and Zookeeper

All the 140 parameters are classified between mandatory, high, medium and low level of importance.

Configuring Kafka is an iterative process: behavior changes over time based on usage and monitoring, so should your configuration.

So let's dive into our sugested configuration for the file server.properties:

```bash
############################# Server Basics #############################

# The id of the broker. This must be set to a unique integer for each broker.
broker.id=1
# change your.host.name by your machine's IP or hostname
advertised.listeners=PLAINTEXT://kafka1:9092

# Switch to enable topic deletion or not, default value is false
delete.topic.enable=true

############################# Log Basics #############################

# A comma seperated list of directories under which to store log files
log.dirs=/data/kafka

# The default number of log partitions per topic. More partitions allow greater
# parallelism for consumption, but this will also result in more files across
# the brokers.
num.partitions=8
# we will have 3 brokers so the default replication factor should be 2 or 3
default.replication.factor=3
# number of ISR to have in order to minimize data loss
min.insync.replicas=2

############################# Log Retention Policy #############################

# The minimum age of a log file to be eligible for deletion due to age
# this will delete data after a week
log.retention.hours=168

# The maximum size of a log segment file. When this size is reached a new log segment will be created.
log.segment.bytes=1073741824

# The interval at which log segments are checked to see if they can be deleted according
# to the retention policies
log.retention.check.interval.ms=300000

############################# Zookeeper #############################

# Zookeeper connection string (see zookeeper docs for details).
# This is a comma separated host:port pairs, each corresponding to a zk
# server. e.g. "127.0.0.1:3000,127.0.0.1:3001,127.0.0.1:3002".
# You can also append an optional chroot string to the urls to specify the
# root directory for all kafka znodes.
zookeeper.connect=zookeeper1:2181,zookeeper2:2181,zookeeper3:2181/kafka 

# Timeout in ms for connecting to zookeeper
zookeeper.connection.timeout.ms=6000


############################## Other ##################################
# I recommend you set this to false in production.
# We'll keep it as true for the course
auto.create.topics.enable=true

```

- **broker.id**: The id of the broker, have to be set differently to every broker (1, 2, 3).
- **advertised.listeners**: That's the one setting why most people have problems with. It's just changes the host name by your machine's IP or hostname. But, there will be a detailed explanation about this one briefly
- **delete.topic.enable**: It depends on your need if it make sense or not to be able to delete topics. For the sake of this sample, we're enabling it
- **log.dirs**: list of directories under which to store log files, comma separated.
- There are some defaults: 
    - **num.partitions=8**
    - **default.replication.factor=3**
    - **min.insync.replicas=2**
- Log Retention Policy
    - **log.retention.hours**: The minimum age of a log file to be eligible for deletion due to age. With 168 hours means that, 7 days from now, if a log is there, it’s going to be deleted.
    - **log.segment.bytes**: The maximum size of a log segment file. When this size is reached a new log segment will be created. the 1073741824 is 1Gib    which may be enough for now.
    - **log.retention.check.interval.ms: **It`s how often you check for deleting stuff. and five minutes is fine
- Zookeeper
    - **zookeeper.connect**: Zookeeper connection string (see [zookeeper docs](https://kafka.apache.org/documentation/) for details). This is a comma separated host:port pairs, each corresponding to a zk (zookeeper) server. e.g “127.0.0.1:3000,127.0.0.1:3001, 127.0.0.1:3002”. You can also append an optional chroot string to the urls to specify the root directory for all kafka znodes. for `zookeeper.connect=zookeeper1:2181,zookeeper2:2181,zookeeper3:2181/kafka` you can see the zookeepers variables that contains their hosts and ports, and, at the very end, a parameter called chroot that tells where is the entry node, the root really, for zookeeper’s file system.** Warning! **Zookeeper become a mess if the parameter is not set right. So, pay attention.
    - **zookeeper.connection.timeout.ms** - The Timeout in ms for connecting to zookeeper
- **auto.create.topics.enable:** Normally false on productions, but nice for this tutorial to be true. It automatically creates topics.

# Hands On: AWS Setup

1. Setup network security to allow Kafka ports(9092)
2. Create and Attach [EBS volumes (basically a drive) ](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ebs-volumes.html)to EC2 Instances (to have a separate drive for Kafka operations)
3. Format the newly attached EBS volumes as XFS (recommended file system for Kafka as per documentation - requires less tuning).
4. Make sure the volume stays mapped on reboot.
5. Apply on all machines

Going to our instance.

The first thing is to create three new volumes:

- **Volume Type: ** We’re going to chose **General Purpose SSD GP2**, because it allows small volumes. But, if you’re serious about your kafka and you want a **cost effective** and **performance effectiv**e Solution you should choose the **Throughput Optimized HDD (st1), **but, for it, you need a minimum 500 GiB.
- **Size: **For this course 2 Gib is enough, but it’s just a demo.
- **Availability Zone:** We’ll need one for availability zone
- **Encryption: **This time there’s no need to encrypt it

After creation we proceed creating the volumes and them attach them. We may create with 2Gib and encryption is up to you.

### Security Groups

The other thing we should do is go to Security Groups and click Kafka Zookeeper, then In bound add the port 9092.

## Instances SetUp

The we're going to use is the following:

```bash

#!/bin/bash

# execute commands as root
sudo su

# Attach the EBS volume in the console, then
# view available disks
lsblk

# we verify the disk is empty - should return "data"
file -s /dev/xvdf

# Note on Kafka: it's better to format volumes as xfs:
# https://kafka.apache.org/documentation/#filesystems
# Install packages to mount as xfs
apt-get install -y xfsprogs

# create a partition
fdisk /dev/xvdf

# format as xfs
mkfs.xfs -f /dev/xvdf

# create kafka directory
mkdir /data/kafka
# mount volume
mount -t xfs /dev/xvdf /data/kafka
# add permissions to kafka directory
chown -R ubuntu:ubuntu /data/kafka
# check it's working
df -h /data/kafka

# EBS Automount On Reboot
cp /etc/fstab /etc/fstab.bak # backup
echo '/dev/xvdf /data/kafka xfs defaults 0 0' >> /etc/fstab

# reboot to test actions
reboot
sudo service zookeeper start
```