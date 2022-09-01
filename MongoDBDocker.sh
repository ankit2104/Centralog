docker run -d -p 27017:27017/tcp -v CentraLogData:/data/db --name CentraLogMongoDB mongo

docker exec -it CentraLogMongoDB bash
mongo
use db
createuser
