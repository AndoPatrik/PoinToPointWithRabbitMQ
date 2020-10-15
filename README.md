# PoinToPointWithRabbitMQ

This repo aims to simulate the concept of the Point to Point messaging with RabbitMQ. Both publisher and subscriber are implemented in C#

## The exercise description

* Make an endpoint that receives HTTP POST with three params:
  * Name
  *  Email
  *  Phone no
* When the endpoint gets called send a message to "people" queue with the received params.
* The consumer needs to print the data and serialize it by using protobuf in a file called person.proto

