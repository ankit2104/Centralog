using CentraLogClient;

var logger = new CentraLogLogger("./settings.json");
while (true) {
    Thread.Sleep(4000);
    logger.Log("Messages inserting", "NO data");
}