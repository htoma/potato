namespace Potato
{
    public class AzureSettings
    {
        public Cosmos Cosmos { get; set; }
    }

    public class Cosmos
    {
        public string EndpointUrl { get; set; }
        public string Key { get; set; }

    }

    public class Chat
    {
        public string Love { get; set; }
    }
}