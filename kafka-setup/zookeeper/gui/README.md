# Management tools for Zookeeper

- You can build your own using the 4 Letter Words
- Or use one of the following
    - Netflix Exhibitor
    - Zookeeper UI (web)
    - Zookeeper GUI (desktop) - Windows binaries available
    - ZK-web (not updated since Jult 2016)
    - ZooNavigator (promising new project)


### ZooNavigator setup


You may create an image with docker installed and then 

```bash
nano zoonavigator.
```

paste some code from tools:

```yml
version: '2'

services:
  # https://github.com/elkozmon/zoonavigator
  web:
    image: elkozmon/zoonavigator-web:latest
    container_name: zoonavigator-web
    network_mode: host
    environment:
      API_HOST: "localhost"
      API_PORT: 9001
      SERVER_HTTP_PORT: 8001
    depends_on:
     - api
    restart: always
  api:
    image: elkozmon/zoonavigator-api:latest
    container_name: zoonavigator-api
    network_mode: host
    environment:
      SERVER_HTTP_PORT: 9001
    restart: always

```

When it's up and running we can see with:

```docker ps```

if it's there. Once it's there, we can just go to the IP and to the specified port and see the really nice UI