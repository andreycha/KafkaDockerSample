```
dotnet publish -c Release -o Published
docker-compose -f docker-compose.yml up
dotnet Published\KafkaDockerSample.dll
docker-compose -f docker-compose.yml down
```