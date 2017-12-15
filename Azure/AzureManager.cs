namespace Azure
{
    public class AzureManager
    {
        protected const string DbName = "Potato";

        protected readonly Cosmos Cosmos;

        public AzureManager(string endpoint, string key)
        {
            Cosmos = new Cosmos(endpoint, key, DbName);
        }
    }
}