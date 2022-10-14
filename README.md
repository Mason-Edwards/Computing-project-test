# Computing-project-test

# Project structure

#Commands for influxdb
Create docker volume for influx db
Run container: docker container run -p 8086:8086 -v influxdb:/var/lib/influxdb influxdb
Go to web UI at localhost:8086 to get token of created user

Format is measurement, host, value:
influx write --bucket test -o test  --token mGKNA3ucyOT2rnIuxmGYQAbnrYkBGaT0Piiotl2AFurXVbCl8ExjRok5I9IKI5f94prJziCGSezz7J_JQUGkBg== "SteeringAngle,host=AutonomousCar value=80"
influx write --bucket test -o test  --token mGKNA3ucyOT2rnIuxmGYQAbnrYkBGaT0Piiotl2AFurXVbCl8ExjRok5I9IKI5f94prJziCGSezz7J_JQUGkBg== "myMeasurementName fieldKey=1 1556813561098000000"
influx write --bucket test -o test  --token mGKNA3ucyOT2rnIuxmGYQAbnrYkBGaT0Piiotl2AFurXVbCl8ExjRok5I9IKI5f94prJziCGSezz7J_JQUGkBg== "SteeringAngle value=1 1556813561098000000"

test inserting data via web endpoint (create service which is a wrapper for hitting the endpoint?).

# Depenencies
Confluent.Kafka - https://docs.confluent.io/platform/current/clients/confluent-kafka-dotnet/_site/api/Confluent.Kafka.html

# Current development notes regarding influxdb

docker volume create influxdb
docker container run -p 8086:8086 -v influxdb:/var/lib/influxdb influxdb

https://hub.docker.com/_/influxdb
There are a number of enviroment variables that can be passed in to automatically set up influx. The ones we are interested in are:
DOCKER_INFLUXDB_INIT_MODE
DOCKER_INFLUXDB_INIT_USERNAME
DOCKER_INFLUXDB_INIT_PASSWORD
DOCKER_INFLUXDB_INIT_ORG
DOCKER_INFLUXDB_INIT_BUCKET
DOCKER_INFLUXDB_INIT_RETENTION
DOCKER_INFLUXDB_INIT_ADMIN_TOKEN

This means that we can completely configure what we need at the stage of running the container.

docker container run -p 8086:8086 --name InfluxDb -v TelemetryTest:/var/lib/influxdb -e DOCKER_INFLUXDB_INIT_MODE=setup -e DOCKER_INFLUXDB_INIT_USERNAME=admin -e DOCKER_INFLUXDB_INIT_PASSWORD=admin123 -e DOCKER_INFLUXDB_INIT_ORG=example -e DOCKER_INFLUXDB_INIT_BUCKET=TelemetryData -e DOCKER_INFLUXDB_INIT_RETENTION=0 -e DOCKER_INFLUXDB_INIT_ADMIN_TOKEN=TVFyhzXR5g6E4nPxMPY6aUjhXwstDVQlbSTd94k-rOHa3TNyBtE6wnqGbSTqrXvyuGw-cI0hXw6pjVD4u_eejg== influxdb


influx setup -> Does inital user setup though command line, instead of having to go though web ui
influx auth list -> gets token 


Created docker file in repo root for setting up influxdb without need for any configuration
The docker file takes in all of the values (except DOCKER_INFLUXDB_INIT_MODE) for setting the above mentioned enviroment variables so that no influx cli commands need to be run to set up the database.
One important thing to note is that the "token" argument is the only argument that doesnt have a default value, so users need to set their own private API token. The rest of the arguments have default values to save some time testing.

An example of building the influxdb image: 
docker build -t testinfluxdb --build-arg token=mGKNA3ucyOT2rnIuxmGYQAbnrYkBGaT0Piiotl2AFurXVbCl8ExjRok5I9IKI5f94prJziCGSezz7J_JQUGkBg== .

Eveything has now been moved out to the docker compose file, so to get everything setup all that is needed is the "docker compose up" command to be executed.

Need to fix kafka docker image...
