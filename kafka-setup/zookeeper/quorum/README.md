# Quorum Setup

It’s good to remember that we’re going to create three different machine. So we gonna:

1. Create an [**AMI**](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/AMIs.html])** **(image) from the existing machines.
2. Create other 2 machines and launch Zookeeper on them
3. Test that the Quorum is running and working

### Create an image

Go ahead, click on it and create an image based on your zookeeper installation. It may take a while, because it’s going to shut down your instance, copy it and then create the image.

### Create 3 new machines

So, being you in the AMI screen, click on launch.

### Quorum

For creating the quorum we’re gonna use some scripts. But it’s important to notice that the three zookeepers have to have the same exact configuration.

So, after installing the zookeeper config, and aquiring them in the certain and proper way, we may have this Quorum working to protect our data. So, if one zookeeper fall, you’re safe!

Some usegfull commands we had to pay atention was:

```bash
#!/bin/bash
# create data dictionary for zookeeper
sudo mkdir -p /data/zookeeper
sudo chown -R ubuntu:ubuntu /data/
# declare the server's identity
echo "1" > /data/zookeeper/myid
# edit the zookeeper settings
rm /home/ubuntu/kafka/config/zookeeper.properties
nano /home/ubuntu/kafka/config/zookeeper.properties
# restart the zookeeper service
sudo service zookeeper stop
sudo service zookeeper start
# observe the logs - need to do this on every machine
cat /home/ubuntu/kafka/logs/zookeeper.out | head -100
nc -vz localhost 2181
nc -vz localhost 2888
nc -vz localhost 3888
echo "ruok" | nc localhost 2181 ; echo
echo "stat" | nc localhost 2181 ; echo
bin/zookeeper-shell.sh localhost:2181
# not happy
ls /
```

To enter into a zookeeper go like:

``` 
bin/zookeeper-shell.sh zookeeper2:2181
```
