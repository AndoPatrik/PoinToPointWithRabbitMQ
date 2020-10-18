# PoinToPointWithRabbitMQ

This repo aims to simulate the concept of the Point to Point messaging with RabbitMQ. Both publisher and subscriber are implemented in C#

## The exercise description

* Make an endpoint that receives HTTP POST with three params:
  * Name
  *  Email
  *  Phone no
* When the endpoint gets called send a message to "people" queue with the received params.
* The consumer needs to print the data and serialize it by using protobuf in a file called person.proto

## Usage

Be aware, that the urls and ports may be differ based on their definition

1. Run your RabbitMQ server on localhost:15672 or localhost:5672. (Make sure to match port in both Publisher & Consumer)
2. Spin up the Publisher and call https://localhost:5001/api/MessagePublisher / http://localhost:5000/api/MessagePublisher
3. Start Consumer CLI application
